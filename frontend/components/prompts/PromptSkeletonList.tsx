import PromptSkeleton from "./PromptSkeleton";

interface PromptSkeletonListProps {
  loading: boolean;
}

export default function PromptSkeletonList({
  loading,
}: PromptSkeletonListProps) {
  return (
    <>
      {loading && (
        <div className="flex flex-col gap-3">
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
          <PromptSkeleton />
        </div>
      )}
    </>
  );
}
