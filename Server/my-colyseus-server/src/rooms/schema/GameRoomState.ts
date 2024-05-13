import {MapSchema, Schema, type} from "@colyseus/schema";
import {Player} from './Player';

export class GameRoomState extends Schema {
  @type({ map: Player })
  players = new MapSchema<Player>();

  createPlayer(sessionId: string) {
    this.players.set(sessionId, new Player());
  }

  removePlayer(sessionId: string) {
    this.players.delete(sessionId);
  }
}
