import { UserButton } from "@clerk/clerk-react";

export default function RoomHeader() {
  return (
    <div className="flex items-center px-4 py-2 basis-12">
      <h1 className="text-xl font-bold">Chat room name</h1>
      <div className="ml-auto">
        <UserButton />
      </div>
    </div>
  );
}
