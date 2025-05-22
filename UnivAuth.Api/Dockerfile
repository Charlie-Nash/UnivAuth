# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos del proyecto y restaurar dependencias
COPY ["UnivAuth.Api/*.csproj", "UnivAuth.Api/"]
COPY ["UnivAuth.Application/*.csproj", "UnivAuth.Application/"]
COPY ["UnivAuth.Domain/*.csproj", "UnivAuth.Domain/"]
COPY ["UnivAuth.Infrastructure/*.csproj", "UnivAuth.Infrastructure/"]

RUN dotnet restore "UnivAuth.Api/UnivAuth.Api.csproj"

# Copiar todo y compilar
COPY . .
RUN dotnet publish "UnivAuth.Api/UnivAuth.Api.csproj" -c Release -o /publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "UnivAuth.Api.dll"]