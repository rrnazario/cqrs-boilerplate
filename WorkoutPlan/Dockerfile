# Build
FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Deploy
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WorkoutPlan.API.dll"]