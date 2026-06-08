import { DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE } from "./constants";

const getApiUrl = () => {
    if (typeof window === 'undefined') {
        return process.env.INTERNAL_API_URL ?? 'http://backend:8080';
    }
    return process.env.NEXT_PUBLIC_API_URL ?? 'http://localhost:5000';
};

const API_URL = getApiUrl();

export async function getModelInfo() {
    const res = await fetch(`${API_URL}/api/info/model`);
    return await res.json();
}

export async function getPrompts(pageSize: number = DEFAULT_PAGE_SIZE, pageNumber: number = DEFAULT_PAGE_NUMBER) {
    const params = new URLSearchParams({
        PageNumber: pageNumber.toString(),
        PageSize: pageSize.toString()
    });
    const res = await fetch(`${API_URL}/api/prompts?${params.toString()}`);
    return await res.json();
}

export async function createPrompt(text: string) {
    const res = await fetch(`${API_URL}/api/prompts`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ text })
    });
    return await res.json();
}
