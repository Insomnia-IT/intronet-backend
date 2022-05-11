#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Insomnia.Portal/Insomnia.Portal.API/Insomnia.Portal.API.csproj", "Insomnia.Portal/Insomnia.Portal.API/"]
COPY ["Insomnia.Portal/Insomnia.Portal.BI/Insomnia.Portal.BI.csproj", "Insomnia.Portal/Insomnia.Portal.BI/"]
COPY ["Insomnia.Portal/Insomnia.Portal.General/Insomnia.Portal.General.csproj", "Insomnia.Portal/Insomnia.Portal.General/"]
COPY ["Insomnia.Portal/Insomnia.Portal.Data/Insomnia.Portal.Data.csproj", "Insomnia.Portal/Insomnia.Portal.Data/"]
RUN dotnet restore "Insomnia.Portal/Insomnia.Portal.API/Insomnia.Portal.API.csproj"
COPY . .
WORKDIR "/src/Insomnia.Portal/Insomnia.Portal.API"
RUN dotnet build "Insomnia.Portal.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Insomnia.Portal.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insomnia.Portal.API.dll"]