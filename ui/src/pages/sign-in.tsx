import { SignInButton as ClerkSignInButton } from "@clerk/clerk-react";
import { Button } from "@/components/ui/button";
import { KeyRound } from "lucide-react";

export default function SignIn() {
  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <div>
        <ClerkSignInButton>
          <Button>
            <KeyRound className="mr-2 h-4 w-4" />
            Sign in
          </Button>
        </ClerkSignInButton>
      </div>
    </div>
  );
}
