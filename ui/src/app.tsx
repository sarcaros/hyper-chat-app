import { SignedIn, SignedOut, useAuth } from "@clerk/clerk-react";
import { type GetToken } from "@clerk/types/dist/session";
import ChatPage from "./pages/chat";
import SignInPage from "./pages/sign-in";
import { useEffect } from "react";

const ApiUrl = import.meta.env.VITE_API_URL;

async function registerUser(getToken: GetToken) {
  const token = await getToken({ template: "HyperChatAppBackendAuth" });

  if (!token) {
    return;
  }

  await fetch(`${ApiUrl}/userinfo`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: "{}",
  });

  window.location.search = "";
}

export default function App() {
  const { getToken } = useAuth();

  useEffect(() => {
    if (window.location.search === "?after-register=") {
      console.log("after register");

      registerUser(getToken);
      return;
    }
  }, [getToken]);

  if (window.location.search === "?after-register=") {
    return <div>Registering</div>;
  }

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
