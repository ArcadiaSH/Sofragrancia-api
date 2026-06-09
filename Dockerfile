FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Sofragrancia-api/Sofragrancia-api.csproj Sofragrancia-api/
RUN dotnet restore Sofragrancia-api/Sofragrancia-api.csproj

COPY . .
WORKDIR /src/Sofragrancia-api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Sofragrancia-api.dll"]
