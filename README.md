# UnivAuth

Microservicio de autenticación construido con **ASP.NET Core**. Desarrollado bajo principios de arquitectura limpia, con soporte para contenedores Docker.

## 🚀 Características

- API RESTful construida con ASP.NET Core.
- Consume una BD POSTGRES.
- Arquitectura por capas (Domain, Application, Infrastructure, API).
- Preparado para despliegue en Docker.

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- Npgsql (Conexión a POSTGRES)
- Docker & Docker Compose
- Clean Architecture (Separación de responsabilidades)

---

## 📦 Docker Compose

```yaml
services:
  api:
    build:
      context: .
      dockerfile: UnivAuth.Api/Dockerfile
    ports:
      - "5001:8080"
    environment:
      - ConnectionStrings__dbPersona=Server=${POSTGRES_IP_SERVER};Port=3306;Database=${POSTGRES_BD_NAME};User=postgres;Password=${POSTGRES_ROOT_PASSWORD};
```

---

## 📬 Endpoint disponible

### POST `/api/v1/auth/login`

Permite la PRIMERA autenticación para acceder al Sistema Web MVC.

#### 🔹 Request JSON
```json
{
    "Usuario": "CP_10030056",
    "Pwd": "65003001"
}
```

#### 🔹 Response JSON
```json
{
    "usr": "CP_10030056",
    "secreto": "a7b6a8b9-a597-4534-ac51-76034085aec3",
    "tfa": false,
    "status": 200,
    "mensaje": ""
}
```

### POST `/api/v1/auth/login2fa`

Permite la SEGUNDA autenticación para acceder al Sistema Web MVC.

#### 🔹 Request JSON
```json
{
    "Usuario": "CP_10030056",
    "Secreto": "a7b6a8b9-a597-4534-ac51-76034085aec3"
}
```

#### 🔹 Response JSON
```json
{
    "id": 2,
    "persona_id": 25539,
    "usr": "CP_10030056",
    "pwd": "",
    "secreto": "a7b6a8b9-a597-4534-ac51-76034085aec3",
    "activo": true,
    "rol_id": 1,
    "tfa": true,
    "nombre_usuario": "CARLOS ALBERTO PE¥A CARHUAMACA",
    "doc_id": "DNI 10030056",
    "status": 200,
    "mensaje": ""
}
```

---

## ▶️ Cómo ejecutar

1. Clonar este repositorio:
   ```bash
   git clone https://github.com/Charlie-Nash/UnivAuth
   cd UnivAuth
   ```

2. Crear un archivo `.env` con las siguientes variables:
   ```env
   POSTGRES_IP_SERVER=ip_del_servidor_de_BD
   POSTGRES_BD_NAME=nombre_de_la_BD
   POSTGRES_ROOT_PASSWORD=tu_password
   ```

3. Ejecutar con Docker Compose:
   ```bash
   sudo docker-compose up --build -d
   o
   sudo docker compose up --build -d
   ```
---

## 👤 Autor

Desarrollado por **Charlie Nash**

---

## 📄 Licencia

Este proyecto se puede usar libremente para fines educativos o personales.