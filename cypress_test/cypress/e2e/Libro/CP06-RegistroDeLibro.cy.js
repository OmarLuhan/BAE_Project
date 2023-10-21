describe('CP08-RegistroDeLibro', () => {
    beforeEach(() => {
        cy.login();
        cy.inLibro("Administrador");
    });
   it('CP06-01 - Lista de Libros', () => {});
    it('CP06-02 - Registro de Libro', () => {
        cy.get('#btnNuevo').click();
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Codigo Barra"')
        cy.get("#txtCodigoBarra").type("2010201010");
        cy.get("#btnGuardar").click();
        cy.get("#txtIsbn").type("2010201010");
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Isbn"')
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Titulo"')
        cy.get("#txtTitulo").type("TituloTest");
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Autor"')
        cy.get("#txtAutor").type("AutorTest");
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe seleccionar una Editorial')
        cy.get("#cboEditorial").select("15");
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe seleccionar un Genero')
        cy.get("#cboGenero").select("19");
        cy.get("#txtPendiente").clear().type("Nan");
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'El campo pendientes de entrega debe ser un numero')
        cy.get("#txtPendiente").clear().type("0");
        cy.get("#txtPrecio").clear().type("Nan");
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'El campo precio debe ser un numero')
        cy.get("#txtPrecio").clear().type("0");
        cy.get("#txtImagen").selectFile('img/librotest.jpeg');
        cy.get("#btnGuardar").click();

        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","Libro creado correctamente")
        cy.contains("OK").click()

    });
    it('CP06-03 - Actualizar Libro', () => {
        cy.get(".dtr-control").first().click()
        cy.get(".btn-editar").first().click({force: true})
        cy.get("#txtCodigoBarra").clear().type("10101010");
        cy.get("#txtIsbn").clear().type("10101010");
        cy.get("#txtTitulo").clear().type("TituloTestUpdate");
        cy.get("#txtAutor").clear().type("AutorTestUpdate");
        cy.get("#cboEditorial").select("5");
        cy.get("#cboGenero").select("9");
        cy.get("#txtImagen").selectFile('img/libroupdate.jpeg');
        cy.get("#btnGuardar").click();
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","El libro fue modificado")
        cy.contains("OK").click()
    });
     it('CP06-04 - Eliminar Libro', () => {
        cy.get(".dtr-control").first().click()
        cy.get(".btn-eliminar").first().click({force: true})
        cy.wait(1000)
        cy.get(".confirm").click()
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","El libro fue Eliminado")
        cy.contains("OK").click()
     });
      it('CP06-05 - Buscar Libro', () => {
        cy.get('[type="search"]').type("Brave")
        cy.get("#tbdata_info").should("contain", "Mostrando 1 a 1 de 1 registros")
      });
});