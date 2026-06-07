# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY *.slnx .
COPY PromptManager.Api/*.csproj ./PromptManager.Api/
COPY PromptManager.Application/*.csproj ./PromptManager.Application/
COPY PromptManager.Domain/*.csproj ./PromptManager.Domain/
COPY PromptManager.Infrastructure/*.csproj ./PromptManager.Infrastructure/

RUN dotnet restore

COPY . .
RUN dotnet publish PromptManager.Api/PromptManager.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PromptManager.Api.dll"]