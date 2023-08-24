# Use the official ASP.NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5271

# Use the official SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY ["Api/Api.csproj", "./Api/"]
COPY ["Application/Application.csproj", "./Application/"]
COPY ["Contracts/Contracts.csproj", "./Contracts/"]
COPY ["Domain/Domain.csproj", "./Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "./Infrastructure/"]
RUN dotnet restore "./Api/Api.csproj"
COPY . .

RUN dotnet build "./Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish

# Use the base image again for the final container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
