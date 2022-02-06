#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Concession/Concession.API/Concession.API.csproj", "Services/Concession/Concession.API/"]
RUN dotnet restore "Services/Concession/Concession.API/Concession.API.csproj"
COPY . .
WORKDIR "/src/Services/Concession/Concession.API"
RUN dotnet build "Concession.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Concession.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Concession.API.dll"]
