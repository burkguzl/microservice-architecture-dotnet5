#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Phonebook/Phonebook.API/Phonebook.API.csproj", "src/Phonebook/Phonebook.API/"]
RUN dotnet restore "src/Phonebook/Phonebook.API/Phonebook.API.csproj"
COPY . .
WORKDIR "/src/src/Phonebook/Phonebook.API"
RUN dotnet build "Phonebook.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phonebook.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phonebook.API.dll"]
