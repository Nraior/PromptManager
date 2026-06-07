import { Prompt } from "@/lib/types";
import PromptStatusIndicator from "./PromptStatusIndicator";

export interface PromptsListProps {
  prompts: Prompt[];
  bottomRef: React.RefObject<HTMLDivElement | null>;
  promptsListRef: React.RefObject<HTMLDivElement | null>;
  updatePaginationLimit: () => void;
  showLoadMoreButton: boolean;
}
export default function PromptsList({
  prompts,
  bottomRef,
  promptsListRef,
  updatePaginationLimit,
  showLoadMoreButton,
}: PromptsListProps) {
  return (
    <div
      ref={promptsListRef}
      className="h-full overflow-y-auto flex flex-col gap-3"
    >
      {showLoadMoreButton && (
        <button
          className="text-xs bg-gray-800 text-gray-400 px-3 py-1 rounded-full hover:bg-gray-700 hover:text-white transition-colors"
          onClick={updatePaginationLimit}
        >
          Load more
        </button>
      )}
      {prompts.map((prompt) => {
        return (
          <div
            className="bg-gray-800 rounded-lg p-4 text-white"
            key={prompt.id}
          >
            <div className="flex items-center justify-between mb-2">
              <span className="text-sm text-gray-400">
                {new Date(prompt.dateAsked).toLocaleString()}
              </span>
              {<PromptStatusIndicator status={prompt.status} />}
            </div>

            <p className="text-white">{prompt.text}</p>
            {prompt.response && (
              <p className="mt-2 text-gray-300 border-t border-gray-700 pt-2">
                {prompt.response}
              </p>
            )}
          </div>
        );
      })}
      <div ref={bottomRef} />
    </div>
  );
}
