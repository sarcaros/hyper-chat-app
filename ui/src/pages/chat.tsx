import Logo from "@/components/chat/logo";
import Room from "@/components/chat/room";
import RoomCreate from "@/components/chat/room-create";
import RoomsList from "@/components/chat/rooms-list";
import {
  ResizableHandle,
  ResizablePanel,
  ResizablePanelGroup,
} from "@/components/ui/resizable";
import { Separator } from "@/components/ui/separator";
import ChatSignalRService from "@/lib/chatsignalr";
import { useAuth } from "@clerk/clerk-react";
import { useEffect } from "react";

export default function Chat() {
  const { getToken } = useAuth();

  useEffect(() => {
    async function connect() {
      const token = await getToken({ template: "HyperChatAppBackendAuth" });
      if (token) {
        const signalR = new ChatSignalRService(token);
        signalR.OnMessageRecieved = (message) => console.log("MR", message);
        await signalR.start("r_ecOQ6VivkqJlbpK6ej09M");
      }
    }

    connect();
  });

  return (
    <div className="flex flex-col h-screen">
      <ResizablePanelGroup direction="horizontal" className="items-stretch">
        <ResizablePanel defaultSize={20} minSize={15} maxSize={25}>
          <Logo />
          <Separator />
          <RoomCreate />
          <Separator />
          <RoomsList />
        </ResizablePanel>
        <ResizableHandle withHandle />
        <ResizablePanel minSize={30} className="flex flex-col">
          <Room />
        </ResizablePanel>
      </ResizablePanelGroup>
    </div>
  );
}
