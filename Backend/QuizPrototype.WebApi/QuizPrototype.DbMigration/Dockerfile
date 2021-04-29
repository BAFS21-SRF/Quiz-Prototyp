FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR "/src"
COPY "QuizPrototype.DbMigration/QuizPrototype.DbMigration.csproj" "QuizPrototype.DbMigration/"
RUN dotnet restore "QuizPrototype.DbMigration/QuizPrototype.DbMigration.csproj"
COPY . .
WORKDIR "/src/QuizPrototype.DbMigration"
RUN dotnet build "QuizPrototype.DbMigration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuizPrototype.DbMigration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizPrototype.DbMigration.dll"]