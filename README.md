# Backend.NetLab03

Este proyecto representa la capa de backend de una aplicación de gestión inmobiliaria desarrollada en .NET. Proporciona una API RESTful para administrar propiedades, contratos, inquilinos, propietarios y pagos. La API está construida utilizando ASP.NET Core y Entity Framework para la persistencia de datos.

## Tabla de Contenidos

- [Introducción](#introducción)
- [Características](#características)
- [Arquitectura](#arquitectura)
- [Requisitos](#requisitos)
- [Instalación](#instalación)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Uso](#uso)
- [Contribuciones](#contribuciones)
- [Licencia](#licencia)

## Introducción

Este backend es la base de datos y la lógica de negocio para una aplicación de gestión inmobiliaria. Gestiona datos sobre propiedades, inquilinos, contratos y pagos, ofreciendo funcionalidades de CRUD para cada entidad. Utiliza una base de datos relacional con Entity Framework y provee una API REST para la comunicación con el frontend.

## Características

- **Gestión de Propiedades**: CRUD para propiedades inmobiliarias.
- **Gestión de Inquilinos**: CRUD para información de inquilinos.
- **Contratos y Pagos**: Creación, visualización y administración de contratos de alquiler y pagos asociados.
- **Autenticación**: Integración de autenticación para seguridad de acceso.
- **Manejo de Migraciones**: Sistema de migraciones para gestionar cambios en la base de datos de forma organizada.

## Arquitectura

El proyecto sigue una arquitectura limpia de controladores y servicios, donde cada módulo de la aplicación está dividido en:

- **Controladores**: Los controladores (`Controllers`) gestionan las solicitudes HTTP. Algunos ejemplos incluyen `ContratosController.cs`, `InmueblesController.cs` y `PropietariosController.cs`.
- **Modelos**: Representan las entidades de la base de datos, como `Contrato`, `Inmueble`, `Inquilino`, `Pago` y `Propietario`, ubicadas en la carpeta `Models`.
- **Contexto de Datos**: `MyDbContext` en `Data` gestiona la conexión y las interacciones con la base de datos utilizando Entity Framework.

## Requisitos

- **.NET SDK** 8.0 o superior
- **SQL Server** o cualquier base de datos compatible con Entity Framework Core
- Un cliente HTTP (por ejemplo, [Postman](https://www.postman.com/)) para probar la API.

## Instalación

1. **Clona este repositorio**:

   ```bash
   git clone https://github.com/Fermin2049/Backend.NetLab03.git
   ```

2. **Configura la base de datos**:
   - Actualiza el archivo `appsettings.json` con la cadena de conexión de tu base de datos.

3. **Ejecuta las migraciones**:
   - Navega al directorio del proyecto y ejecuta los siguientes comandos para aplicar las migraciones y configurar la base de datos:
     ```bash
     dotnet ef database update
     ```

4. **Ejecuta el proyecto**:
   - Ejecuta el siguiente comando para iniciar la aplicación:
     ```bash
     dotnet run
     ```

5. **Prueba la API**:
   - Utiliza Postman o cualquier cliente HTTP para enviar solicitudes a la API en `https://localhost:{puerto}`.

## Estructura del Proyecto

La estructura básica de este proyecto es la siguiente:

```plaintext
.
├── Controllers/                    # Controladores de API
├── Data/                           # Contexto de datos (Entity Framework)
├── Migrations/                     # Migraciones de base de datos
├── Models/                         # Modelos de datos
├── Properties/                     # Configuración de propiedades
├── wwwroot/                        # Archivos estáticos (imágenes, etc.)
├── appsettings.json                # Configuración de la aplicación
└── Program.cs                      # Configuración de arranque
```

## Uso

### Endpoints Principales

A continuación se presenta una lista de los endpoints clave de la API:

- **Propiedades**
  - `GET /api/inmuebles`: Obtiene todas las propiedades.
  - `POST /api/inmuebles`: Crea una nueva propiedad.
  - `PUT /api/inmuebles/{id}`: Actualiza una propiedad.
  - `DELETE /api/inmuebles/{id}`: Elimina una propiedad.

- **Inquilinos**
  - `GET /api/inquilinos`: Lista de inquilinos.
  - `POST /api/inquilinos`: Añadir un nuevo inquilino.

- **Contratos**
  - `GET /api/contratos`: Obtiene todos los contratos.
  - `POST /api/contratos`: Crea un contrato nuevo.

- **Pagos**
  - `GET /api/pagos`: Lista de pagos.
  - `POST /api/pagos`: Registra un pago nuevo.

Para mayor detalle sobre los endpoints, consulta la documentación de la API o el archivo `TpFinalLaboratorio.Net.http`.

## Contribuciones

Las contribuciones son bienvenidas. Para colaborar, sigue estos pasos:

1. Realiza un fork de este repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz un commit (`git commit -m 'Agregar nueva funcionalidad'`).
4. Sube tus cambios a tu repositorio (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request en este repositorio.

## Licencia

Este proyecto está licenciado bajo la **Licencia MIT**. Consulta el archivo `LICENSE` para más detalles.

---
