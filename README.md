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
**WIP**
> ⚠️ **Note:** Docker support is currently experimental and may be unstable.

Available only on the `docker-version` branch:

```bash
git checkout docker-version
docker compose up --build
```

The Docker setup composes the following services:

- **Front-end** – web client
- **Back-end** – API server
- **Ollama** – local LLM runtime (automatically pulls the `mistral` model)
- **PostgreSQL** – relational database

---

## 📋 Prerequisites

Make sure the following are installed before running the project:

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
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
dotnet user-secrets init --project PromptManager/PromptManager.Api.csproj
dotnet user-secrets set "ConnectionStrings:PostgresConnection" "Host=localhost;Port=5432;Database=PromptManagerDb;Username=postgres;Password={YOUR_PASSWORD}" --project PromptManager/PromptManager.Api.csproj
```

> Replace `{YOUR_PASSWORD}` with your actual local PostgreSQL password.

Install dependencies, apply migrations, and start the server:

```bash
dotnet restore
dotnet ef database update --project PromptManager.Infrastructure --startup-project PromptManager
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

## ✅ Verification & Access

Once both servers are running, you can verify the application is working correctly by accessing the following local endpoints:

* **Frontend UI:** [http://localhost:3000](http://localhost:3000) 
* **Swagger API:** [http://localhost:5286/swagger](http://localhost:5286/swagger) — Explore and test the backend endpoints directly. *(Note: Verify your exact port in the backend terminal output).*
* **Hangfire Dashboard:** [http://localhost:5286/hangfire](http://localhost:5286/hangfire) — Monitor background tasks.
