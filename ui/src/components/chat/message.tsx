export default function Message() {
  return (
    <div
      className={
        "flex flex-col flex-shrink-0 items-start gap-2 rounded-lg border p-3 text-left text-sm transition-all hover:bg-accent"
      }
    >
      <div className="flex w-full flex-col gap-1">
        <div className="flex items-center">
          <div className="flex items-center gap-2">
            <div className="font-semibold">username</div>
          </div>
          <div className={"ml-auto text-xs text-muted-foreground"}>
            date here
          </div>
        </div>
        <div className="text-xs font-medium">subject here</div>
      </div>
      <div className="line-clamp-2 text-xs text-muted-foreground">
        text here
      </div>
    </div>
  );
}
