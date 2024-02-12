import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

export default function ChatReplyForm() {
  return (
    <div className="p-4 shrink-0 basis-16">
      <form>
        <div className="flex gap-4">
          <Input className="" placeholder={`Reply ...`} />
          <Button onClick={(e) => e.preventDefault()} className="ml-auto">
            Send
          </Button>
        </div>
      </form>
    </div>
  );
}
