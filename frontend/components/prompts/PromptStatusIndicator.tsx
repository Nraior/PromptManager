import { PromptStatus } from "@/lib/types";

export interface PromptStatusIndicatorProps {
  status: PromptStatus;
}
export default function PromptStatusIndicator({
  status,
}: PromptStatusIndicatorProps) {
  const labels: Record<PromptStatus, string> = {
    0: "Received",
    1: "Processing",
    2: "Successful",
    3: "Failed",
  };

  const styles = {
    0: "bg-yellow-500/20 text-yellow-400",
    1: "bg-blue-500/20 text-blue-400",
    2: "bg-green-500/20 text-green-400",
    3: "bg-red-500/20 text-red-400",
  };

  return (
    <span className={`text-xs px-2 py-1 rounded-full ${styles[status]}`}>
      {labels[status]}
    </span>
  );
}
