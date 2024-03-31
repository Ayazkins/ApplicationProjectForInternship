FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Controllers/Controllers.csproj", "Controllers/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Controllers/Controllers.csproj"
COPY . .
WORKDIR "/src/Controllers"
RUN dotnet build "Controllers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Controllers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Controllers.dll"]
