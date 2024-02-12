import { Separator } from "@/components/ui/separator";
import RoomHeader from "./room-header";
import RoomChat from "./room-chat";

export default function room() {
  return (
    <>
      <RoomHeader />
      <Separator />
      <RoomChat />
    </>
  );
}
