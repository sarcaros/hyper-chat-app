import { UserButton } from "@clerk/clerk-react";
import InviteUser from "./invite-user";
import { Separator } from "@/components/ui/separator";

export default function RoomHeader() {
  return (
    <div className="flex items-center px-4 basis-12 gap-4">
      <h1 className="text-xl font-bold ">Chat room name</h1>
      <Separator orientation="vertical" />
      <InviteUser />
      <Separator orientation="vertical" />
      <div className="ml-auto">
        <UserButton />
      </div>
    </div>
  );
}
