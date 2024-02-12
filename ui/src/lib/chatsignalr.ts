import * as signalR from "@microsoft/signalr";

const SignalRHubUrl = import.meta.env.VITE_SIGNALR_HUB_URL;

type Message = {
  content: string;
  publicUserId: string;
  Time: string;
};

export default class ChatSignalRService {
  private _connection: signalR.HubConnection;
  private _token: string;

  constructor(userToken: string) {
    console.log("connection to: ", SignalRHubUrl);
    this._token = userToken;
    this._connection = new signalR.HubConnectionBuilder()
      .withUrl(SignalRHubUrl, {
        accessTokenFactory: () => this._token,
      })
      .build();
  }

  private _activeRoomId: string | null = null;

  public OnMessageRecieved: ((message: Message) => void) | null = null;

  public async start(publicRoomId: string) {
    try {
      console.debug("connecting to chat room");
      this._connection.on("SendMessage", (message: Message) =>
        this.OnMessageRecieved?.(message)
      );

      await this._connection.start();
      await this._connection.send(
        "JoinRoom",
        (this._activeRoomId = publicRoomId)
      );
    } catch (error) {
      console.error("Error connecting to the chat room", error);
    } finally {
      this._activeRoomId = null;
    }
  }

  public stop() {
    console.debug("disconnecting from chat room");
    this._connection.off("SendMessage");
    if (this._connection.state !== signalR.HubConnectionState.Connected) {
      this._connection
        .send("LeaveRoom", this._activeRoomId)
        .then(() => this._connection.stop())
        .then(() => (this._activeRoomId = null));
    }
  }

  public async sendMessage(message: string) {
    try {
      console.debug("sending message to chat room");
      await this._connection.send(
        "SendMessageToRoom",
        this._activeRoomId,
        message
      );
    } catch (error) {
      console.error("Error sending message", error);
    }
  }
}
