import { getModelInfo } from "@/lib/api";

export default async function Nav() {
  const info = await getModelInfo();
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
