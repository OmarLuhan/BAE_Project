# BAE
<p>
  Diseño e implementacion de un sistema web para
  la gestion del proceso de ventas en la 
  distribuidora Bae
</p>

>[!NOTE]
>  TECNOLOGIAS USADAS
>
>| C#   | .NET     | SQL Server  |
>|------|----------|-------------|
>| JS   | jQuery   | Bootstrap   |
>| HTML | Razor    | CSS         |
>| GIT  |GitHub    |             |
>
> HERRAMIENTAS
>
>| VSCode | SSMS  | Sdk .Net 7 |
>|--------|-------|------------|

>[!IMPORTANT]
>COMO LEVANTAR EL SERVICIO EN MODO DESARROLO

>[!WARNING]
> ANTES DE INICIAR 
>
>1.Tener instalado visual studio code
>
>2.Tener instalado git y tener cuenta en git hub
>
>3.Tener instalado el sdk .NET7
>
>4.Tener instalado o tener acceso a una instancia de
>base de datos SQL server
>
>5.Tener instalado el SSMS SQL Server Management Studio

>[!TIP]
> ESTAS LISTO ? EMPECEMOS
>
>1. Clonar el repositorio usando el comando git clone
>
>2. Navegar hacia la carpeta CapstoneG14/
>   
>3. Ingresar el comando dotnet restore
>
>4. Ingresar en la instancia de la base de datos SQL server
>
>5.  Revisar los scripts que se encuentran en la carpeta CapstoneG14/Data/;
>    encontraras 3 scrpts, el primero es el esquema de la base de dados, el segundo
>    son los inserts con datos iniciales para que el sitema pueda funcionar, y el tercero es 
>    un script con el esquema, datos iniciales, y datos de prueba todo en uno
>
>6. Ejecutar el script del esquema y el script de inserts iniciales
>
>7. Puedes elgir ejecutar el script todo en uno, si haces esto pasar al punto 9
>
>8. Cambiar las configuracion de las apis [ir a configuracion de apis]
>
>9. Regresar a la carpeta CapstoneG14/appsettings.json
>
>10. Cambiar la cadena de conexion por la del servidor actual de bd
>
>11. Ejecutar el comando dotnet clear (no es obligatorio pero puede resolver problemas)
>
>12. Ejecutar el comando dotnet build
>
>13. Ejecutar el comando dotnet run
>
>14. Ya tienes corriendo la palicacion web
>
>15. Logueate con las credenciales N00209455@upn.pe 123
>

>[!CAUTION]
>CONFIGURACION DE APIS
>1. Despues de ejecutar los scripts
>
>2. Realizar un select from * configuracion
>   se mostrara una tabla con 3 columnas
>
>| recurso         | propiedad    | valor |
>|-----------------|--------------|-------|
>| Firebase_storage| email        | xxxxx |
>| Firebase_storage| clave        | xxxxx |
>| Firebase_storage| ruta         |xxx.com|
>| Firebase_storage| api_key      |xxxxxxx|
>| Servicio_Correo | correo       |xxxxxxx|
>| Servicio_Correo | clave        |xxxxxxx|
>| Servicio_Correo | alias        |xxxxxxx|
>
>4. Revisar el archivo configApis
>
>5. Para el recurso servio_Correo:
>   correo [Es el correo con el cual esta configurado el servicio]
>   clave  [Es la contraseña de de aplicacion del correo]
>   alias  [Es el nobre con el que te llegara el correo, no es obligatorio cambiarlo]
>
>7. Para el recurso de Firebase_Storage
> email[Es un usuario con el cual nos vamos a autenticar en el servicio de firebase]
> clave[es la contraseña del usuario para ingresar al servicio de firebase]
> ruta[Es la ruta fisica en donde se encuentra el storage]
>  api_key[Es la llave con la cual ingresamos al sevicio de storage]
>
>6. Reemplazar los valores por los que se enuentran dentro del archivo de
>   configApis

>[!IMPORTANT]
>  QUE HACER SI NO ENCUENTRO EL ARCHIVO configApis?
>
>  LAS APIS NO FUNCIONAN?
>
>  COMO CONSIGO NUEVAS APIS?
>

>[!TIP]
> INSTRUCCIONES PARA CONSGUIR NUEVAS APIS
>

#dotnet new mvc

# orm de sql server

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# dotnet-ef

dotnet tool install --global dotnet-ef

# cadena de conexion

dotnet ef dbcontext scaffold "Server=server_name;Database=bd_name;User=sa;Password=password;Trusted_Connection=False;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

# firebase

dotnet add package Firebase.Auth --version 1.0.0
dotnet add package FirebaseStorage.net --version 1.0.3

# automaper

dotnet add package AutoMapper --version 12.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.0.1

# generar pdf




dotnet add package DinkToPDF
son necesarias las librerias que
se encuentran en LibreriaPDF Y
Las extenciones que se encuentran en
Extenciones


>[!NOTE]
>Esto es una nota importante que debes tener en cuenta.

>[!TIP]
>Aquí tienes un consejo útil para mejorar tu código.

>[!IMPORTANT]
>Por favor, ten en cuenta que esta acción es irreversible.

>[!WARNING]
>Este procedimiento puede causar la pérdida de datos. ¡Ten cuidado!

>[!CAUTION]
>¡Advertencia! Este proceso puede afectar el rendimiento del sistema.
