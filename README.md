# PromptManager

A full-stack application for managing LLM prompts, built with **ASP.NET Core**, **Next.js**, and local AI inference via **Ollama**.

All prompts and data are processed **locally**

---

## 🛠 Core Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core, Entity Framework Core |
| Frontend | Next.js, TypeScript |
| Database | PostgreSQL |
| AI / LLM | Ollama (Mistral) |

---

## Docker
 
> **Work in progress.**
 
---

## 📋 Prerequisites

Make sure the following are installed before running the project:

- [Node.js & pnpm](https://pnpm.io/installation)
- **PostgreSQL** — running locally or via Docker on port `5432`
- [Ollama](https://ollama.com/)

### Ollama — pull the model

The backend uses the **Mistral** model by default. Pull it before starting the app:

```bash
ollama pull mistral
```

---

## 🚀 Local Development Setup

### 1. Database (PostgreSQL)

Make sure your PostgreSQL server is running.

### 2. Backend

Navigate to the backend directory:

```bash
cd backend
```

Initialize secrets and set your connection string:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:PostgresConnection" \
  "Host=localhost;Port=5432;Database=PromptManagerDb;Username=postgres;Password=YOUR_PASSWORD"
```

> Replace `YOUR_PASSWORD` with your actual local PostgreSQL password.

Install dependencies, apply migrations, and start the server:

```bash
dotnet restore
dotnet ef database update
dotnet run --project PromptManager
```

Check the terminal output for the exact `localhost` port the API is running on.

### 3. Frontend

Open a new terminal and navigate to the frontend directory:

```bash
cd frontend
```

Install dependencies and start the dev server:

```bash
pnpm install
pnpm dev
```
