FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["FullStackChallenge.Web.API/FullStackChallenge.Web.API.csproj", "FullStackChallenge.Web.API/"]
RUN dotnet restore "FullStackChallenge.Web.API/FullStackChallenge.Web.API.csproj"
COPY . .
WORKDIR "/src/FullStackChallenge.Web.API"
RUN dotnet build "FullStackChallenge.Web.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FullStackChallenge.Web.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FullStackChallenge.Web.API.dll"]