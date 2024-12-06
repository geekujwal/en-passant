FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY Directory.Build.props ./

COPY Lilith/Lilith.csproj ./Lilith/

COPY Stella/ Stella/

RUN dotnet restore Lilith/Lilith.csproj

COPY Lilith/. ./Lilith/
RUN ls -R /app/Lilith

RUN dotnet publish Lilith/Lilith.csproj -c Release -o /app -v diag

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# WORKDIR /app

COPY --from=build /app .

EXPOSE 5241

ENTRYPOINT ["dotnet", "Lilith.dll"]
