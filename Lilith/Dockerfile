FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
ENV ASPNETCORE_URLS=http://localhost:5241

COPY ["Lilith/Startup/Startup.csproj", "Lilith/Startup/"]
COPY ["Lilith/Contracts/Contracts.csproj", "Lilith/Contracts/"]
COPY ["Stella/Core/ErrorHandling/ErrorHandling.csproj", "Stella/Core/ErrorHandling/"]
COPY ["Stella/Stella.csproj", "Stella/"]

RUN dotnet restore "Lilith/Startup/Startup.csproj"

COPY . .

RUN dotnet build "Lilith/Startup/Startup.csproj" -c Release -o /app/build

RUN dotnet publish "Lilith/Startup/Startup.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5241

ENTRYPOINT ["dotnet", "Startup.dll"]

