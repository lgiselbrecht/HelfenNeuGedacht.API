# Fachhochschule Vorarlberg - Anwendungsintegration / Innovation Project

## HelfenNeuGedacht.API

Eine Web API auf Basis von .NET 10, inklusive MySQL-Datenbank und SignalR für Echtzeit-Kommunikation.

---

## Voraussetzungen

- .NET 10 SDK
- Docker & Docker Compose
- Git

---

## Getting Started

#### 1. Repository klonen


- git clone https://github.com/lgiselbrecht/HelfenNeuGedacht.API.git
- cd HelfenNeuGedacht.API

#### 2. Abhängigkeiten installieren

- dotnet restore

#### 3. Datenbank starten (Docker)

- cd Docker
- docker compose up -d

#### 4. API starten

### Verwendete Packete:

- ASP.NET Core (.NET 10) – Web API
- Entity Framework Core – Datenbankzugriff
- MySQL (Docker) – Datenbank
- SignalR – Echtzeit-Kommunikation
- KonfigurationConnection String (Development):Server=localhost;Port=3307;Database=helfenneugedacht;User=user;Password=password;

## Wichtig:

## Für Dev zwecke haben wir unsere Datenbank auf Port 3307 gemapt siehe docker-compose.yml
