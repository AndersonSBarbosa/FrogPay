# FrogPay

Este projeto é uma API para o registro de lojas no FrogPay. A aplicação foi desenvolvida em .NET Core e utiliza um banco de dados PostgreSQL.

## Requisitos

- Docker
- Docker Compose
- .NET SDK 7.0

## Configuração

### 1. Configurar o Banco de Dados PostgreSQL

Primeiro, você precisa criar um contêiner Docker para o PostgreSQL. Use o seguinte comando:

```bash
docker run --name postgres-db-FrogPay -e POSTGRES_PASSWORD=dunha -e POSTGRES_DB=DBFrogPay -p 5432:5432 -d postgres
```

#### 2. Configurar a Aplicação .NET

Atualize sua string de conexão no arquivo appsettings.json:

``` json
{
  "ConnectionStrings": {
    "ManagerAPIPostgre": "Host=localhost;Port=5432;Database=DBFrogPay;Username=postgres;Password=dunha"
  },
  "Jwt": {
    "Key": "c2tkaGdrbGFzZGpnZnVpZWFydHl3dWl5d3VvaWZoc2RqYWlmaGFzZGxrZmhhdXdpZWFsaGRzdWZoc3VpZGZoc2FkaXVseTgzOTQ3NTM0ODk1eTNoZmRmaGRzYWxrYWZoOHd0aGZhc2RsaXVhZmg4OTQ4aGdpZnVkaGc5ODVoczU5OGhzZGZsdmhzZTg1ZWhnOThoZmxzZHpudmw4ZThlcnR5OGU1Z3llODloZXI4aGdsZHJn",
    "Login": "dunha",
    "Password": "A86E92D20",
    "Issuer": "FrogPayAuthServer",
    "Audience": "FrogPayUsers",
    "DurationInMinutes": "15"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Executar os tests unitários:
``` bash
dotnet test
```


### 3. Criar o Dockerfile
Crie um arquivo chamado Dockerfile no diretório raiz do seu projeto com o seguinte conteúdo:

dockerfile.txt
```
# Use the official .NET image for the base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FrogPay.API/FrogPay.API.csproj", "FrogPay.API/"]
RUN dotnet restore "FrogPay.API/FrogPay.API.csproj"
COPY . .
WORKDIR "/src/FrogPay.API"
RUN dotnet build "FrogPay.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrogPay.API.csproj" -c Release -o /app/publish /p:UseAppHost=false


# Use the base image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrogPay.API.dll"]
```
### 4. Criar o docker-compose.yml
Crie um arquivo chamado docker-compose.yml no diretório raiz do seu projeto com o seguinte conteúdo:

``` yaml
version: '3.8'

services:
  postgres-db:
    image: postgres
    environment:
      POSTGRES_PASSWORD: dunha
      POSTGRES_DB: DBFrogPay
    ports:
      - "5432:5432"
    volumes:
      - postgres-data:/var/lib/postgresql/data

  webapp:
    build:
      context: .
      dockerfile: Dockerfile
    depends_on:
      - postgres-db
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__ManagerAPIPostgre=Host=postgres-db;Port=5432;Database=DBFrogPay;Username=postgres;Password=dunha
    ports:
      - "5000:80"
      - "5001:443"
    volumes:
      - ./logs:/app/logs

volumes:
  postgres-data:
```

### 5. Construir e Executar os Contêineres
Para construir a imagem da sua aplicação e iniciar os contêineres, use os seguintes comandos:

``` bash
docker-compose build
docker-compose up
```

### 6. Parar os Contêineres
Para parar os contêineres, use o seguinte comando:

``` bash
docker-compose down
```