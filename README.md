# 🎓 Proyecto LINQ con C#, JSON y SQLite (Entity Framework Core)

Este proyecto es parte de un curso paso a paso sobre **LINQ en C#**, que comienza con una lista en memoria y evoluciona hacia el uso de **JSON** y **SQLite con Entity Framework Core**.

---

## 🚀 ¿Qué incluye el proyecto?

✅ LINQ aplicado a una lista de estudiantes  
✅ Menú interactivo en consola  
✅ Exportación e importación con archivos JSON  
✅ Persistencia con base de datos SQLite usando EF Core  
✅ Estructura clara y separada por responsabilidades

---

## 📂 Estructura del proyecto

```
/GestorEstudiantesLinq/
│
├── Estudiante.cs            # Clase base con propiedades
├── AppDbContext.cs          # DbContext para EF Core + SQLite
├── GestorEstudiantes.cs     # Contiene todos los métodos LINQ y utilidades
├── Program.cs               # Punto de entrada, contiene menú interactivo
└── estudiantes.db           # Base de datos SQLite (se genera en runtime)
```

---

## 🛠️ Tecnologías usadas

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)
- C#
- LINQ
- Entity Framework Core
- SQLite
- JSON (System.Text.Json)

---

## 📌 Consultas LINQ implementadas

1. Filtrar por carrera (`Where`)
2. Ordenar por edad (`OrderBy / OrderByDescending`)
3. Mostrar solo nombres (`Select`)
4. Agrupar por carrera (`GroupBy + Count`)
5. ¿Hay mayores de cierta edad? (`Any`)
6. Estudiante más joven (`First`)
7. Edad promedio (`Average`)
8. Proyección con objetos anónimos
9. Resumen personalizado (nombre, carrera en mayúsculas, mayor de edad)

---

## 🔄 Funcionalidades adicionales

- Menú interactivo en consola
- Validación de entradas desde teclado
- Exportación e importación de estudiantes con archivos JSON
- Guardado automático en base de datos SQLite

---

## 💾 Requisitos

Asegúrate de tener instalado:

```bash
dotnet --version
# Se recomienda .NET 8 o superior
```

Instala los paquetes necesarios (si no están):

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

---

## ▶️ Ejecutar el proyecto

Desde la terminal, en la carpeta raíz del proyecto:

```bash
dotnet run
```

---

## 📚 Estructura modular

El proyecto sigue el principio de responsabilidad única:

- El menú solo muestra opciones
- La lógica de LINQ está en `GestorEstudiantes`
- La persistencia se maneja en `AppDbContext`
- Separación clara entre lógica, entrada de datos y almacenamiento

---

## 📽️ Curso relacionado

Este proyecto forma parte de una serie de videos en YouTube donde se explica cada paso desde cero.

👉 _Enlace próximamente disponible_

---

## ✅ Autor

Creado paso a paso con ayuda de ChatGPT y desarrollado por [immanuel448]
