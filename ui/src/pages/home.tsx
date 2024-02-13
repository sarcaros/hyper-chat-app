import { Button } from "@/components/ui/button";
import { Play } from "lucide-react";

export default function HomePage() {
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <div>
        <Button
          className="hover:shadow-lg hover:shadow-slate-500 transition-all"
          onClick={() => console.log("Clicked")}
        >
          <Play className="mr-2 h-4 w-4" />
          Hello!
        </Button>
      </div>
    </div>
  );
}
