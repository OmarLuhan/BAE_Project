
--_______________________________ INSERTAR ROLES ________________________________
insert into rol(descripcion,esActivo) values
('Administrador',1),
('Empleado',1),
('Supervisor',1)


--________________________________ INSERTAR USUARIO ________________________________
--SELECT * FROM Usuario
----clave : 123
insert into Usuario(nombre,correo,telefono,idRol,urlFoto,nombreFoto,clave,esActivo) values
('omarlujan','syadmin@bae.com','909090',1,'','','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3',1)

--________________________________ RECURSOS DE FIREBASE_STORAGE Y CORREO ________________________________
--(AQUI DEBES INCLUIR TUS PROPIAS CLAVES Y CRENDENCIALES)

insert into Configuracion(recurso,propiedad,valor) values
('FireBase_Storage','email','emailautorizado@gmail.com'),
('FireBase_Storage','clave','usuario123456789'),
('FireBase_Storage','ruta','ventasasp-6574.appspot.com'),
('FireBase_Storage','api_key','AIzaSyDtIH3_BADYTwIBtcDm-UIkqI9ZFA'),
('FireBase_Storage','carpeta_usuario','IMAGENES_USUARIO'),
('FireBase_Storage','carpeta_producto','IMAGENES_PRODUCTO'),
('FireBase_Storage','carpeta_logo','IMAGENES_LOGO')

insert into Configuracion(recurso,propiedad,valor) values
('Servicio_Correo','correo','serviciocorreo@gmail.com'),
('Servicio_Correo','clave','cpznvfbofjbrvgxx'),
('Servicio_Correo','alias','alias'),
('Servicio_Correo','host','smtp.gmail.com'),
('Servicio_Correo','puerto','587')


--________________________________ INSERTAR NEGOCIO ________________________________
--select * from Negocio

insert into Negocio(idNegocio,urlLogo,nombreLogo,numeroDocumento,nombre,correo,direccion,telefono,porcentajeImpuesto,simboloMoneda)
values(1,'','','','','','','',0,'')


--________________________________ INSERTAR G NEROS ________________________________

INSERT INTO Genero (descripcion, esActivo) VALUES ('Ficción', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('No Ficción', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Ciencia Ficción', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Fantasía', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Romance', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Terror', 0);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Historico', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Misterio', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Biografía', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Autobiografía', 0);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Poesía', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Infantil', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Juvenil', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Autoayuda', 0);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Viajes', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Ciencias Naturales', 0);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Filosof a', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Religi n', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Arte', 1);
INSERT INTO Genero (descripcion, esActivo) VALUES ('Politica', 0);

--________________________________ INSERTAR EDITORIALES ________________________________
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Penguin Random House', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('HarperCollins', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Macmillan Publishers', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Simon & Schuster', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Hachette Book Group', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Bloomsbury', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Oxford University Press', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Pearson', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Cambridge University Press', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Scholastic', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Wiley', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Elsevier', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('SAGE Publications', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('McGraw-Hill Education', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Doubleday', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Little, Brown and Company', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Pantheon Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Kodansha', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Springer', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Vintage Books', 0);

