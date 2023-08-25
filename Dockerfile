# Use a suitable base image that includes the necessary tools
FROM mcr.microsoft.com/dotnet/sdk:7.0

# Install Entity Framework Core tools
RUN dotnet tool install --global dotnet-ef

# Add the tools directory to the PATH
ENV PATH "{$PATH}:/root/.dotnet/tools"

WORKDIR /app

# Copy the project files to the container
COPY LibMgmtSys.Backend/Api/Api.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY LibMgmtSys.Backend/. ./

# Run Entity Framework Core migrations
CMD ["dotnet", "ef", "database", "update", "-p", "Infrastructure.csproj", "-s", "Api.csproj"]

