# Examen - Proyecto .NET Core 8 (Web API + MVC)

## 📌 Descripción
Este proyecto consiste en una aplicación desarrollada en .NET Core 8 que implementa Web API y MVC en una misma solución. Se utiliza Entity Framework Core con SQLite para la gestión de datos y se han implementado validaciones, AJAX para actualización dinámica y documentación con Swagger.

---

## ⚙️ Configuración del Proyecto

1. Clona este repositorio:
   ```sh
   git clone https://github.com/jmoroyoqui/Ribbit_JCMG.git
   cd proyecto
   ```

2. Asegúrate de tener instalado:
   - .NET 8 SDK
   - Visual Studio o VS Code
   - SQLite

3. Restaura las dependencias:
   ```sh
   dotnet restore
   ```

4. Configura la base de datos en `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Data Source=productos.db"
   }
   ```

5. Aplica las migraciones y genera la base de datos:
   ```sh
   dotnet ef database update
   ```

6. Ejecuta la aplicación:
   ```sh
   dotnet run
   ```

---

## 🛠️ Modelo de Datos
El proyecto maneja la entidad **Producto** con las siguientes propiedades:

| Propiedad       | Tipo de Dato | Reglas de Validación                        |
|----------------|-------------|---------------------------------------------|
| Id             | int         | Clave primaria, autogenerada               |
| Nombre         | string      | Requerido, entre 3 y 100 caracteres        |
| Descripción    | string      | Opcional, máximo 500 caracteres            |
| Precio         | decimal     | Mayor a 0, requerido                        |
| Stock         | int         | Requerido, mayor o igual a 0               |
| FechaCreacion  | DateTime    | Autogenerada por el sistema                 |

---

## 🚀 Endpoints de la API

### 📌 ProductosController

| Método | Endpoint                     | Descripción                                   |
|--------|------------------------------|-----------------------------------------------|
| GET    | `/api/Productos`             | Obtiene todos los productos                  |
| GET    | `/api/Productos/{id}`        | Obtiene un producto por ID                   |
| POST   | `/api/Productos`             | Crea un nuevo producto                       |
| PUT    | `/api/Productos/{id}`        | Actualiza un producto                        |
| DELETE | `/api/Productos/{id}`        | Elimina un producto                          |

Ejemplo de petición **POST**:
```json
{
  "nombre": "Producto Ejemplo",
  "descripcion": "Descripción del producto",
  "precio": 10.99,
  "stock": 100
}
```

---

## 🎨 Implementación MVC
El proyecto cuenta con un sistema MVC que permite gestionar los productos mediante una interfaz gráfica con las siguientes vistas:

- **Index**: Lista todos los productos.
- **Details**: Muestra detalles de un producto.
- **Create**: Formulario para agregar productos.
- **Edit**: Formulario para editar productos.
- **Delete**: Confirmación para eliminar un producto.

Además, se usa **AJAX** para actualizar los productos sin necesidad de recargar la página.

---

## 🛠️ Dependencias y Servicios

- Entity Framework Core
- SQLite
- Swagger para documentación
- jQuery y AJAX para interactividad en la UI
- Inyección de dependencias para separación de lógica de negocio

---

## 🧪 Pruebas Unitarias
Se han implementado dos pruebas unitarias para la API:

1. No se puede crear un producto con precio negativo.
2. Un producto no puede tener un nombre vacío o menor a 3 caracteres.

Ejecutar las pruebas:
```sh
   dotnet test
```

---

## 📜 Documentación con Swagger
Para acceder a la documentación de la API, ejecutar la aplicación y abrir:
```
http://localhost:5000/swagger
```

---

## 📌 Instrucciones para Ejecutar la Aplicación

1. Ejecutar la API:
   ```sh
   dotnet run --project ProyectoAPI
   ```

2. Ejecutar la aplicación MVC:
   ```sh
   dotnet run --project ProyectoMVC
   ```

La aplicación estará disponible en `http://localhost:5000`.

---

## 📢 Contribución
Si deseas mejorar el proyecto, puedes hacer un **fork** del repositorio, crear una nueva rama y enviar un **pull request**.

---

## 🏆 Autor
Desarrollado por **Julio Cesar Moroyoqui Gil** como parte del examen de .NET Core 8 para Ribbit.

