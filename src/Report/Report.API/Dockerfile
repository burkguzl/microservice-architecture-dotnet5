#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Report/Report.API/Report.API.csproj", "src/Report/Report.API/"]
COPY ["src/Report/Report.Application/Report.Application.csproj", "src/Report/Report.Application/"]
COPY ["src/Report/Report.Core/Report.Core.csproj", "src/Report/Report.Core/"]
COPY ["src/Common/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/Common/EventBusRabbitMQ/"]
COPY ["src/Report/Report.Infrastructure/Report.Infrastructure.csproj", "src/Report/Report.Infrastructure/"]
RUN dotnet restore "src/Report/Report.API/Report.API.csproj"
COPY . .
WORKDIR "/src/src/Report/Report.API"
RUN dotnet build "Report.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.API.dll"]
