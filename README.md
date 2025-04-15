# API Gestor de Turnos

Esta API está pensada para un gestor de turnos pequeño para un centro médico.  
Su objetivo es facilitar la administración de turnos y la gestión de la información de doctores, pacientes y usuarios.

## Descripción

La API permite:
- **Gestión de Turnos:** Creación, edición y eliminación de turnos.
- **Administración de Usuarios:** Registro y manejo de la información de doctores, pacientes y otros usuarios.
- **Expansión Futura:** A futuro se planea incorporar historiales médicos, definir precios de consultas y gestionar servicios ofrecidos por cada doctor, entre otras características.

## Características

- **Turnos:** Creación y gestión completa de turnos.
- **Usuarios:** Registro y administración de la información de doctores, pacientes y demás usuarios.
- **Escalabilidad:** Código preparado para futuras mejoras como historiales médicos y servicios personalizados.

## Tecnologías y Dependencias

La API está desarrollada en C# utilizando ASP.NET y se apoya en Docker para su despliegue en contenedores.  
Entre las dependencias utilizadas destacan:
- **EntityFramework:** Versión 9.0.2  
- **Microsoft.VisualStudio.Azure.Containers.Tools.Targets:** Versión 1.21.0  
- **Swashbuckle.AspNetCore:** Versión 6.6.2  

Para conocer las versiones de otras dependencias, revisa el archivo `.csproj` o ejecuta el comando `dotnet list package` en la raíz del proyecto.

## Instalación y Ejecución

### Requisitos Previos

- **Visual Studio:** El proyecto se ejecuta y se desarrolla desde Visual Studio.  
- **Docker:** Se requiere tener Docker instalado y en ejecución, ya que la API utiliza un `Dockerfile` para construir la imagen del contenedor.

### Ejecución desde Visual Studio

1. Abre la solución en Visual Studio.
2. Asegúrate de que Docker esté funcionando en tu sistema.
3. Ejecuta el proyecto en modo desarrollo; este modo habilita la documentación interactiva de la API mediante Swagger.

### Documentacion de la API
La documentación de la API se genera dinámicamente usando SwaggerUI. En el código se configura de la siguiente manera:

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Cuando la aplicación se ejecuta en modo desarrollo, podrás acceder a la documentación navegando a la ruta /swagger.
