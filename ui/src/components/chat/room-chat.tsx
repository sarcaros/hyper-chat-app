import { ScrollArea } from "@/components/ui/scroll-area";
import Message from "./message";
import ChatReplyForm from "./chat-reply-form";

export default function RoomChat() {
  return (
    <div className="flex-1 flex flex-col overflow-auto">
      <div className="max-h-full flex-1 flex flex-col  bg-slate-100 overflow-auto">
        <div className="flex flex-col-reverse flex-1 gap-2 p-4 pt-0 overflow-auto">
          <div>1</div>
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
          <Message />
        </div>
      </div>
      <ChatReplyForm />
    </div>
  );
}
