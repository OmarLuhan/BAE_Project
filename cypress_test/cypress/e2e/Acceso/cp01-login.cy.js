

describe('CP01 - Login', () => {
    beforeEach(()=>{
        cy.visit("http://localhost:5000/")
        cy.get(".h4").should("have.text", "B.A.E Distribuidora")
       })
       it('CP01-01 - Login incorrecto', () => {
        cy.get("#Correo").type("admin@gmail")
        cy.get("#Clave").type("admin588382")
        cy.get(".btn").click()
        cy.get(".alert-danger").should("contain", "No se encontraron Coincidencias")
       });
       it('CP01-02 - Login correcto', () => {
        cy.get("#Correo").type("n00209455@upn.pe")
        cy.get("#Clave").type("123")
        cy.get(".btn").click()
        cy.get(".display-4").should("have.text", "Bienvenido a BAE")
       })
});