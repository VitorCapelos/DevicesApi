# Devices API

A clean, containerized ASP.NET Core Web API with SQL Server integration.  

This project demonstrates modern backend practices: C# 13+, .NET, Entity Framework Core, automatic migrations, seeding, Docker Compose orchestration, API documentation, unit and integration tests, and developer‑friendly tooling.

---

## ✨ Features ✨

- **ASP.NET Core 8.0** Web API
- **Entity Framework Core** with automatic migrations & seeding
- **SQL Server 2022** running in Docker
- **Swagger / OpenAPI** for interactive API exploration
- **Health Checks** for container readiness
- **Docker Compose** for one‑command startup
- **Seeded demo data** so endpoints return results immediately
- Create, update (PUT/PATCH), fetch (single/all), filter by brand/state, delete
- Domain rules:
  - [X] Immutable CreationTime
  - [X] Name/Brand cannot change when InUse
  - [X] InUse cannot be deleted

---

## Getting Started

### Prerequisites
- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/)

### Run the project
- Keep in mind you must haver Docker as Linux Containers to build. And also Entity Framework Core version 9.

```bash
docker compose up --build
```

Then you should be able to open `http://localhost:8080/swagger/index.html` in you Web Browser to read the documentation and use the API

## Swagger
<img width="1891" height="928" alt="image" src="https://github.com/user-attachments/assets/29555c43-4435-4d8b-a43f-8be0ad0cf55d" />


## Test Explorer

<img width="784" height="395" alt="image" src="https://github.com/user-attachments/assets/5fcbe360-5b95-4c26-bc64-06d3a7c2b145" />

