FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["all_in_one_dotnet_authorization_server/all_in_one_dotnet_authorization_server.csproj", "all_in_one_dotnet_authorization_server/"]
RUN dotnet restore "all_in_one_dotnet_authorization_server/all_in_one_dotnet_authorization_server.csproj"
COPY . .
WORKDIR "/src/all_in_one_dotnet_authorization_server"
RUN dotnet build "all_in_one_dotnet_authorization_server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "all_in_one_dotnet_authorization_server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "all_in_one_dotnet_authorization_server.dll"]
