describe('CP03 - DatosDelNegocio', () => {
    beforeEach(()=>{
    cy.login();
    cy.inNegocio();
    });
    it('CP03-01 - Ingresar a Datos del Negocio', () => {
    });
    it('CP03-02 - Actualizar Datos del Negocio', () => {
    cy.get("#txtRazonSocial").clear().type("New-BAE Distribuidora")
    cy.get("#txtCorreo").clear().type("newweboperaciones@nbae.com")
    cy.get("#txtTelefono").clear().type("987123576")
    cy.get("#txtDireccion").clear().type("new-Av. Los Olivos 1684")
    cy.get("#txtImpuesto").clear().type("20")
    cy.get("#txtSimboloMoneda").clear().type("$.")
    cy.get("#txtLogo").selectFile('img/logoupdate.jpg')
    cy.get("#btnGuardarCambios").click()
    cy.inDashboard();
    cy.inNegocio();
    cy.get("#txtRazonSocial").clear().type("BAE Distribuidora")
    cy.get("#txtCorreo").clear().type("weboperaciones@nbae.com")
    cy.get("#txtTelefono").clear().type("987143576")
    cy.get("#txtDireccion").clear().type("Av. Los Olivos 1684")
    cy.get("#txtImpuesto").clear().type("18")
    cy.get("#txtSimboloMoneda").clear().type("s/.")
    cy.get("#txtLogo").selectFile('img/logo.webp')
    cy.get("#btnGuardarCambios").click()
    });
});