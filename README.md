![Snipaste_2023-09-25_10-22-11](https://github.com/FranciscoAndreoli/alkemyumsa/assets/72111673/dd305b2b-0c61-4340-9847-823a75f38e3d)

# Sistema de Control de Horas de Servicio
Bienvenido al repositorio del Sistema de Control de Horas de Servicio diseñado para TechOil, líder en el sector Oil & Gas en Latinoamérica. A continuación, encontrarás una descripción detallada de la arquitectura y especificaciones técnicas del proyecto.

# 📌 Introducción
Actualmente, TechOil gestiona manualmente el proceso de control, registro y archivo de horas de servicio dedicadas a diferentes proyectos. Esta solución digital se propone como una forma de modernizar y agilizar dicho proceso.

# 📝 Información del Proyecto
El sistema se divide en dos fases principales:

Backend: Establecimiento de una estructura para el procesamiento y almacenamiento de datos.
Frontend: Diseño de una interfaz amigable e intuitiva para la gestión eficiente de la información.

# 🧱 Especificación de la Arquitectura
La arquitectura del sistema se ha estructurado de la siguiente manera:

Capa Controller
Será el punto de entrada a la API. En los controladores deberíamos definir la menor cantidad de lógica posible y utilizarlos como un pasamanos con la capa de servicios.

Capa DataAccess
Es donde definiremos el DbContext y crearemos los seeds correspondientes para popular nuestras entidades.

Capa Entities
En este nivel de la arquitectura definiremos todas las entidades de la base de datos.

Capa Repositories
En esta capa definiremos las clases correspondientes para realizar el repositorio genérico y la unidad de trabajo.

Capa Core
Es nuestra capa principal, en ella encontramos varios subniveles:

Helper: Definiremos lógica que pueda ser de utilidad para todo el proyecto. Por ejemplo, algún método para encriptar/desencriptar contraseñas.
Interfaces: Se definirán las interfaces que utilizaremos en los servicios.
Mapper: En esta carpeta irán las clases de mapeo para vincular entidad-dto o dto-entidad.
Models: Se definirán los modelos que necesitemos para el desarrollo. Dentro de esta carpeta encontramos DTO, para definirlos ahí dentro.
Services: Se incluirán todos los servicios solicitados por el proyecto.

# ⚙ Especificación Técnica
Para el desarrollo de este proyecto se utilizan las siguientes tecnologías y patrones:

ASP.NET Core Web API en su versión .NET Core 6.
Entity Framework Core con un enfoque model-first para la base de datos.
Patrón Repositorio.
Seguridad basada en roles con JSON Web Tokens (JWT).
Contraseñas encriptadas.

Paquetes instalados:
![Snipaste_2023-09-25_10-32-53](https://github.com/FranciscoAndreoli/alkemyumsa/assets/72111673/2b4bed14-d321-47e2-9186-857f76e1520b)


# 🔄 Especificación de GIT

Se deben seguir las siguientes directrices para el manejo del código fuente:

Crear ramas a partir de DEV con la nomenclatura Feature/Us-xx (donde xx es el número de historia).
El título del pull request debe contener el título de la historia tomada.
Los commits deben ser descriptivos.
El pull request solo debe contener cambios relacionados con la historia tomada.
Añadir capturas de pantalla en la descripción de los pull requests como evidencia.
¡Gracias por ser parte de este proyecto y ayudarnos a mejorar la gestión de horas de servicio de TechOil!
