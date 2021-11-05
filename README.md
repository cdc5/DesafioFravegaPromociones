# Desafio Fravega - API promociones 
## Challenge Fravega para consultora Sooft Technology realizado por Christian Damián Cristofano

Se realiza el desafio propuesto "Promociones" utilizando tecnología .Net 5 (.Net Core > 3.1) en C# con base de datos MonogDB y la utilización de contenedores Docker.
El proyecto esta estructurado en una arquitectura de capas "Clean Architecture".

![Arquitectura_Capas](https://user-images.githubusercontent.com/15236085/138007524-a5a868b1-ab3f-46ac-be01-6d81c191c8aa.jpg)

En la que el **Core del negocio (PromocionesFravega.Core)** se encuentra en el centro de la aplicación, en la misma se modelan las entidades necesarias, en este caso solo utilizamos una clase "Promocion" que contiene toda la información para dar solucion a la problemática. La misma capa se comparte con los servicios debido al reducido tamaño de la aplicación por lo que no se consideró necesario realizar un proyecto aparte para los mismos.

Alrededor del Core implementamos las capas:
  - **Infraestructura (PromocionesFravega.Infrastructure)**: Se encarga de la conexión a la base de datos MongoDB.
  - **API (PromocionesFravega.Api)**: Es el contacto con el mundo exterior y la capa contra la cual impactan las solicitudes REST y devuelve los recursos solicitados. Es la capa donde se alojan los controladores.
  - **Testing (PromocionesFravega.UnitTests)**: Se encarga de las pruebas unitarias del proyecto.

## Instalación y uso:

* El proyecto esta realizado en .Net 5 por lo que es necesaria la instalación del paquete de .Net correspondiente, como IDE se utilizó Visual Studio 2019 V16.11.5, para tener compatibilidad con el proyecto (debido a .Net 5) se debe tener una versión de Visual Studio superior o igual a la V16.8
* En el proyecto se provee el archivo "PromocionesFravega.Infrastructure\Data\DB\promociones.json" , con datos de prueba en caso de ser necesario para su carga en MongoDB.
* Mediante la utilización de docker, se pueden generar los contenedores para la ejecución del proyecto:
  - Ejecutar el comando: "docker-compose up -d" dentro del directorío \src del proyecto y docker se encargará de generar dos contenedores:
    
    1: Contendor MongoDB en puerto:27017
    2: Aplicación .Net Core en puerto:8000

Una vez finalizado el proceso de docker ambos contendores se encontrarán aptos para ser utilizados.

* También se puede ejecutar el proyecto desde visual studio desde los comandos ejecutar o depurar, pero se debe tener en cuenta que el contendor de mongoDB debe estar en ejecución para que la aplicación pueda acceder a la Base de Datos.

## Estructura Base de Datos

La base de datos cuenta con un Documento de promociones, correspondiente a la clase brindada para el desfío: "Promocion".

```
public class Promocion
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public IEnumerable<string> MediosDePago { get; private set; }
        public IEnumerable<string> Bancos { get; private set; }
        public IEnumerable<string> CategoriasProductos { get; private set; }
        public int? MaximaCantidadDeCuotas { get; private set; }
        public decimal? ValorInteresCuotas { get; private set; }
        public decimal? PorcentajeDeDescuento { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }
}
```
## API

EL proyecto resuelve el problema planteado exponiendo un endpoints REST para cada punto de la consigna:

* GET:   GetPromociones http://localhost:8000/api/v1/Promocion
* GET:   GetPromocion http://localhost:8000/api/v1/Promocion/GetPromocion?id=971459f6-cfb1-4e1e-b67f-6cf2ddbe2979
* GET:   GetPromocionesVigentes http://localhost:8000/api/v1/Promocion/GetPromocionesVigentes
* GET:   GetPromocionesVigentesPorFecha http://localhost:8000/api/v1/Promocion/GetPromocionesVigentesPorFecha?Fecha=2021-11-01
* GET:   GetPromocionesVigentesPorVenta http://localhost:8000/api/v1/Promocion/GetPromocionesVigentesPorVenta
* POST:  CrearPromocion http://localhost:8000/api/v1/Promocion
* PUT:   ActualizarPromocion http://localhost:8000/api/v1/Promocion
* PATCH: ActualizarPromocionVigencia http://localhost:8000/api/v1/Promocion
* DEL:   EliminarPromocion http://localhost:8000/api/v1/Promocion/EliminarPromocion?id=37365a47-3aad-4791-a038-d48263eb1a87

Se adjunta colección de Postman con todos los llamados a los endpoints creados en "PromocionesFravega.Infrastructure\Postman\Desafio.postman_collection.json"
También al ejecutar la aplicación, se despliega la página principal de swagger con la documentación correspondiente a la Api, 
por lo que los llamados también pueden ser vistos desde la url: http://localhost:8000/swagger/index.html

## Testing
El proyecto PromocionesFravega.UnitTests es el encargado de llevar a cabo las pruebas unitarias, se utilizó la libreria xUnit, para simplificar el proceso de testing. 
La clase **PromocionControllerTest** es donde se encuentran todas las pruebas unitarias realizadas y se encarga de testear a la clase **PromocionController**. Se creó la clase mock PromocionRepositoryMock para simular el comportamiento del accesso a la BD.

![imagen](https://user-images.githubusercontent.com/15236085/140513286-e7e107e6-9c92-4241-bf9e-34d819ae5c62.png)

## Documentación con Swagger
 
Al iniciar el proyecto, se abrirá en el explorador web la pagina de inicio (index.html) de Swagger brindando la documentación correspondiente a las solicitudes de los endpoints.

![imagen](https://user-images.githubusercontent.com/15236085/140513397-9987c37f-a35e-4ccf-96b7-b34992c81f24.png)
![imagen](https://user-images.githubusercontent.com/15236085/140513460-b1e876b1-a3f8-496c-9ff3-6b7fc6bef956.png)
![imagen](https://user-images.githubusercontent.com/15236085/140513539-d50d9585-da58-48be-b634-9a200fe7f6e9.png)




