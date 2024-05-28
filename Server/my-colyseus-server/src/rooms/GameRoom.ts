import { Room, Client, Delayed } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Library } from '../Library';
import axios from 'axios';

export class GameRoom extends Room<GameRoomState> {
  maxClients = 2;
  playersDeck = new Map();
  awaitStart: Delayed;
  startTimer: Delayed;
  tickCount: number = 0;
  gameIsStarted: boolean = false;

  onCreate (options: any) {
    console.log("Game Room created!");
    this.setState(new GameRoomState());

    this.onMessage(Library.Spawn, (client, data) => {
      const spawnData = JSON.parse(data.json);
      if(this.playersDeck.get(client.id).includes(spawnData.cardID)) {
        spawnData.serverTime = this.clock.elapsedTime;
        const json = JSON.stringify(spawnData);
        client.send(Library.SpawnPlayer, json);
        this.broadcast(Library.SpawnEnemy, json, { except: client });
      } else {
        client.send(Library.Cheat);
      }
    });
  }

  async onJoin (client: Client, options: any) {
    try{
      let response = await axios.post(
          Library.getDeckURI,
          {
            key: Library.phpKEY,
            userID: options.id
          });
      this.playersDeck.set(client.id, response.data);
    } catch(error) {
      console.error("error:", error);
    }
    this.state.createPlayer(client.sessionId);

    if(this.clients.length < 2) return;

    this.broadcast(Library.getReady);
    this.awaitStart = this.clock.setTimeout(()=>{
      try {
        this.broadcast(Library.start, JSON.stringify(
            {
              player1ID: this.clients[0].id,
              player1: this.playersDeck.get(this.clients[0].id),
              player2: this.playersDeck.get(this.clients[1].id),
            }));
        this.gameIsStarted = true;
      } catch (error) {
        this.broadcast(Library.cancelStart);
      }
    }, 1000);

    this.startTimer = this.clock.setInterval(() => {
      this.tickCount++;
      this.broadcast(Library.StartTick, JSON.stringify({
        tick: this.tickCount,
        time: this.clock.elapsedTime
      }));
      if(this.tickCount > 9)
        this.startTimer.clear();
    }, 1000);
  }

  onLeave (client: Client, consented: boolean) {
    if(this.gameIsStarted === false && this.awaitStart !== undefined && this.awaitStart.active){
      this.broadcast(Library.cancelStart);
      this.awaitStart.clear();
    }
    if(this.playersDeck.has(client.id)) {
      this.playersDeck.delete(client.id);
    }

    this.state.removePlayer(client.sessionId);
  }

}
