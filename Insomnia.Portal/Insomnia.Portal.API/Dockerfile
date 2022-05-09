#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Insomnia.Portal.API/Insomnia.Portal.API.API/Insomnia.Portal.API.API.csproj", "Insomnia.Portal.API/Insomnia.Portal.API.API/"]
COPY ["Insomnia.Portal.API/Insomnia.Portal.API.BI/Insomnia.Portal.API.BI.csproj", "Insomnia.Portal.API/Insomnia.Portal.API.BI/"]
COPY ["Insomnia.Portal.API/Insomnia.Portal.API.Data/Insomnia.Portal.API.Data.csproj", "Insomnia.Portal.API/Insomnia.Portal.API.Data/"]
COPY ["Insomnia.Portal.API/Insomnia.Portal.API.General/Insomnia.Portal.API.General.csproj", "Insomnia.Portal.API/Insomnia.Portal.API.General/"]
RUN dotnet restore "Insomnia.Portal.API/Insomnia.Portal.API.API/Insomnia.Portal.API.API.csproj"
COPY . .
WORKDIR "/src/Insomnia.Portal.API/Insomnia.Portal.API.API"
RUN dotnet build "Insomnia.Portal.API.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insomnia.Portal.API.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insomnia.Portal.API.API.dll"]