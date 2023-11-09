
describe('CP01 - Manejo de Roles', () => {
    beforeEach(() => {
        cy.visit("http://localhost:5000/")
        cy.get(".h4").should("have.text", "B.A.E Distribuidora")
    });
    it('CP01-01 - Ingresar con usuario administrador', () => {
        cy.login();
        cy.get("#accordionSidebar").should("be.visible")
        cy.get("#accordionSidebar").find(".nav-item").should("have.length", 6)
        cy.inDashboard();
        cy.inNegocio();
        cy.inUsuario();
        cy.inGenero("Administrador");
        cy.inEditorial("Administrador");
        cy.inLibro("Administrador");
        cy.inNewVenta("Administrador");
        cy.inHistorialVenta("Administrador");
        cy.get(".img-profile").click()
        cy.get(".dropdown-item").first().click()
        cy.get("#txtRol").should("have.value", "Administrador")
    });
   it('CP01-02 - Ingresar con usuario supervisor', () => {
    cy.get("#Correo").type("koltelardi@gufum.com")
    cy.get("#Clave").type("123")
    cy.get(".btn").click()
    cy.get(".display-4").should("have.text", "Bienvenido a BAE")
    cy.get("#accordionSidebar").should("be.visible")
    cy.get("#accordionSidebar").find(".nav-item").should("have.length", 3)

    cy.inGenero("Supervisor");
    cy.inEditorial("Supervisor");
    cy.inLibro("Supervisor");
    cy.inNewVenta("Supervisor");
    cy.inHistorialVenta("Supervisor");
    
    cy.visit("http://localhost:5000/Dashboard/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Usuario/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Negocio/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Reporte/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")

    cy.get(".img-profile").click()
    cy.get(".dropdown-item").first().click()
    cy.get("#txtRol").should("have.value", "Supervisor")
     });
it('CP01-03 - Ingresar con usuario empleado', () => {
    cy.get("#Correo").type("tilmagayde@gufum.com")
    cy.get("#Clave").type("123")
    cy.get(".btn").click()
    cy.get(".display-4").should("have.text", "Bienvenido a BAE")
    cy.get("#accordionSidebar").should("be.visible")
    cy.get("#accordionSidebar").find(".nav-item").should("have.length", 2)

    cy.inNewVenta("Empleado");
    cy.inHistorialVenta("Empleado");
    

    cy.visit("http://localhost:5000/Dashboard/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Usuario/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Negocio/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Genero/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Editorial/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Libro/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")
    cy.visit("http://localhost:5000/Reporte/Index")
    cy.get(".display-4").should("have.text", "No Autorizado")


    cy.get(".img-profile").click()
    cy.get(".dropdown-item").first().click()
    cy.get("#txtRol").should("have.value", "Empleado")
});
});