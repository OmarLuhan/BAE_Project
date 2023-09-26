
--_______________________________ INSERTAR ROLES ________________________________
insert into rol(descripcion,esActivo) values
('Administrador',1),
('Empleado',1),
('Supervisor',1)


--________________________________ INSERTAR USUARIO ________________________________
--SELECT * FROM Usuario
----clave : 123
insert into Usuario(nombre,correo,telefono,idRol,urlFoto,nombreFoto,clave,esActivo) values
('omarlujan','n00209455@upn.pe','909090',1,'','','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1)

--________________________________ RECURSOS DE FIREBASE_STORAGE Y CORREO ________________________________
--(AQUI DEBES INCLUIR TUS PROPIAS CLAVES Y CRENDENCIALES)

insert into Configuracion(recurso,propiedad,valor) values
('FireBase_Storage','email','v43728837@gmail.com'),
('FireBase_Storage','clave','usuario123456789'),
('FireBase_Storage','ruta','ventasasp-8d05c.appspot.com'),
('FireBase_Storage','api_key','AIzaSyDtIH3_A6mt9YAYTwIBt9Z4D-UIkqI9ZFA'),
('FireBase_Storage','carpeta_usuario','IMAGENES_USUARIO'),
('FireBase_Storage','carpeta_producto','IMAGENES_PRODUCTO'),
('FireBase_Storage','carpeta_logo','IMAGENES_LOGO')

insert into Configuracion(recurso,propiedad,valor) values
('Servicio_Correo','correo','v43728837@gmail.com'),
('Servicio_Correo','clave','cpznvfbofjbrvxxx'),
('Servicio_Correo','alias','Store.com'),
('Servicio_Correo','host','smtp.gmail.com'),
('Servicio_Correo','puerto','587')


--________________________________ INSERTAR NEGOCIO ________________________________
--select * from Negocio

insert into Negocio(idNegocio,urlLogo,nombreLogo,numeroDocumento,nombre,correo,direccion,telefono,porcentajeImpuesto,simboloMoneda)
values(1,'','','','','','','',0,'')


--________________________________ INSERTAR CATEGORIAS ________________________________


--________________________________ INSERTAR TIPO DOCUMENTO VENTA ________________________________

--select * from TipoDocumentoVenta

insert into TipoDocumentoVenta(descripcion,esActivo) values
('Boleta',1),
('Factura',1)


--________________________________ INSERTAR NUMERO CORRELATIVO ________________________________
--select * from NumeroCorrelativo
--000001
insert into NumeroCorrelativo(ultimoNumero,cantidadDigitos,gestion,fechaActualizacion) values
(0,6,'venta',getdate()),
(0,6,'Pedido',getdate())


--________________________________ INSERTAR MENUS ________________________________
--select * from Menu

--*menu padre
insert into Menu(descripcion,icono,controlador,paginaAccion,esActivo) values
('DashBoard','fas fa-fw fa-tachometer-alt','DashBoard','Index',1)

insert into Menu(descripcion,icono,esActivo) values
('Administracion','fas fa-fw fa-cog',1),
('Libros','fas fa-fw fa-clipboard-list',1),
('Pedidos','fa fa-tasks',1),
('Ventas','fas fa-fw fa-tags',1),
('Reportes','fas fa-fw fa-chart-area',1)


--*menu hijos Administracion
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Usuarios',2,'Usuario','Index',1),
('Negocio',2,'Negocio','Index',1)


--*menu hijos - Inventario
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Generos',3,'Genero','Index',1),
('Editoriales',3,'Editorial','Index',1),
('Libros',3,'Libro','Index',1)

--*menu hijos - Pedidos
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Nuevo Pedido',4,'Pedido','NuevoPedido',1),
('Historial Pedido',4,'Pedido','HistorialPedido',1)

--*menu hijos - Ventas
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Nueva Venta',5,'Venta','NuevaVenta',1),
('Historial Venta',5,'Venta','HistorialVenta',1)

--*menu hijos - Reportes
insert into Menu(descripcion,idMenuPadre, controlador,paginaAccion,esActivo) values
('Reporte de Ventas',6,'Reporte','Index',1)

UPDATE Menu SET idMenuPadre = idMenu where idMenuPadre is null


--________________________________ INSERTAR ROL MENU ________________________________
select * from Menu
select * from RolMenu
SELECT * FROM ROL

--*administrador
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(1,1,1),
(1,7,1),
(1,8,1),
(1,9,1),
(1,10,1),
(1,11,1),
(1,12,1),
(1,13,1),
(1,14,1),
(1,15,1),
(1,16,1)

--*Empleado
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(2,12,1),
(2,13,1),
(2,14,1),
(2,15,1)


--*Supervisor
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(3,9,1),
(3,10,1),
(3,11,1),
(3,12,1),
(3,13,1),
(3,14,1),
(3,15,1)