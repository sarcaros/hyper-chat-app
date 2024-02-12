import { useState } from "react";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Loader2, UserPlus } from "lucide-react";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Input } from "@/components/ui/input";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";

const fakeApiCall = () => new Promise((resolve) => setTimeout(resolve, 3000));

const formSchema = z.object({
  userName: z.string().min(3).max(30),
});

export default function InviteUser() {
  const [open, setOpen] = useState(false);
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      userName: "",
    },
  });

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    await fakeApiCall();
    console.log(values);
    setOpen(false);
  };

  return (
    <div className="flex flex-col gap-4">
      <nav className="grid gap-1">
        <Dialog open={open} onOpenChange={setOpen}>
          <DialogTrigger asChild>
            <Button
              variant={"link"}
              size={"default"}
              className={cn("justify-start")}
            >
              <UserPlus className="mr-2 h-4 w-4" />
              Invite user
            </Button>
          </DialogTrigger>
          <DialogContent className="sm:max-w-[425px]">
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)}>
                <DialogHeader>
                  <DialogTitle>Invite user</DialogTitle>
                  <DialogDescription>Enter user name.</DialogDescription>
                </DialogHeader>
                <div className="grid gap-4 py-4">
                  <FormField
                    control={form.control}
                    name="userName"
                    render={({ field }) => (
                      <FormItem>
                        <div className="grid grid-cols-4 items-center gap-4">
                          <FormLabel className="text-right">Name</FormLabel>
                          <FormControl>
                            <Input
                              autoComplete={"off"}
                              className="col-span-3"
                              {...field}
                            />
                          </FormControl>
                          <FormMessage />
                        </div>
                      </FormItem>
                    )}
                  />
                </div>
                <DialogFooter>
                  {form.formState.isSubmitting === false ? (
                    <Button type="submit">Invite</Button>
                  ) : (
                    <Button disabled type="submit">
                      <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                      Invite
                    </Button>
                  )}
                </DialogFooter>
              </form>
            </Form>
          </DialogContent>
        </Dialog>
      </nav>
    </div>
  );
}
