"use client";

import { useState } from "react";
import { MIN_PROMPT_LENGTH, MAX_PROMPT_LENGTH } from "@/lib/constants";

interface PromptInputProps {
  onSubmit: (text: string) => void;
  loading: boolean;
}

export default function PromptInput({ onSubmit, loading }: PromptInputProps) {
  const [text, setText] = useState("");
  const isValid =
    text.trim().length >= MIN_PROMPT_LENGTH &&
    text.trim().length <= MAX_PROMPT_LENGTH;

  const handleSubmit = () => {
    if (!text.trim() || loading) return;
    onSubmit(text);
    setText("");
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleSubmit();
    }
  };

  return (
    <div className="flex gap-2 items-end border-t border-gray-700 pt-4">
      <textarea
        value={text}
        onChange={(e) => setText(e.target.value)}
        onKeyDown={handleKeyDown}
        disabled={loading}
        placeholder="Type your prompt... (Enter to send, Shift+Enter for new line)"
        rows={3}
        minLength={5}
        maxLength={2000}
        className="flex-1 bg-gray-800 text-white placeholder-gray-500 rounded-lg px-4 py-3 resize-none focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
      />
      <button
        onClick={handleSubmit}
        disabled={loading || !text.trim() || !isValid}
        className="bg-blue-600 hover:bg-blue-500 disabled:opacity-50 disabled:cursor-not-allowed text-white px-4 py-3 rounded-lg transition-colors shrink-0 font-medium"
      >
        {loading ? "..." : "➤"}
      </button>
    </div>
  );
}
