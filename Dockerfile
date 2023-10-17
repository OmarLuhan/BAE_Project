# Utiliza la imagen base con el SDK de .NET 7
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /web_bae

# Copia el archivo de proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Crear explícitamente el directorio Utilities
RUN mkdir -p /web_bae/Utilities/LibreriaPDF
# Copia la carpeta Utilidades (que contiene libwkhtmltox) al directorio de trabajo dentro del contenedor
COPY Utilities/LibreriaPDF /web_bae/Utilities/LibreriaPDF
RUN ls -la /web_bae/Utilities/LibreriaPDF
# Copia todo el código fuente y compila la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Crea la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /web_bae
COPY --from=build /web_bae/out ./

# Expone el puerto en el que se ejecutará la aplicación
EXPOSE 5000
ENTRYPOINT ["dotnet", "CapstoneG14.dll"]