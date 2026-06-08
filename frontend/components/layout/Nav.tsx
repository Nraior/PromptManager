import { getModelInfo } from "@/lib/api";

export default async function Nav() {
  let info = null;
  try {
    info = await getModelInfo();
  } catch {
    console.error("failed to fetch model ");
  }
  return (
    <nav className="bg-gray-900 text-white px-6 py-4 flex items-center justify-between">
      <h1 className="text-xl font-bold select-none">Prompt Manager</h1>
      {info?.model && (
        <p>
          Powered by {info?.provider} {info?.model}
        </p>
      )}
    </nav>
  );
}
