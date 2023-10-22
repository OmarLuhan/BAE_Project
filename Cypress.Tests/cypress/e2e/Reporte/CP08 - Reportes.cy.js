describe('CP08 - Reportes', () => {
    beforeEach(()=>{
        cy.login()
        cy.inReporte()
    })
    it('CP08-01 - Reporte de ventas', () => {})
    it('CP08-02 - Reporte de ventas-filtrar', () => {
        cy.get('#txtFechaInicio').type('01/01/2023')
        cy.get('#txtFechaFin').type('12/12/2023')
        cy.get('#btnBuscar').click()
        cy.wait(500)
        cy.get("#tbdata").find('tbody').find('tr').should('have.length.greaterThan', 0);
    })
});