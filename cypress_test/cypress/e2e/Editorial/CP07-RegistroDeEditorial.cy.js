describe('CP07 Registro de Editorial', () => {
    beforeEach(()=>{
        cy.login();
        cy.inEditorial("Administrador");
    });
   it('CP07-01 - Ingresar a Registro de Editorial', () => {});
    it("CP07-02 - Registrar Editorial", () => {
        cy.get("#btnNuevo").click();
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo "Descripción"')
        cy.get("#txtDescripcion").type("Editorial de prueba");
        cy.get("#btnGuardar").click();
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","Editorial creada")
        cy.contains("OK").click()
    });
     it("CP07-03 - Actualizar Editorial", () => {
        cy.get(".fa-pencil-alt").first().click();
        cy.get("#txtDescripcion").clear();
        cy.get("#btnGuardar").click();
        cy.get("#toast-container").should("contain", 'Debe completar el campo "Descripción"')
        cy.get("#txtDescripcion").type("Update Editorial");
        cy.get("#btnGuardar").click();
        cy.contains("Listo!").should("contain","Listo!")
        cy.get(".text-muted").should("contain","La Editorial ha sido modificada")
        cy.contains("OK").click()
     });
    it("CP07-04 - Eliminar Editorial", () => {
        cy.get(".fa-trash-alt").first().click();
        cy.wait(1000)
        cy.get(".confirm").click()
        cy.wait(1000)
        cy.contains("Listo!").should("contain","Listo!")
        cy.contains("OK").click()
    });
});
