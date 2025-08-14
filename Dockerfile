# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Copiar el certificado SSL (si es necesario)
COPY ca.pem /app/ca.pem

# Variables de entorno para Railway
ENV ASPNETCORE_URLS=http://+:$PORT
ENV ASPNETCORE_ENVIRONMENT=Production

# Railway asigna dinámicamente el puerto
EXPOSE $PORT
ENTRYPOINT ["dotnet", "CCC_Rugby_Web.dll"]