"use client";

import { useEffect, useRef, useState } from "react";
import { getPrompts, createPrompt } from "@/lib/api";
import { Prompt, PromptStatus } from "@/lib/types";
import {
  DEFAULT_PAGE_SIZE,
  MAX_PAGE_SIZE,
  POLLING_INTERVAL,
} from "@/lib/constants";
import PromptsList from "./PromptsList";
import PromptInput from "./PromptInput";
import PromptSkeletonList from "./PromptSkeletonList";

export default function PromptsWindow() {
  const [prompts, setPrompts] = useState<Prompt[]>([]);
  const [loading, setLoading] = useState(true);
  const [promptSent, setPromptSent] = useState(false);
  const initialScrollDone = useRef(false);
  const userScrolledUp = useRef(false);
  const bottomRef = useRef<HTMLDivElement>(null);
  const [showLoadMoreButton, setShowLoadMoreButton] = useState(true);
  const [errors, setErrors] = useState<string[]>([]);
  const [paginationLimit, updatePaginationLimit] = useState(DEFAULT_PAGE_SIZE);
  const prevLastPromptIdRef = useRef<number | string | null>(null);
  const prevLastPromptStatusRef = useRef<PromptStatus | null>(null);

  const scrollContainerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const lastPrompt = prompts[prompts.length - 1];
    if (!lastPrompt) return;

    if (prevLastPromptIdRef.current !== null) {
      const isNewMessageAtBottom =
        lastPrompt.id !== prevLastPromptIdRef.current;
      const didStatusChange =
        lastPrompt.status !== prevLastPromptStatusRef.current;

      if (
        isNewMessageAtBottom ||
        (didStatusChange && !userScrolledUp.current)
      ) {
        bottomRef.current?.scrollIntoView({ behavior: "smooth" });
      }
    }

    prevLastPromptIdRef.current = lastPrompt.id;
    prevLastPromptStatusRef.current = lastPrompt.status;
  }, [prompts]);

  useEffect(() => {
    const container = scrollContainerRef.current;
    if (!container) return;

    const handleScroll = () => {
      const atBottom =
        container.scrollHeight - container.scrollTop - container.clientHeight <
        10;
      userScrolledUp.current = !atBottom;
    };

    container.addEventListener("scroll", handleScroll);
    return () => container.removeEventListener("scroll", handleScroll);
  }, []);

  useEffect(() => {
    const fetchPrompts = async () => {
      try {
        const fetchedPrompts = await getPrompts(paginationLimit);
        const reachedBackendLimit = paginationLimit >= MAX_PAGE_SIZE;

        if (fetchedPrompts.length < paginationLimit || reachedBackendLimit) {
          setShowLoadMoreButton(false);
        } else {
          setShowLoadMoreButton(true);
        }
        setPrompts([...fetchedPrompts].reverse());
      } catch {
        setShowLoadMoreButton(false);
        console.error("Failed while fetching prompts");
      } finally {
        setLoading(false);
      }
    };

    fetchPrompts();

    const interval = setInterval(fetchPrompts, POLLING_INTERVAL);

    return () => clearInterval(interval);
  }, [paginationLimit]);

  useEffect(() => {
    if (!initialScrollDone.current && prompts.length > 0) {
      bottomRef.current?.scrollIntoView({ behavior: "instant" });
      initialScrollDone.current = true;
    }
  }, [prompts, initialScrollDone]);

  const handleSubmit = async (text: string) => {
    setPromptSent(true);
    setErrors([]);
    try {
      const result = await createPrompt(text);
      if (result?.errors) {
        return setErrors(result.errors.Text);
      }
      const fetchedPrompts = await getPrompts(paginationLimit);
      setPrompts([...fetchedPrompts].reverse());
    } catch {
      const errorMsg = "Failed while creating prompt";
      console.error(errorMsg);
      setErrors([errorMsg]);
    } finally {
      setLoading(false);
      setPromptSent(false);
    }
  };

  return (
    <div className="flex flex-col flex-1 overflow-hidden">
      <div className="flex-1 overflow-y-auto">
        <PromptSkeletonList loading={loading} />

        <PromptsList
          promptsListRef={scrollContainerRef}
          prompts={prompts}
          bottomRef={bottomRef}
          showLoadMoreButton={showLoadMoreButton}
          updatePaginationLimit={() => {
            updatePaginationLimit((current) =>
              Math.min(current + DEFAULT_PAGE_SIZE, MAX_PAGE_SIZE),
            );
          }}
        />
      </div>
      <div className="shrink-0 pt-4">
        <PromptInput onSubmit={handleSubmit} loading={loading || promptSent} />
      </div>
      {errors.map((err) => {
        return (
          <p key={err} className="text-red-500 text-sm font-medium mt-1">
            {err}
          </p>
        );
      })}
    </div>
  );
}
