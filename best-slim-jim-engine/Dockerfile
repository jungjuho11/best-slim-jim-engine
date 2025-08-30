FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["best-slim-jim-engine/best-slim-jim-engine.csproj", "best-slim-jim-engine/"]
RUN dotnet restore "best-slim-jim-engine/best-slim-jim-engine.csproj"
COPY . .
WORKDIR "/src/best-slim-jim-engine"
RUN dotnet build "best-slim-jim-engine.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "best-slim-jim-engine.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "best-slim-jim-engine.dll"]