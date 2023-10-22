
describe('CP02 - Control de Usuarios', () => {
    beforeEach(()=>{
    cy.login();
    cy.inUsuario();
    })
    it('CP02-01 - Listar usuarios', () => {
    });
    it('CP02-02 - Crear usuario', () => {
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
    it('CP02-03 - UsuarioCorreoUnico', () => {
        cy.get("#btnNuevo").click()
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Nombre"')
        cy.get("#txtNombre").type("Angie")
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Correo"')
        cy.get("#txtCorreo").type("jazper0208@gmail.com")
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get("#toast-container").should("contain", 'Debe completar el campo:"Telefono"')
        cy.get("#txtTelefono").type("04417673")
        cy.wait(1000)
        cy.get("#cboRol").select("Empleado")
        cy.wait(1000)
        cy.get("#txtFoto").selectFile('anim/gifupdate.gif')
        cy.wait(1000)
        cy.get("#btnGuardar").click()
        cy.get('.showSweetAlert', { timeout: 50000 }).should('exist');
        cy.wait(1000)
        cy.contains("Error!").should("contain","Error!")
        cy.get(".text-muted").should("contain","El correo ya existe")
        cy.contains("OK").click()
        cy.wait(1000)
    });
    it("CP02-04 - Editar usuario", () => {
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
    it('CP02-05 - Eliminar usuario', () => {
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
    it('CP02-06 - Buscar usuario', () => {
          cy.get('[type="search"]').type("supervisor")
          cy.get(".odd").find("td").eq(1).should("contain", "supervisor")
    });

    it('CP02-07 - OrdenarUsuarioPorNombre', () => {
        cy.get(".sorting").eq(1).click()
        cy.get(".sorting").eq(1).should("have.class", "sorting_asc")
        cy.get(".sorting").eq(1).click()
        cy.get(".sorting").eq(1).should("have.class", "sorting_desc")
    });
});
