import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Form, FormField, FormItem } from "@/components/ui/form";
import { Send } from "lucide-react";

const fakeApiCall = () => new Promise((resolve) => setTimeout(resolve, 500));

const formSchema = z.object({
  message: z.string().max(160),
});

export default function ChatReplyForm() {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      message: "",
    },
  });

  const onSubmit = async (values: z.infer<typeof formSchema>) => {
    await fakeApiCall();
    console.log(values);
  };

  return (
    <div className="p-4 shrink-0 basis-16">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <div className="flex gap-4">
            <FormField
              control={form.control}
              name="message"
              render={({ field }) => (
                <FormItem className="flex-1">
                  <Input
                    autoComplete={"off"}
                    placeholder={`Reply ...`}
                    {...field}
                  />
                </FormItem>
              )}
            />
            <Button type="submit" className="ml-auto">
              <Send className="mr-2 h-4 w-4" />
              Send
            </Button>
          </div>
        </form>
      </Form>
    </div>
  );
}
