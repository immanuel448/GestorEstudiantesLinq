# 🎓 Gestor de Estudiantes en C# (.NET)  
**LINQ · Entity Framework Core · SQLite · JSON**

Proyecto desarrollado en **C# (.NET)** que demuestra el uso práctico de **LINQ**, **Entity Framework Core** y **SQLite** para la gestión de datos, aplicando buenas prácticas de estructura, persistencia y validación de información.

El sistema permite administrar estudiantes desde una aplicación de consola, realizando consultas eficientes con LINQ y almacenando la información de forma persistente.

---

## 🚀 Funcionalidades principales

✅ Gestión de estudiantes mediante un menú interactivo en consola  
✅ Consultas avanzadas utilizando LINQ  
✅ Persistencia de datos con SQLite y Entity Framework Core  
✅ Exportación e importación de datos en formato JSON  
✅ Validaciones de entrada para evitar datos incorrectos  
✅ Separación clara de responsabilidades en el código  

---

## 📂 Estructura del proyecto

```
/GestorEstudiantesLinq/
│
├── Estudiante.cs            # Entidad principal del dominio
├── AppDbContext.cs          # DbContext para EF Core + SQLite
├── GestorEstudiantes.cs     # Lógica de negocio y consultas LINQ
├── Program.cs               # Punto de entrada y menú interactivo
└── estudiantes.db           # Base de datos SQLite (generada en runtime)
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

- Filtrado por carrera (`Where`)
- Ordenamiento por edad (`OrderBy`, `OrderByDescending`)
- Proyección de datos (`Select`)
- Agrupación y conteo (`GroupBy`, `Count`)
- Evaluación de condiciones (`Any`)
- Obtención de registros específicos (`First`)
- Cálculo de promedios (`Average`)
- Proyección con objetos anónimos
- Resúmenes personalizados de información

---

## 🔄 Funcionalidades adicionales

- Menú interactivo en consola
- Validación de datos ingresados por el usuario
- Exportación e importación de información a archivos JSON
- Uso de migraciones para control de la base de datos
- Manejo básico de excepciones en operaciones de archivo

---

## 💾 Requisitos

Asegúrate de tener instalado:

```bash
dotnet --version
# Se recomienda .NET 8 o superior
```

Instala los paquetes necesarios:

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

## 📈 Posibles extensiones futuras

El proyecto sigue el principio de responsabilidad única:

- Exposición de la lógica mediante una API REST
- Interfaz gráfica (Web o Desktop)
- Autenticación de usuarios
- Persistencia en otros motores de base de datos

---

## 📽️ Curso relacionado

Este proyecto forma parte de una serie de videos en YouTube donde se explica cada paso desde cero.

[Video resumido del Proyecto en Youtube](https://www.youtube.com/watch?v=1Q5n6XDp2uI&t=50s)

---

## ✅ Autor

Desarrollado por [immanuel448](https://github.com/immanuel448)

