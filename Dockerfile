# Use the .NET SDK 7.0 as base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install .NET Framework 4.8
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
       wine \
    && rm -rf /var/lib/apt/lists/* \
    && mkdir /wine \
    && wget -q https://download.microsoft.com/download/E/4/7/E47C5489-9A3D-4F24-B880-7B1F3D1DB5E1/NDP48-KB4503549-x86-x64-AllOS-ENU.exe \
    && wine NDP48-KB4503549-x86-x64-AllOS-ENU.exe /quiet /norestart \
    && rm NDP48-KB4503549-x86-x64-AllOS-ENU.exe

# Change the current directory
WORKDIR /app

# Copy the project files
COPY . .

# Run dotnet restore and build
RUN dotnet restore
RUN dotnet build --configuration Release

# Run tests
RUN dotnet test --configuration Release

# Optionally, create a final image without the SDK
FROM mcr.microsoft.com/dotnet/runtime:5.0
COPY --from=build /app /app
WORKDIR /app