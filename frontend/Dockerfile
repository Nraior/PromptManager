FROM node:22-alpine AS base
RUN npm install -g pnpm

# -----------------------------------
# ETAP 1: Builder
# -----------------------------------
FROM base AS builder
WORKDIR /app

COPY package.json pnpm-lock.yaml ./

# Ominięcie blokady pnpm i ochrona przed złośliwymi skryptami instalacyjnymi
RUN pnpm install --frozen-lockfile --ignore-scripts

# Ręczna kompilacja (rebuild) zaufanych i wymaganych paczek
RUN pnpm rebuild sharp unrs-resolver

COPY . .

ENV NEXT_TELEMETRY_DISABLED=1 
RUN pnpm build

# -----------------------------------
# ETAP 2: Środowisko uruchomieniowe (Runtime)
# -----------------------------------
FROM base AS runtime
WORKDIR /app

# Konfiguracja pod optymalizację
ENV NODE_ENV=production
ENV NEXT_TELEMETRY_DISABLED=1
ENV PORT=3000
EXPOSE 3000

# Załatanie luki bezpieczeństwa: Użytkownik non-root
RUN addgroup --system --gid 1001 nodejs && \
    adduser --system --uid 1001 nextjs

COPY --from=builder /app/public ./public
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

# Zmiana uprawnień z roota na stworzonego użytkownika
USER nextjs

CMD ["node", "server.js"]