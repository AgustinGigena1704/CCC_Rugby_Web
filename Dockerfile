# Usar la imagen base de .NET 8 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["CCC_Rugby_Web.csproj", "./"]
RUN dotnet restore "CCC_Rugby_Web.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar y publicar el proyecto
RUN dotnet build "CCC_Rugby_Web.csproj" -c Release -o /app/build
RUN dotnet publish "CCC_Rugby_Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Usar la imagen runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Instalar ca-certificates para SSL
RUN apt-get update && apt-get install -y ca-certificates && rm -rf /var/lib/apt/lists/*

# Copiar la aplicación publicada
COPY --from=build /app/publish .

# Copiar el certificado SSL para MySQL
COPY ca.pem ./ca.pem

# Exponer el puerto
EXPOSE 8080

# Variables de entorno
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "CCC_Rugby_Web.dll"]