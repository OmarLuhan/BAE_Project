describe('CP07 - Registrar ventas', () => {
    beforeEach(() => {
        cy.login()
    });
   it('CP07-01 - Ingresar al modulo nueva venta', () => {
    cy.inNewVenta("Administrador")
   });
    it('CP07-02 - Registrar una venta', () => {
      cy.inNewVenta("Administrador")
        cy.get("#btnTerminarVenta").click()
        cy.get("#toast-container").should("contain", 'no ha ingresado el nombre de ningun cliente')
        cy.get("#txtDocumentoCliente").type("20141878477")
        cy.get("#btnBuscar").click()
        cy.addLibro("Ulysses","2")
        cy.addLibro("Brave New World","2")
        cy.addLibro("The Lord of the Rings","2")
        cy.addLibro("One Hundred Years of Solitude","2")
        cy.addLibro("The Great Gatsby","2")
        cy.addLibro("The Brothers Karamazov","2")
        cy.wait(500)
        cy.get("#btnTerminarVenta").click()
        cy.wait(500)
        cy.contains("Registrado!").should("contain","Registrado!")
        cy.get(".text-muted").should("contain","Numero venta: 00")
        cy.contains("OK").click()
    });
    it('CP07-03 -VerificarVenta-ImprimirRecibo', () => {
        cy.inHistorialVenta("Administrador")
        cy.get("#txtFechaInicio").type("01/01/2023")
        cy.get("#txtFechaFin").type("12/12/2023")
        cy.get("#btnBuscar").click()
        cy.get(".table").find("tbody").find("tr").last().find("button").click()
        cy.get(".modal-header").find("h6").should("contain","Detalle Venta")
        cy.wait(1000)
        cy.get(".btn-danger").click()
    });
});
