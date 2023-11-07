USE [master]
GO
/****** Object:  Database [BAE]    Script Date: 7/11/2023 14:02:39 ******/
CREATE DATABASE [BAE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BAE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\BAE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BAE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\BAE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BAE] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BAE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BAE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BAE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BAE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BAE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BAE] SET ARITHABORT OFF 
GO
ALTER DATABASE [BAE] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BAE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BAE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BAE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BAE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BAE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BAE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BAE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BAE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BAE] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BAE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BAE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BAE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BAE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BAE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BAE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BAE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BAE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BAE] SET  MULTI_USER 
GO
ALTER DATABASE [BAE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BAE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BAE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BAE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BAE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BAE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BAE] SET QUERY_STORE = OFF
GO
USE [BAE]
GO
/****** Object:  Table [dbo].[Configuracion]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuracion](
	[recurso] [varchar](50) NULL,
	[propiedad] [varchar](50) NULL,
	[valor] [varchar](60) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePedido]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedido](
	[idDetallePedido] [int] IDENTITY(1,1) NOT NULL,
	[idPedido] [int] NULL,
	[idLibro] [int] NULL,
	[editorialLibro] [varchar](100) NULL,
	[tituloLibro] [varchar](100) NULL,
	[generoLibro] [varchar](100) NULL,
	[cantidad] [int] NULL,
	[precio] [decimal](10, 2) NULL,
	[total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDetallePedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[idDetalleVenta] [int] IDENTITY(1,1) NOT NULL,
	[idVenta] [int] NULL,
	[idLibro] [int] NULL,
	[editorialLibro] [varchar](100) NULL,
	[tituloLibro] [varchar](100) NULL,
	[generoLibro] [varchar](100) NULL,
	[cantidad] [int] NULL,
	[precio] [decimal](10, 2) NULL,
	[total] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDetalleVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Editorial]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editorial](
	[idEditorial] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idEditorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[idGenero] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idGenero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro](
	[idLibro] [int] IDENTITY(1,1) NOT NULL,
	[CodigoBarra] [varchar](50) NULL,
	[isbn] [varchar](50) NULL,
	[Titulo] [varchar](100) NULL,
	[precio] [decimal](10, 2) NULL,
	[pendiente] [int] NULL,
	[Autor] [varchar](50) NULL,
	[urlImagen] [varchar](500) NULL,
	[nombreImagen] [varchar](100) NULL,
	[idGenero] [int] NULL,
	[idEditorial] [int] NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[idMenu] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](30) NULL,
	[idMenuPadre] [int] NULL,
	[icono] [varchar](30) NULL,
	[controlador] [varchar](30) NULL,
	[paginaAccion] [varchar](30) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Negocio]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negocio](
	[idNegocio] [int] NOT NULL,
	[urlLogo] [varchar](500) NULL,
	[nombreLogo] [varchar](100) NULL,
	[numeroDocumento] [varchar](50) NULL,
	[nombre] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[direccion] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[porcentajeImpuesto] [decimal](10, 2) NULL,
	[simboloMoneda] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[idNegocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumeroCorrelativo]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumeroCorrelativo](
	[idNumeroCorrelativo] [int] IDENTITY(1,1) NOT NULL,
	[ultimoNumero] [int] NULL,
	[cantidadDigitos] [int] NULL,
	[gestion] [varchar](100) NULL,
	[fechaActualizacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idNumeroCorrelativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[numeroPedido] [varchar](6) NULL,
	[idUsuario] [int] NULL,
	[documentoCliente] [varchar](10) NULL,
	[nombreCliente] [varchar](20) NULL,
	[subTotal] [decimal](10, 2) NULL,
	[impuestoTotal] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](30) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolMenu]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolMenu](
	[idRolMenu] [int] IDENTITY(1,1) NOT NULL,
	[idRol] [int] NULL,
	[idMenu] [int] NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idRolMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDocumentoVenta]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDocumentoVenta](
	[idTipoDocumentoVenta] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idTipoDocumentoVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[correo] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[idRol] [int] NULL,
	[urlFoto] [varchar](500) NULL,
	[nombreFoto] [varchar](100) NULL,
	[clave] [varchar](100) NULL,
	[esActivo] [bit] NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 7/11/2023 14:02:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[idVenta] [int] IDENTITY(1,1) NOT NULL,
	[numeroVenta] [varchar](6) NULL,
	[idTipoDocumentoVenta] [int] NULL,
	[idUsuario] [int] NULL,
	[documentoCliente] [varchar](11) NULL,
	[nombreCliente] [varchar](100) NULL,
	[subTotal] [decimal](10, 2) NULL,
	[impuestoTotal] [decimal](10, 2) NULL,
	[Total] [decimal](10, 2) NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'email', N'autorized@gmail.com')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'clave', N'mentoring')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'ruta', N'ventasasp-8d05c.appspot.com')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'api_key', N'AIzaSyDtIH3_A6mt9YAYTwIBt9Z4D-UIkqI9ZFA')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'carpeta_usuario', N'IMAGENES_USUARIO')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'carpeta_producto', N'IMAGENES_PRODUCTO')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'FireBase_Storage', N'carpeta_logo', N'IMAGENES_LOGO')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'Servicio_Correo', N'correo', N'v43728837@gmail.com')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'Servicio_Correo', N'clave', N'gfggjgcccqxizfeo')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'Servicio_Correo', N'alias', N'Distribuidora autorizada BAE')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'Servicio_Correo', N'host', N'smtp.gmail.com')
INSERT [dbo].[Configuracion] ([recurso], [propiedad], [valor]) VALUES (N'Servicio_Correo', N'puerto', N'587')
GO
SET IDENTITY_INSERT [dbo].[DetalleVenta] ON 

INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (1, 1, 12, N'Oxford University Press', N'Ulysses', N'Infantil', 2, CAST(97.40 AS Decimal(10, 2)), CAST(194.80 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (2, 1, 2, N'Doubleday', N'Brave New World', N'Ciencia Ficción', 2, CAST(89.90 AS Decimal(10, 2)), CAST(179.80 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (3, 1, 17, N'Macmillan Publishers', N'The Lord of the Rings', N'Arte', 2, CAST(105.30 AS Decimal(10, 2)), CAST(210.60 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (4, 1, 13, N'Abrams Books', N'One Hundred Years of Solitude', N'Juvenil', 2, CAST(101.75 AS Decimal(10, 2)), CAST(203.50 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (5, 1, 4, N'Puffin Books', N'The Great Gatsby', N'Autobiografía', 2, CAST(74.30 AS Decimal(10, 2)), CAST(148.60 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (6, 1, 15, N'Dorling Kindersley', N'The Brothers Karamazov', N'Biografía', 2, CAST(113.90 AS Decimal(10, 2)), CAST(227.80 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (7, 2, 12, N'Oxford University Press', N'Ulysses', N'Infantil', 2, CAST(97.40 AS Decimal(10, 2)), CAST(194.80 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (8, 2, 2, N'Doubleday', N'Brave New World', N'Ciencia Ficción', 2, CAST(89.90 AS Decimal(10, 2)), CAST(179.80 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (9, 2, 17, N'Macmillan Publishers', N'The Lord of the Rings', N'Arte', 2, CAST(105.30 AS Decimal(10, 2)), CAST(210.60 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (10, 2, 13, N'Abrams Books', N'One Hundred Years of Solitude', N'Juvenil', 2, CAST(101.75 AS Decimal(10, 2)), CAST(203.50 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (11, 2, 4, N'Puffin Books', N'The Great Gatsby', N'Autobiografía', 2, CAST(74.30 AS Decimal(10, 2)), CAST(148.60 AS Decimal(10, 2)))
INSERT [dbo].[DetalleVenta] ([idDetalleVenta], [idVenta], [idLibro], [editorialLibro], [tituloLibro], [generoLibro], [cantidad], [precio], [total]) VALUES (12, 2, 15, N'Dorling Kindersley', N'The Brothers Karamazov', N'Biografía', 2, CAST(113.90 AS Decimal(10, 2)), CAST(227.80 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[DetalleVenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Editorial] ON 

INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (1, N'Penguin Random House', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (2, N'HarperCollins', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (3, N'Macmillan Publishers', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (4, N'Simon & Schuster', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (5, N'Hachette Book Group', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (6, N'Bloomsbury', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (7, N'Oxford University Press', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (8, N'Pearson', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (9, N'Cambridge University Press', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (10, N'Scholastic', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (11, N'Wiley', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (12, N'Elsevier', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (13, N'SAGE Publications', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (14, N'McGraw-Hill Education', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (15, N'Doubleday', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (16, N'Little, Brown and Company', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (17, N'Pantheon Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (18, N'Kodansha', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (19, N'Springer', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (20, N'Vintage Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (21, N'Alfred A. Knopf', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (22, N'Tor Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (23, N'Bantam Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (24, N'Ballantine Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (25, N'Berkley Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (26, N'Chronicle Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (27, N'Dorling Kindersley', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (28, N'Farrar, Straus and Giroux', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (29, N'Hay House', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (30, N'John Wiley & Sons', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (31, N'Ace Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (32, N'Puffin Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (33, N'Quirk Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (34, N'Rodale Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (35, N'St. Martin Press', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (36, N'TarcherPerigee', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (37, N'Zondervan', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (38, N'Abrams Books', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (39, N'Baen Books', 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Editorial] ([idEditorial], [descripcion], [esActivo], [fechaRegistro]) VALUES (40, N'Candlewick Press', 0, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
SET IDENTITY_INSERT [dbo].[Editorial] OFF
GO
SET IDENTITY_INSERT [dbo].[Genero] ON 

INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (1, N'Ficción', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (2, N'No Ficción', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (3, N'Ciencia Ficción', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (4, N'Fantasía', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (5, N'Romance', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (6, N'Terror', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (7, N'Historico', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (8, N'Misterio', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (9, N'Biografía', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (10, N'Autobiografía', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (11, N'Poesía', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (12, N'Infantil', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (13, N'Juvenil', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (14, N'Autoayuda', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (15, N'Viajes', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (16, N'Ciencias Naturales', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (17, N'Filosofia', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (18, N'Religion', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (19, N'Arte', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Genero] ([idGenero], [descripcion], [esActivo], [fechaRegistro]) VALUES (20, N'Politica', 0, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
SET IDENTITY_INSERT [dbo].[Genero] OFF
GO
SET IDENTITY_INSERT [dbo].[Libro] ON 

INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (1, N'9780451524935', N'0451524934', N'1984', CAST(123.45 AS Decimal(10, 2)), 100, N'George Orwell', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F8e239d5159b34b13a1754ec2d4b24248.jpg?alt=media&token=98f67919-07b7-4176-b33a-b9b4bdbd4085', N'8e239d5159b34b13a1754ec2d4b24248.jpg', 12, 27, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (2, N'9780451524936', N'0451524935', N'Brave New World', CAST(89.90 AS Decimal(10, 2)), 96, N'Aldous Huxley', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Ffa0da145ca8345f8b5e39106d42c56aa.jpg?alt=media&token=9e0a4efd-8aac-4d69-b080-a45cae4c20e5', N'fa0da145ca8345f8b5e39106d42c56aa.jpg', 3, 15, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (3, N'9780451524937', N'0451524936', N'To Kill a Mockingbird', CAST(67.50 AS Decimal(10, 2)), 100, N'Harper Lee', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Fb68367e4b89f4ff3966db50d78e634cd.jpg?alt=media&token=99b2b503-ff0f-45a6-95e1-a4f116c8c8d1', N'b68367e4b89f4ff3966db50d78e634cd.jpg', 7, 21, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (4, N'9780451524938', N'0451524937', N'The Great Gatsby', CAST(74.30 AS Decimal(10, 2)), 96, N'F. Scott Fitzgerald', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F57cfc4da775e45bf98e31577d430aed4.jpg?alt=media&token=4c36225b-325a-4726-bf60-54407725a4b3', N'57cfc4da775e45bf98e31577d430aed4.jpg', 10, 32, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (5, N'9780451524939', N'0451524938', N'Moby Dick', CAST(95.20 AS Decimal(10, 2)), 100, N'Herman Melville', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Fef633f66ae1b448a95f315af5d79ff6a.jpg?alt=media&token=d528811e-49eb-4e88-9242-7e5966daa6d5', N'ef633f66ae1b448a95f315af5d79ff6a.jpg', 1, 8, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (6, N'9780451524940', N'0451524939', N'War and Peace', CAST(104.15 AS Decimal(10, 2)), 100, N'Leo Tolstoy', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Fed4e10d3b6994718a7bcc3d7fc13b8fb.webp?alt=media&token=ff0a730f-d555-4102-809e-5cb6e8fa2993', N'ed4e10d3b6994718a7bcc3d7fc13b8fb.webp', 14, 19, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (7, N'9780451524941', N'0451524940', N'Pride and Prejudice', CAST(63.10 AS Decimal(10, 2)), 100, N'Jane Austen', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F69fd295fabee43df91698d52beef0b2e.jpg?alt=media&token=65c9b299-fb43-4073-be20-483493b622f7', N'69fd295fabee43df91698d52beef0b2e.jpg', 2, 10, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (8, N'9780451524942', N'0451524941', N'The Catcher in the Rye', CAST(87.60 AS Decimal(10, 2)), 100, N'J.D. Salinger', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F74b86a5cb70f4fde9e74c6d6d7a9ae77.webp?alt=media&token=e54133e8-23f6-43f0-80b2-46c9c4044716', N'74b86a5cb70f4fde9e74c6d6d7a9ae77.webp', 6, 17, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (9, N'9780451524943', N'0451524942', N'The Odyssey', CAST(92.10 AS Decimal(10, 2)), 100, N'Homer', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F3e3cdc35268b416aae1c7938ad5f3175.jpg?alt=media&token=ba4d02cd-23f2-42de-9888-10b98bad0fe3', N'3e3cdc35268b416aae1c7938ad5f3175.jpg', 8, 24, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (10, N'9780451524944', N'0451524943', N'Crime and Punishment', CAST(99.80 AS Decimal(10, 2)), 100, N'Fyodor Dostoevsky', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F19db92fe53b2423f8939635c9289b1d8.jpg?alt=media&token=754750b2-e3a6-4437-922c-fd5ab64e8051', N'19db92fe53b2423f8939635c9289b1d8.jpg', 16, 29, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (11, N'9780451524945', N'0451524944', N'Don Quixote', CAST(110.00 AS Decimal(10, 2)), 100, N'Miguel de Cervantes', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F013d8ef2e59d42808c5920c237285fcc.jpg?alt=media&token=81b22a81-2a7d-4bfd-9eff-5ce9b49e3067', N'013d8ef2e59d42808c5920c237285fcc.jpg', 11, 33, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (12, N'9780451524946', N'0451524945', N'Ulysses', CAST(97.40 AS Decimal(10, 2)), 96, N'James Joyce', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F997a05cae8c1467ebf871533dd437ffd.jpg?alt=media&token=2122e1ba-e694-4bbe-9a07-eb4a51b6098f', N'997a05cae8c1467ebf871533dd437ffd.jpg', 12, 7, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (13, N'9780451524947', N'0451524946', N'One Hundred Years of Solitude', CAST(101.75 AS Decimal(10, 2)), 96, N'Gabriel Garcia Marquez', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F9cf69e5786154f2081a356a9df4b0001.jpg?alt=media&token=45e87550-e830-471d-887c-6e6292f5be83', N'9cf69e5786154f2081a356a9df4b0001.jpg', 13, 38, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (14, N'9780451524948', N'0451524947', N'The Divine Comedy', CAST(88.20 AS Decimal(10, 2)), 100, N'Dante Alighieri', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Fda371619f83944698711a7e0100fa6e0.jpg?alt=media&token=427b742b-3a79-488d-8393-306ffc01d804', N'da371619f83944698711a7e0100fa6e0.jpg', 4, 40, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (15, N'9780451524949', N'0451524948', N'The Brothers Karamazov', CAST(113.90 AS Decimal(10, 2)), 96, N'Fyodor Dostoevsky', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F9d82cd25ef36441483451c8c5e7fad66.jpg?alt=media&token=606beced-db81-482d-94c0-9005797a9b07', N'9d82cd25ef36441483451c8c5e7fad66.jpg', 9, 27, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (16, N'9780451524950', N'0451524949', N'Anna Karenina', CAST(119.60 AS Decimal(10, 2)), 100, N'Leo Tolstoy', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2Fb323e7343bb843afa7be31081205edf0.jpg?alt=media&token=abfcf47d-562e-4c74-bf3a-54709ae2361e', N'b323e7343bb843afa7be31081205edf0.jpg', 18, 23, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (17, N'9780451524951', N'0451524950', N'The Lord of the Rings', CAST(105.30 AS Decimal(10, 2)), 96, N'J.R.R. Tolkien', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F15ab3d40591e4a07b9d21f5acef9d355.jpg?alt=media&token=14a09093-51a4-4b71-b137-e4e29e1cbc7f', N'15ab3d40591e4a07b9d21f5acef9d355.jpg', 19, 3, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (18, N'9780451524952', N'0451524951', N'A Tale of Two Cities', CAST(73.90 AS Decimal(10, 2)), 100, N'Charles Dickens', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F53750d1363ff4c3e99e2b30027bc6b6f.jpg?alt=media&token=d6c9de55-98b4-4e40-a7e4-cf59d4be1cec', N'53750d1363ff4c3e99e2b30027bc6b6f.jpg', 5, 11, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (19, N'9780451524953', N'0451524952', N'The Little Prince', CAST(59.90 AS Decimal(10, 2)), 100, N'Antoine de Saint-Exup ry', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F2229157706b04920aecf09e6e85ba207.jpg?alt=media&token=9a064489-90c5-4cfb-9d69-528d0a7ffb65', N'2229157706b04920aecf09e6e85ba207.jpg', 17, 35, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
INSERT [dbo].[Libro] ([idLibro], [CodigoBarra], [isbn], [Titulo], [precio], [pendiente], [Autor], [urlImagen], [nombreImagen], [idGenero], [idEditorial], [esActivo], [fechaRegistro]) VALUES (20, N'9780451524954', N'0451524953', N'Wuthering Heights', CAST(96.20 AS Decimal(10, 2)), 100, N'Emily Bront ', N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_PRODUCTO%2F1a39d198067240fda896e9287afc9993.jpg?alt=media&token=73431f01-b2a5-4b83-8e19-ca261335a37d', N'1a39d198067240fda896e9287afc9993.jpg', 15, 14, 1, CAST(N'2023-11-06T20:07:29.123' AS DateTime))
SET IDENTITY_INSERT [dbo].[Libro] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (1, N'DashBoard', 1, N'fas fa-fw fa-tachometer-alt', N'DashBoard', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (2, N'Administracion', 2, N'fas fa-fw fa-cog', NULL, NULL, 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (3, N'Libros', 3, N'fas fa-fw fa-clipboard-list', NULL, NULL, 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (4, N'Pedidos', 4, N'fa fa-tasks', NULL, NULL, 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (5, N'Ventas', 5, N'fas fa-fw fa-tags', NULL, NULL, 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (6, N'Reportes', 6, N'fas fa-fw fa-chart-area', NULL, NULL, 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (7, N'Usuarios', 2, NULL, N'Usuario', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (8, N'Negocio', 2, NULL, N'Negocio', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (9, N'Generos', 3, NULL, N'Genero', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (10, N'Editoriales', 3, NULL, N'Editorial', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (11, N'Libros', 3, NULL, N'Libro', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (12, N'Nuevo Pedido', 4, NULL, N'Pedido', N'NuevoPedido', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (13, N'Historial Pedido', 4, NULL, N'Pedido', N'HistorialPedido', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (14, N'Nueva Venta', 5, NULL, N'Venta', N'NuevaVenta', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (15, N'Historial Venta', 5, NULL, N'Venta', N'HistorialVenta', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[Menu] ([idMenu], [descripcion], [idMenuPadre], [icono], [controlador], [paginaAccion], [esActivo], [fechaRegistro]) VALUES (16, N'Reporte de Ventas', 6, NULL, N'Reporte', N'Index', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
INSERT [dbo].[Negocio] ([idNegocio], [urlLogo], [nombreLogo], [numeroDocumento], [nombre], [correo], [direccion], [telefono], [porcentajeImpuesto], [simboloMoneda]) VALUES (1, N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_LOGO%2Fb4f6cd8e413243d3b83c33c5f107a1c3.webp?alt=media&token=c3286539-50d6-4a79-8bf9-f157aa9158ac', N'b4f6cd8e413243d3b83c33c5f107a1c3.webp', N'106069224', N'DistribuidoraBAE', N'weboperaciones@nbae.com', N'Av. Los Olivos 1684', N'987143576', CAST(18.00 AS Decimal(10, 2)), N's/.')
GO
SET IDENTITY_INSERT [dbo].[NumeroCorrelativo] ON 

INSERT [dbo].[NumeroCorrelativo] ([idNumeroCorrelativo], [ultimoNumero], [cantidadDigitos], [gestion], [fechaActualizacion]) VALUES (1, 2, 6, N'venta', CAST(N'2023-11-07T09:55:21.260' AS DateTime))
INSERT [dbo].[NumeroCorrelativo] ([idNumeroCorrelativo], [ultimoNumero], [cantidadDigitos], [gestion], [fechaActualizacion]) VALUES (2, 0, 6, N'Pedido', CAST(N'2023-11-06T20:07:29.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[NumeroCorrelativo] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([idRol], [descripcion], [esActivo], [fechaRegistro]) VALUES (1, N'Administrador', 1, CAST(N'2023-11-06T20:07:29.117' AS DateTime))
INSERT [dbo].[Rol] ([idRol], [descripcion], [esActivo], [fechaRegistro]) VALUES (2, N'Empleado', 1, CAST(N'2023-11-06T20:07:29.117' AS DateTime))
INSERT [dbo].[Rol] ([idRol], [descripcion], [esActivo], [fechaRegistro]) VALUES (3, N'Supervisor', 1, CAST(N'2023-11-06T20:07:29.117' AS DateTime))
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[RolMenu] ON 

INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (1, 1, 1, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (2, 1, 7, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (3, 1, 8, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (4, 1, 9, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (5, 1, 10, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (6, 1, 11, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (7, 1, 12, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (8, 1, 13, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (9, 1, 14, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (10, 1, 15, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (11, 1, 16, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (12, 2, 12, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (13, 2, 13, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (14, 2, 14, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (15, 2, 15, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (16, 3, 9, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (17, 3, 10, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (18, 3, 11, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (19, 3, 12, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (20, 3, 13, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (21, 3, 14, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
INSERT [dbo].[RolMenu] ([idRolMenu], [idRol], [idMenu], [esActivo], [fechaRegistro]) VALUES (22, 3, 15, 1, CAST(N'2023-11-06T20:07:29.130' AS DateTime))
SET IDENTITY_INSERT [dbo].[RolMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoDocumentoVenta] ON 

INSERT [dbo].[TipoDocumentoVenta] ([idTipoDocumentoVenta], [descripcion], [esActivo], [fechaRegistro]) VALUES (1, N'Boleta', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
INSERT [dbo].[TipoDocumentoVenta] ([idTipoDocumentoVenta], [descripcion], [esActivo], [fechaRegistro]) VALUES (2, N'Factura', 1, CAST(N'2023-11-06T20:07:29.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[TipoDocumentoVenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([idUsuario], [nombre], [correo], [telefono], [idRol], [urlFoto], [nombreFoto], [clave], [esActivo], [fechaRegistro]) VALUES (1, N'omarlujan', N'n00209455@upn.pe', N'909090', 1, N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_USUARIO%2Faf64a6e71cec4158a8f42a915ae1876d.jpg?alt=media&token=56087830-68f3-4582-82d6-7acd87b69f20', N'af64a6e71cec4158a8f42a915ae1876d.jpg', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, CAST(N'2023-11-06T20:07:29.120' AS DateTime))
INSERT [dbo].[Usuario] ([idUsuario], [nombre], [correo], [telefono], [idRol], [urlFoto], [nombreFoto], [clave], [esActivo], [fechaRegistro]) VALUES (3, N'omarlujan', N'omarlujan.h@gmail.com', N'912565908', 3, N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_USUARIO%2F94fd886b28464b1b9950e47c3f25f594.jpg?alt=media&token=1ef42ddf-b814-4cde-9c0b-a8d9a3fc8f04', N'94fd886b28464b1b9950e47c3f25f594.jpg', N'8eef4272afd76c3c721f94e108b7bab939ed11fc4a463dde1c50f7aed64591cf', 1, CAST(N'2023-11-06T23:20:13.910' AS DateTime))
INSERT [dbo].[Usuario] ([idUsuario], [nombre], [correo], [telefono], [idRol], [urlFoto], [nombreFoto], [clave], [esActivo], [fechaRegistro]) VALUES (4, N'supervisor', N'koltelardi@gufum.com', N'934567745', 3, N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_USUARIO%2F0d047759f13a477a8b370920190295bd.gif?alt=media&token=02d51756-f34f-45cc-a713-e70236cd37c4', N'0d047759f13a477a8b370920190295bd.gif', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, CAST(N'2023-11-06T23:25:28.093' AS DateTime))
INSERT [dbo].[Usuario] ([idUsuario], [nombre], [correo], [telefono], [idRol], [urlFoto], [nombreFoto], [clave], [esActivo], [fechaRegistro]) VALUES (5, N'Empleado', N'tilmagayde@gufum.com', N'934577745', 2, N'https://firebasestorage.googleapis.com/v0/b/ventasasp-8d05c.appspot.com/o/IMAGENES_USUARIO%2F5227c39fbf3d48c2a53838214a9e3ede.gif?alt=media&token=ba8828ee-3d86-4ad9-b36f-a53bbce4b1d0', N'5227c39fbf3d48c2a53838214a9e3ede.gif', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, CAST(N'2023-11-06T23:27:36.430' AS DateTime))
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Venta] ON 

INSERT [dbo].[Venta] ([idVenta], [numeroVenta], [idTipoDocumentoVenta], [idUsuario], [documentoCliente], [nombreCliente], [subTotal], [impuestoTotal], [Total], [fechaRegistro]) VALUES (1, N'000001', 1, 1, N'20141878477', N'UNIVERSIDAD PRIVADA ANTENOR ORREGO', CAST(987.37 AS Decimal(10, 2)), CAST(177.73 AS Decimal(10, 2)), CAST(1165.10 AS Decimal(10, 2)), CAST(N'2023-11-06T23:42:14.040' AS DateTime))
INSERT [dbo].[Venta] ([idVenta], [numeroVenta], [idTipoDocumentoVenta], [idUsuario], [documentoCliente], [nombreCliente], [subTotal], [impuestoTotal], [Total], [fechaRegistro]) VALUES (2, N'000002', 1, 1, N'20141878477', N'UNIVERSIDAD PRIVADA ANTENOR ORREGO', CAST(987.37 AS Decimal(10, 2)), CAST(177.73 AS Decimal(10, 2)), CAST(1165.10 AS Decimal(10, 2)), CAST(N'2023-11-07T09:55:21.350' AS DateTime))
SET IDENTITY_INSERT [dbo].[Venta] OFF
GO
ALTER TABLE [dbo].[Editorial] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Genero] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Libro] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Menu] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[RolMenu] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[TipoDocumentoVenta] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[Venta] ADD  DEFAULT (getdate()) FOR [fechaRegistro]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD FOREIGN KEY([idPedido])
REFERENCES [dbo].[Pedido] ([idPedido])
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD FOREIGN KEY([idVenta])
REFERENCES [dbo].[Venta] ([idVenta])
GO
ALTER TABLE [dbo].[Libro]  WITH CHECK ADD FOREIGN KEY([idEditorial])
REFERENCES [dbo].[Editorial] ([idEditorial])
GO
ALTER TABLE [dbo].[Libro]  WITH CHECK ADD FOREIGN KEY([idGenero])
REFERENCES [dbo].[Genero] ([idGenero])
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD FOREIGN KEY([idMenuPadre])
REFERENCES [dbo].[Menu] ([idMenu])
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD FOREIGN KEY([idMenu])
REFERENCES [dbo].[Menu] ([idMenu])
GO
ALTER TABLE [dbo].[RolMenu]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([idTipoDocumentoVenta])
REFERENCES [dbo].[TipoDocumentoVenta] ([idTipoDocumentoVenta])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([idUsuario])
GO
USE [master]
GO
ALTER DATABASE [BAE] SET  READ_WRITE 
GO
