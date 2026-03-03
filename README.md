# HelfenNeuGedacht.API

Eine .NET 10.0 Web API für das HelfenNeuGedacht-Projekt mit MySQL-Datenbankanbindung.

## 📋 Voraussetzungen

Bevor Sie beginnen, stellen Sie sicher, dass folgende Software auf Ihrem System installiert ist:

- **.NET 10.0 SDK** - [Download](https://dotnet.microsoft.com/download)
- **Docker** & **Docker Compose** - [Download](https://www.docker.com/get-started)
- **Git** - [Download](https://git-scm.com/downloads)
- Eine IDE Ihrer Wahl (optional):
  - Visual Studio
  - JetBrains Rider

## 🚀 Getting Started

### 1. Repository klonen

```bash
git clone https://github.com/lgiselbrecht/HelfenNeuGedacht.API.git
cd HelfenNeuGedacht.API
```

### 2. NuGet Packages installieren

Navigieren Sie zum API-Projektordner und stellen Sie die NuGet-Packages wieder her:

```bash
cd HelfenNeuGedacht.API
dotnet restore
```

#### Verwendete Packages

Das Projekt verwendet folgende Haupt-Packages:

- **Microsoft.EntityFrameworkCore** (10.0.3) - Entity Framework Core
- **Microsoft.EntityFrameworkCore.Design** (10.0.3) - EF Core Design Tools
- **Microsoft.EntityFrameworkCore.Tools** (10.0.3) - EF Core CLI Tools
- **MySql.EntityFrameworkCore** (10.0.1) - MySQL Provider für EF Core
- **Microsoft.AspNetCore.OpenApi** (10.0.3) - OpenAPI/Swagger Support

Falls Sie die Packages manuell installieren müssen:

```bash
dotnet add package Microsoft.EntityFrameworkCore --version 10.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.3
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 10.0.3
dotnet add package MySql.EntityFrameworkCore --version 10.0.1
dotnet add package Microsoft.AspNetCore.OpenApi --version 10.0.3
```

### 3. Docker MySQL Setup

Die Entwicklungsumgebung verwendet eine MySQL-Datenbank, die in einem Docker-Container läuft.

#### MySQL Container starten

Navigieren Sie zum Docker-Ordner und starten Sie den Container:

```bash
cd Docker
docker compose up -d
```

#### Docker-Konfiguration

Die Docker-Compose-Datei erstellt folgende MySQL-Instanz:

- **Image:** MySQL 8.0
- **Container Name:** `mysql_helfenneugedacht`
- **Host Port:** 3307 (mapped zu Container Port 3306)
- **Datenbank:** `helfenneugedacht`
- **Root Password:** `root`
- **User:** `user`
- **Password:** `password`
- **Volumes:** Daten werden in `./mysql_data` persistiert

#### Container-Status überprüfen

```bash
# Alle laufenden Container anzeigen
docker ps

# Container-Logs anzeigen
docker logs mysql_helfenneugedacht
```


### 4. Datenbank-Migrationen (optional)

Falls Entity Framework Migrationen vorhanden sind oder Sie neue erstellen möchten:

```bash
# Zurück zum API-Projektordner
cd ../HelfenNeuGedacht.API

# Datenbank aktualisieren
dotnet ef database update

```

Die API ist nun erreichbar unter:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger UI:** `http://localhost:5000/swagger` (falls konfiguriert)

## 🔧 Konfiguration

### Connection Strings

Die Datenbank-Verbindungseinstellungen befinden sich in:

- **appsettings.json** - Production-Einstellungen (Platzhalter)
- **appsettings.Development.json** - Development-Einstellungen (Docker MySQL)

**Development Connection String:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3307;Database=helfenneugedacht;User=user;Password=password;"
  }
}
```

## 📁 Projektstruktur

```
HelfenNeuGedacht.API2/
├── Docker/
│   ├── docker-compose.yml        # Docker Compose Konfiguration
│   └── mysql_data/                # MySQL Daten-Volume
├── HelfenNeuGedacht.API/
│   ├── Controllers/               # API Controller
│   ├── Infrastructure/
│   │   └── Repositories/
│   │       └── MySqlRepository/   # Datenbank-Repositories
│   ├── Properties/
│   │   └── launchSettings.json    # Launch-Profile
│   ├── appsettings.json           # App-Konfiguration (Production)
│   ├── appsettings.Development.json # App-Konfiguration (Development)
│   ├── Program.cs                 # Entry Point
│   └── HelfenNeuGedacht.API.csproj
└── README.md
```
