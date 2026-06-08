export default function PromptSkeleton() {
  return (
    <div className="bg-gray-800 rounded-lg p-4 animate-pulse">
      <div className="flex items-center justify-between mb-2">
        <div className="h-3 w-24 bg-gray-700 rounded" />
        <div className="h-5 w-16 bg-gray-700 rounded-full" />
      </div>
      <div className="h-4 w-3/4 bg-gray-700 rounded mt-2" />
    </div>
  );
}
