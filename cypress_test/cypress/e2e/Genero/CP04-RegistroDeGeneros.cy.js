describe('CP04 - Registro de Generos', () => {
    beforeEach(()=>{
        cy.login();
        cy.inGenero("Administrador");
    });
    it('CP04-01 - Ingresar a Registro de Generos', () => {});
    it('CP04-02 - Registrar Genero', () => {
        cy.get("#btnNuevo").click();
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo "Descripción"')
        cy.get("#txtDescripcion").type("Genero de prueba");
        cy.get("#btnGuardar").click();
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","Genero creado")
        cy.contains("OK").click()
    });
     it('CP04-03 - Actualizar Genero', () => {
        cy.get(".fa-pencil-alt").first().click();
        cy.get("#txtDescripcion").clear();
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo "Descripción"')
        cy.get("#txtDescripcion").type("Update Genero");
        cy.get("#btnGuardar").click();
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","El Genero ha sido modificado")
        cy.contains("OK").click()
     });
     it('CP04-04 - Eliminar Genero', () => {
        cy.get(".fa-trash-alt").first().click();
        cy.wait(1000)
        cy.get(".confirm").click()
        cy.wait(1000)
        cy.contains("Listo!").should("contain","Listo!")
        cy.contains("OK").click()
     });
});