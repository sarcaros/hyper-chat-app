import { SignedIn, SignedOut } from "@clerk/clerk-react";

import ChatPage from "./pages/chat";
import SignInPage from "./pages/sign-in";

export default function App() {
  return (
    <div>
      {/*
        use the clerk components to determine routing instead of router component
        signedout - display children when signedout
        signedin - display children when signedin
      */}
      <SignedOut>
        <SignInPage />
      </SignedOut>
      <SignedIn>
        <ChatPage />
      </SignedIn>
    </div>
  );
}
