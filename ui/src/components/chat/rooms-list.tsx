import { cn } from "@/lib/utils";
import { Button } from "../ui/button";

export default function RoomsList() {
  return (
    <div className="flex flex-col gap-4 py-2">
      <nav className="grid gap-1 px-2">
        <Button
          size={"sm"}
          className={cn(
            "dark:bg-muted dark:text-white dark:hover:bg-muted dark:hover:text-white",
            "justify-start"
          )}
        >
          {"Room Name"}
        </Button>
        <Button variant={"ghost"} size={"sm"} className={cn("justify-start")}>
          {"Room Name 2"}
        </Button>
      </nav>
    </div>
  );
}