INSERT INTO Editorial (descripcion, esActivo) VALUES ('Alfred A. Knopf', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Tor Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Bantam Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Ballantine Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Berkley Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Chronicle Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Dorling Kindersley', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Farrar, Straus and Giroux', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Hay House', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('John Wiley & Sons', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Ace Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Puffin Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Quirk Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Rodale Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('St. Martin Press', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('TarcherPerigee', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Zondervan', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Abrams Books', 0);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Baen Books', 1);
INSERT INTO Editorial (descripcion, esActivo) VALUES ('Candlewick Press', 0);
--_______________________________INSERTAR TIENDAS_____________________________
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Amazon', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('eBay', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Etsy', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('AliExpress', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Walmart', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Target Online', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Newegg', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Best Buy Online', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Shopify Stores', 1);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('MercadoLibre', 1);

INSERT INTO Tienda (descripcion, esActivo) VALUES ('Zalando', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('ASOS', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Rakuten', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Flipkart', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Snapdeal', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Myntra', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('JD', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Taobao', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Tmall', 0);
INSERT INTO Tienda (descripcion, esActivo) VALUES ('Wayfair', 0);

--________________________________ INSERTAR G NEROS ________________________________
insert into Libro
(CodigoBarra, isbn, Titulo, precio, stock, Autor, idGenero, idEditorial, esActivo)
values 
('9780451524935', '0451524934', '1984', 123.45, 0, 'George Orwell', 12, 27, 1)
insert into Libro
(CodigoBarra, isbn, Titulo, precio, stock, Autor, idGenero, idEditorial, esActivo)
values 
('9780451524936', '0451524935', 'Brave New World', 89.90, 0, 'Aldous Huxley', 3, 15, 1),
('9780451524937', '0451524936', 'To Kill a Mockingbird', 67.50, 0, 'Harper Lee', 7, 21, 1),
('9780451524938', '0451524937', 'The Great Gatsby', 74.30, 0, 'F. Scott Fitzgerald', 10, 32, 1),
('9780451524939', '0451524938', 'Moby Dick', 95.20, 0, 'Herman Melville', 1, 8, 1),
('9780451524940', '0451524939', 'War and Peace', 104.15, 0, 'Leo Tolstoy', 14, 19, 1),
('9780451524941', '0451524940', 'Pride and Prejudice', 63.10, 0, 'Jane Austen', 2, 10, 1),
('9780451524942', '0451524941', 'The Catcher in the Rye', 87.60, 0, 'J.D. Salinger', 6, 17, 1),
('9780451524943', '0451524942', 'The Odyssey', 92.10, 0, 'Homer', 8, 24, 1),
('9780451524944', '0451524943', 'Crime and Punishment', 99.80, 0, 'Fyodor Dostoevsky', 16, 29, 1),
('9780451524945', '0451524944', 'Don Quixote', 110.00, 0, 'Miguel de Cervantes', 11, 33, 1),
('9780451524946', '0451524945', 'Ulysses', 97.40, 0, 'James Joyce', 12, 7, 1),
('9780451524947', '0451524946', 'One Hundred Years of Solitude', 101.75, 0, 'Gabriel Garcia Marquez', 13, 38, 1),
('9780451524948', '0451524947', 'The Divine Comedy', 88.20, 0, 'Dante Alighieri', 4, 40, 1),
('9780451524949', '0451524948', 'The Brothers Karamazov', 113.90, 0, 'Fyodor Dostoevsky', 9, 27, 1),
('9780451524950', '0451524949', 'Anna Karenina', 119.60, 0, 'Leo Tolstoy', 18, 23, 1),
('9780451524951', '0451524950', 'The Lord of the Rings', 105.30, 0, 'J.R.R. Tolkien', 19, 3, 1),
('9780451524952', '0451524951', 'A Tale of Two Cities', 73.90, 0, 'Charles Dickens', 5, 11, 1),
('9780451524953', '0451524952', 'The Little Prince', 59.90, 0, 'Antoine de Saint-Exup ry', 17, 35, 1),
('9780451524954', '0451524953', 'Wuthering Heights', 96.20, 0, 'Emily Bront ', 15, 14, 1);


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
('Historial Pedido',4,'Pedido','HistorialPedido',1),
('Tiendas E',4,'Tienda','Index',1)

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
(1,16,1),
(1,17,1)

--*Empleado
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(2,12,1),
(2,13,1),
(2,14,1),
(2,15,1),
(2,16,1)


--*Supervisor
INSERT INTO RolMenu(idRol,idMenu,esActivo) values
(3,9,1),
(3,10,1),
(3,11,1),
(3,12,1),
(3,13,1),
(3,14,1),
(3,15,1),
(3,16,1)