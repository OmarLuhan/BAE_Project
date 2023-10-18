

describe('CP01 - Login', () => {
    beforeEach(()=>{
        cy.visit("http://localhost:5000/")
        cy.get(".h4").should("have.text", "B.A.E Distribuidora")
       })
       it('Login incorrecto', () => {
        cy.get("#Correo").type("admin@gmail")
        cy.get("#Clave").type("admin588382")
        cy.get(".btn").click()
        cy.get(".alert-danger").should("contain", "No se encontraron Coincidencias")
       });
       it('Login correcto', () => {
        cy.get("#Correo").type("n00209455@upn.pe")
        cy.get("#Clave").type("123")
        cy.get(".btn").click()
       cy.get(".h3").should("have.text", "Dashboard")
       })
});