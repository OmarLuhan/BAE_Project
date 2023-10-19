
describe('CP03 - Control de Usuarios', () => {
    beforeEach(()=>{
    cy.login();
    })
    it('Listar usuarios', () => {
        listar_usuario()
    });
    it('Crear usuario', () => {
        listar_usuario()
        cy.get("#btnNuevo").click()
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Nombre"')
        cy.get("#txtNombre").type("Jazmin")
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Correo"')
        cy.get("#txtCorreo").type("jazper0208@gmail.com")
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Telefono"')
        cy.get("#txtTelefono").type("04415673")
        cy.wait(1000)
        cy.get("#cboRol").select("Administrador")
        cy.wait(1000)
        cy.get("#txtFoto").selectFile('anim/gif.gif')
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get('.showSweetAlert', { timeout: 50000 }).should('exist');
        cy.wait(1000)
        cy.contains("Listo!").should("contain","Listo!")
        cy.contains("OK").click()
    });
    it("Editar usuario", () => {
        listar_usuario()
        cy.get(".dtr-control").first().click()
        cy.get(".btn-editar").first().click({force: true})
        cy.wait(1000)
        cy.get("#txtNombre").clear().type("Jazmin perez")
        cy.wait(1000)
        cy.get("#txtTelefono").clear().type("91248493")
        cy.wait(1000)
        cy.get("#cboRol").select("Empleado")
        cy.wait(1000)
        cy.get("#cboEstado").select("No Activo")
        cy.wait(1000)
        cy.get("#txtFoto").selectFile('anim/gifupdate.gif')
        cy.get("#btnGuardar").click() 
        cy.get('.showSweetAlert', { timeout: 50000 }).should('exist');
        cy.wait(1000)
        cy.contains("Listo!").should("contain","Listo!")
        cy.contains("OK").click()
    });
    it('Eliminar usuario', () => {
        listar_usuario()
        cy.get(".dtr-control").first().click()
        cy.get(".btn-eliminar").first().click({force: true})
        cy.wait(1000)
        cy.get(".cancel").click()
        cy.wait(1000)
        cy.get(".btn-eliminar").first().click({force: true})
        cy.wait(1000)
        cy.get(".confirm").click()
        cy.wait(1000)
        cy.contains("Listo!").should("contain","Listo!")
        cy.contains("OK").click()
    });
});

//Funciones
//Funcion para listar usuarios
function listar_usuario(){
    cy.get("#accordionSidebar .nav-link").eq(1).click()
    cy.get("#menucollapse1 .bg-white").find('a').first().should("contain", "Usuarios")
    cy.get("#menucollapse1 .bg-white").find('a').last().should("contain", "Negocio")
    cy.get("#menucollapse1 .bg-white").find('a').first().click()
    cy.get(".card-header").find('h6').should("have.text", "Lista de Usuarios")
}