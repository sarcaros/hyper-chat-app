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

export default function Chat() {
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
