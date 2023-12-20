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
>1.Clonar el repositorio usando el comando git clone
>
>2. Navegar hacia la carpeta CapstoneG14/
>   
>3. Inglesar el comando dotnet restore
>
>4. Ingresar en la instancia de la base de datos SQL server
>
>5. Ejecutar los scripts que se encuentran en la caprpeta CapstoneG14/Data/
>
>6. 



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
