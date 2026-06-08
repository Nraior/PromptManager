# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy only csproj files first for layer caching
COPY PromptManager/PromptManager.Api.csproj ./PromptManager/
COPY PromptManager.Application/PromptManager.Application.csproj ./PromptManager.Application/
COPY PromptManager.Domain/PromptManager.Domain.csproj ./PromptManager.Domain/
COPY PromptManager.Infrastructure/PromptManager.Infrastructure.csproj ./PromptManager.Infrastructure/

# Restore with retries for NuGet instability
RUN dotnet restore PromptManager/PromptManager.Api.csproj --verbosity minimal || dotnet restore PromptManager/PromptManager.Api.csproj --verbosity minimal

# Copy remaining source
COPY . .

# Clean previous build artifacts that might interfere
RUN rm -rf PromptManager/bin PromptManager/obj && \
    dotnet publish PromptManager/PromptManager.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PromptManager.Api.dll"]
