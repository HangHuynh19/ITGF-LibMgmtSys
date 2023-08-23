# Use the official ASP.NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./Api/Api.csproj", ""]
RUN dotnet restore "./Api/Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish

# Use the base image again for the final container
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
