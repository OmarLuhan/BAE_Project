
describe('CP02 - Recuperar contraseña', () => {
    beforeEach(()=>{
        cy.visit("http://localhost:5000/")
        cy.get(".h4").should("have.text", "B.A.E Distribuidora")
    });
    it('CP00-00 - Recuperar contraseña incorrecto', () => {
        cy.get(".text-center .small").click()
        cy.get(".h4").should("have.text", "¿Olvidó su contraseña?")
        cy.get(".alert-warning").should("contain", "Ingrese su correo electrónico")
        cy.get("#Correo").type("admin@gmail")
        cy.get(".btn-primary").click()
        cy.get(".alert-danger").should("contain", "No encontramos ningun usuario asociado al correo")
    });
    it('CP00-00 - Recuperar contraseña correcto', () => {
        cy.get(".text-center .small").click()
        cy.get(".h4").should("have.text", "¿Olvidó su contraseña?")
        cy.get(".alert-warning").should("contain", "Ingrese su correo electrónico")
        cy.get("#Correo").type("omarlujan.h@gmail.com")
        cy.get(".btn-primary").click()
        cy.get(".alert-success").should("contain", "Se ha enviado un correo con las instrucciones para restablecer su clave")
    });
});