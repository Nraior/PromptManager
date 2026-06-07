export type PromptStatus = 0 | 1 | 2 | 3;

export interface Prompt {
    id: string;
    text: string;
    status: PromptStatus;
    response: string | null;
    dateAsked: string;
}