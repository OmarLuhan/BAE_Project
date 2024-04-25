# Utiliza la imagen base con el SDK de .NET 7
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /web_bae

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /web_bae
COPY --from=build /web_bae/out ./

EXPOSE 5249
ENTRYPOINT ["dotnet", "CapstoneG14.dll"]