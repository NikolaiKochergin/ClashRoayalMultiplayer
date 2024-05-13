import { Room, Client, Delayed } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Library } from '../Library';
import axios from 'axios';

export class GameRoom extends Room<GameRoomState> {
  maxClients = 2;
  playersDeck = new Map();
  awaitStart: Delayed;
  gameIsStarted: boolean = false;

  onCreate (options: any) {
    console.log("Game Room created!");

    this.setState(new GameRoomState());
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

    if(this.clients.length < 2){
      return;
    }

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
    }, 100000);
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
