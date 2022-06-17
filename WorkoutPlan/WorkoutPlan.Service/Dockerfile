FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkoutPlan.Service/WorkoutPlan.Service.csproj", "WorkoutPlan.Service/"]
RUN dotnet restore "WorkoutPlan.Service/WorkoutPlan.Service.csproj"
COPY . .
WORKDIR "/src/WorkoutPlan.Service"
RUN dotnet build "WorkoutPlan.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkoutPlan.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkoutPlan.Service.dll"]