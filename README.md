# 🏠 Backend.NetLab03

**Backend.NetLab03** es la capa de backend de una aplicación de gestión inmobiliaria desarrollada en **.NET**. Proporciona una **API RESTful** para administrar **propiedades, contratos, inquilinos, propietarios y pagos**. La API está construida utilizando **ASP.NET Core y Entity Framework Core** para la persistencia de datos.

---

## 📑 Tabla de Contenidos

1. [Introducción](#-introducción)
2. [Características](#-características)
3. [Arquitectura](#%EF%B8%8F-arquitectura)
4. [Requisitos](#%EF%B8%8F-requisitos)
5. [Instalación](#%EF%B8%8F-instalación)
6. [Estructura del Proyecto](#-estructura-del-proyecto)
7. [Uso](#-uso)
8. [Contribuciones](#%F0%9F%A4%9D-contribuciones)
9. [Licencia](#-licencia)

---

## 📌 Introducción

Este backend es la base de datos y la lógica de negocio para una aplicación de gestión inmobiliaria. Gestiona información sobre **propiedades, inquilinos, contratos y pagos**, ofreciendo funcionalidades **CRUD** para cada entidad. Utiliza **Entity Framework Core** como ORM y provee una **API REST** para la comunicación con el frontend.

---

## 🚀 Características

✅ **Gestión de Propiedades**: CRUD para propiedades inmobiliarias.  
✅ **Gestión de Inquilinos**: CRUD para información de inquilinos.  
✅ **Contratos y Pagos**: Creación, visualización y administración de contratos de alquiler y pagos asociados.  
✅ **Autenticación y Seguridad**: Implementación de autenticación basada en **JWT**.  
✅ **Manejo de Migraciones**: Sistema de migraciones de base de datos con **Entity Framework Core**.  

---

## 🏗️ Arquitectura

El proyecto sigue una **arquitectura limpia** basada en **controladores y servicios**, donde cada módulo de la aplicación está dividido en:

- **Controladores (Controllers)**: Gestionan las solicitudes HTTP. Ejemplos:
  - `ContratosController.cs`
  - `InmueblesController.cs`
  - `PropietariosController.cs`
- **Modelos (Models)**: Representan las entidades de la base de datos, como:
  - `Contrato.cs`
  - `Inmueble.cs`
  - `Inquilino.cs`
  - `Pago.cs`
  - `Propietario.cs`
- **Contexto de Datos (Data)**:
  - `MyDbContext.cs`: Gestiona la conexión y las interacciones con la base de datos usando **Entity Framework Core**.

---

## ⚙️ Requisitos

Antes de ejecutar este proyecto, asegúrate de tener instalado:

- **.NET SDK 8.0** o superior
- **SQL Server** o cualquier base de datos compatible con **Entity Framework Core**
- Un **cliente HTTP** (por ejemplo, Postman) para probar la API

---

## 📥 Instalación

### 1️⃣ Clonar este repositorio

```sh
git clone https://github.com/Fermin2049/Backend.NetLab03.git
cd Backend.NetLab03
```

### 2️⃣ Configurar la base de datos

- Actualiza el archivo `appsettings.json` con la cadena de conexión de tu base de datos.

### 3️⃣ Aplicar migraciones

Ejecuta los siguientes comandos para aplicar las migraciones y configurar la base de datos:

```sh
dotnet ef database update
```

### 4️⃣ Ejecutar el proyecto

Inicia la aplicación con el siguiente comando:

```sh
dotnet run
```

### 5️⃣ Probar la API

- Accede a **[http://localhost:5000/swagger/](http://localhost:5000/swagger/)** para ver la documentación interactiva de la API.
- Usa **Postman** o un **cliente HTTP** para hacer solicitudes a los endpoints.

---

## 📂 Estructura del Proyecto

```bash
Backend.NetLab03/
│── Controllers/                    # Controladores de API
│   ├── ContratosController.cs
│   ├── InmueblesController.cs
│   ├── InquilinosController.cs
│   ├── PagosController.cs
│   ├── PropietariosController.cs
│── Data/                           # Contexto de datos (Entity Framework)
│   ├── MyDbContext.cs
│── Migrations/                     # Migraciones de base de datos
│── Models/                         # Modelos de datos
│   ├── Contrato.cs
│   ├── Inmueble.cs
│   ├── Inquilino.cs
│   ├── Pago.cs
│   ├── Propietario.cs
│── Properties/                     # Configuración de propiedades
│── wwwroot/                        # Archivos estáticos (imágenes, etc.)
│── appsettings.json                # Configuración de la aplicación
│── Program.cs                      # Configuración de arranque
```

---

## 📌 Uso

### 🔹 Endpoints Principales

#### 🏠 Propiedades
- **GET** `/api/inmuebles` → Obtiene todas las propiedades.
- **POST** `/api/inmuebles` → Crea una nueva propiedad.
- **PUT** `/api/inmuebles/{id}` → Actualiza una propiedad.
- **DELETE** `/api/inmuebles/{id}` → Elimina una propiedad.

#### 👤 Inquilinos
- **GET** `/api/inquilinos` → Lista de inquilinos.
- **POST** `/api/inquilinos` → Añadir un nuevo inquilino.

#### 📑 Contratos
- **GET** `/api/contratos` → Obtiene todos los contratos.
- **POST** `/api/contratos` → Crea un contrato nuevo.

#### 💰 Pagos
- **GET** `/api/pagos` → Lista de pagos.
- **POST** `/api/pagos` → Registra un pago nuevo.

Para más detalles, consulta la documentación en **Swagger** o el archivo `TpFinalLaboratorio.Net.http`.

---

## 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Para colaborar, sigue estos pasos:

1️⃣ Haz un fork del repositorio.
2️⃣ Crea una nueva rama:

```sh
git checkout -b feature/nueva-funcionalidad
```

3️⃣ Realiza tus cambios y haz un commit:

```sh
git commit -m "Agregar nueva funcionalidad"
```

4️⃣ Sube los cambios a tu repositorio:

```sh
git push origin feature/nueva-funcionalidad
```

5️⃣ Abre un **Pull Request** en este repositorio.

---

## 📜 Licencia

Este proyecto está licenciado bajo la **Licencia MIT**.

