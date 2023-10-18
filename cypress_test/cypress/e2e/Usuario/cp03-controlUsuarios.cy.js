
describe('CP03 - Control de Usuarios', () => {
    beforeEach(()=>{
    cy.login();
    })
    it('Listar usuarios', () => {
        cy.get("#accordionSidebar .nav-link").eq(1).click()
        cy.get("#menucollapse1 .bg-white").find('a').first().should("contain", "Usuarios")
        cy.get("#menucollapse1 .bg-white").find('a').last().should("contain", "Negocio")
        cy.get("#menucollapse1 .bg-white").find('a').first().click()
        cy.get(".card-header").find('h6').should("have.text", "Lista de Usuarios")
    });
});