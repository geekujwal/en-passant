# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the Directory.Build.props file from the root of your project (EnPassant)
COPY Directory.Build.props ./

# Copy the Lilith project file and restore dependencies
COPY Lilith/Lilith.csproj ./Lilith/

# Copy the Stella project (class library) into the container
COPY Stella/ Stella/

# Run dotnet restore to restore dependencies for the Lilith project and its dependencies
RUN dotnet restore Lilith/Lilith.csproj

# Copy the entire Lilith directory and publish it to the /app directory
COPY Lilith/. ./Lilith/
RUN ls -R /app/Lilith

RUN dotnet publish Lilith/Lilith.csproj -c Release -o /app -v diag

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory in the container
# WORKDIR /app

# Copy the published app from the build image
COPY --from=build /app .

# Expose the port your app will run on
EXPOSE 5241

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Lilith.dll"]

