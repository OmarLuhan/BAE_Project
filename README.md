# CapstoneG14

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
