# Gestor de Estudiantes en C# (.NET)
LINQ · Entity Framework Core · SQLite · JSON · Arquitectura por capas

Proyecto desarrollado en **C# (.NET)** para la gestión de estudiantes desde consola, aplicando buenas prácticas de arquitectura, asincronía y separación de responsabilidades.

El sistema permite realizar operaciones **CRUD reales** contra una base de datos SQLite, utilizando **Entity Framework Core** y consultas avanzadas con **LINQ**.

---

## Funcionalidades principales

- Menú interactivo en consola  
- Alta, edición y eliminación de estudiantes (CRUD)  
- Consultas con LINQ  
- Persistencia real con SQLite  
- Arquitectura por capas  
- Repository Pattern  
- Métodos asíncronos (async/await)  
- Validaciones centralizadas con Helpers  
- Exportación a JSON  
- Código refactorizado y escalable  

---

## Arquitectura aplicada

Flujo real del sistema:

Usuario  
→ Helpers (validaciones)  
→ Services (lógica de negocio)  
→ Repository  
→ Entity Framework Core  
→ Base de datos SQLite  

Buenas prácticas:

- Async / Await real  
- Clean Code  
- Separación por capas  
- Repository Pattern  
- Refactorización progresiva  
- Código mantenible  
- Arquitectura profesional  

---

## Tecnologías usadas

- .NET 8  
- C#  
- LINQ  
- Entity Framework Core  
- SQLite  
- JSON  

---

## Ejecución

```bash
dotnet run
# Se recomienda .NET 8 o superior
```

---

## Video del proyecto

Este proyecto está basado en un video donde se explica la versión inicial:

[Video resumido del Proyecto en Youtube](https://www.youtube.com/watch?v=1Q5n6XDp2uI&t=50s)


> **Nota:**  
> El video corresponde a una versión anterior del proyecto.  
> La base conceptual sigue siendo válida (LINQ, EF Core, estructura),  
> pero el proyecto fue mejorado posteriormente con:
>
> - Repository Pattern  
> - Async / Await real  
> - Helpers  
> - Update / Delete  
> - Refactorización  
>
> Esta versión es una **evolución profesional** del video.

---

## Posibles extensiones

- API REST  
- Interfaz Web  
- Autenticación  
- DTOs  
- AutoMapper  
- Logging (Serilog)  
- Paginación  
- Tests unitarios  

---

## Autor

**Immanuel**  
Desarrollado por [immanuel448](https://github.com/immanuel448)



