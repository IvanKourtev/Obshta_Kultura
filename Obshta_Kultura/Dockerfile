FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["Obshta_Kultura.csproj", "./"]
RUN dotnet restore "Obshta_Kultura.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "Obshta_Kultura.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Obshta_Kultura.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Obshta_Kultura.dll"] 