#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7301

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ArtClub.BFF/ArtClub.BFF.csproj", "ArtClub.BFF/"]
RUN dotnet restore "ArtClub.BFF/ArtClub.BFF.csproj"
COPY . .
WORKDIR "/src/ArtClub.BFF"
RUN dotnet build "ArtClub.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArtClub.BFF.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArtClub.BFF.dll"]