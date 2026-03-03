# <Projektname> – C# /.NET Web API

## Entwicklung

Für die **lokale Entwicklung** wird eine **MySQL-Datenbank über Docker** verwendet.

Die Docker-Konfiguration befindet sich im Ordner:


/docker


### MySQL-Container starten

```bash
cd docker
docker compose up -d

Dadurch wird die MySQL-Datenbank für die API lokal gestartet.

Die Connection-Strings sind entsprechend auf die Docker-MySQL-Instanz konfiguriert (siehe appsettings.Development.json).

API starten
dotnet run

Optional mit spezifischem Projekt:

dotnet run --project <ApiProjekt>.csproj
Voraussetzungen

.NET SDK installiert

Docker & Docker Compose installiert
