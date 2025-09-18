# Use official .NET SDK to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files
COPY docFrontend2/*.csproj ./docFrontend2/
RUN dotnet restore ./docFrontend2

# Copy everything else and build
COPY . .
RUN dotnet publish ./docFrontend2 -c Release -o /app/out

# Use runtime image for final app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port Railway expects
ENV PORT 8080
EXPOSE 8080

# Start Blazor Server app
ENTRYPOINT ["dotnet", "docfrontend2.dll"]
