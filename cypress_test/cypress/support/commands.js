// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
Cypress.Commands.add('login', () => { 
    cy.visit("http://localhost:5000/")
    cy.get("#Correo").type("n00209455@upn.pe")
    cy.get("#Clave").type("123")
    cy.get(".btn").click()
    cy.get(".display-4").should("have.text", "Bienvenido a BAE")
})

Cypress.Commands.add('inDashboard', () => {
    cy.get("#accordionSidebar").find(".nav-item").first().click()
    cy.get(".h3").should("have.text", "Dashboard")
})
Cypress.Commands.add('inUsuario', () => {
    cy.get("#accordionSidebar").find(".nav-item").eq(1).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(1).find("a").eq(1).click()
    cy.get(".m-0").should("have.text", "Lista de Usuarios")
})
Cypress.Commands.add('inNegocio', () => {
    cy.get("#accordionSidebar").find(".nav-item").eq(1).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(1).find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Datos del Negocio")
})

Cypress.Commands.add('inGenero', (rol) => {
    if(rol == "Administrador"){
    cy.get("#accordionSidebar").find(".nav-item").eq(2).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(2).find("a").eq(1).click()
    cy.get(".m-0").should("have.text", "Lista Genero Libro")
    }
    if(rol == "Supervisor"){
    cy.get("#accordionSidebar").find(".nav-item").first().click()
    cy.get("#accordionSidebar").find(".nav-item").first().find("a").eq(1).click()
    cy.get(".m-0").should("have.text", "Lista Genero Libro")
    }
});

Cypress.Commands.add('inEditorial', (rol) => {
    if(rol == "Administrador"){
    cy.get("#accordionSidebar").find(".nav-item").eq(2).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(2).find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Lista Editorial Libro")
    }
    if(rol == "Supervisor"){
    cy.get("#accordionSidebar").find(".nav-item").first().click()
    cy.get("#accordionSidebar").find(".nav-item").first().find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Lista Editorial Libro")
    }
});

Cypress.Commands.add('inLibro', (rol) => {
    if(rol == "Administrador"){
    cy.get("#accordionSidebar").find(".nav-item").eq(2).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(2).find("a").eq(3).click()
    cy.get(".m-0").should("have.text", "Lista de Libros")
    }
    if(rol == "Supervisor"){
    cy.get("#accordionSidebar").find(".nav-item").first().click()
    cy.get("#accordionSidebar").find(".nav-item").first().find("a").eq(3).click()
    cy.get(".m-0").should("have.text", "Lista de Libros")
    }
});
Cypress.Commands.add('inNewVenta', (rol) => {
    if(rol == "Administrador"){
    cy.get("#accordionSidebar").find(".nav-item").eq(4).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(4).find("a").eq(1).click()
    cy.get(".m-0").first().should("have.text", "Cliente")
    }
    if(rol == "Supervisor"){
    cy.get("#accordionSidebar").find(".nav-item").eq(2).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(2).find("a").eq(1).click()
    cy.get(".m-0").first().should("have.text", "Cliente")
    }
    if(rol == "Empleado"){
     cy.get("#accordionSidebar").find(".nav-item").eq(1).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(1).find("a").eq(1).click()
    cy.get(".m-0").first().should("have.text", "Cliente")
    }
});
Cypress.Commands.add('inHistorialVenta', (rol) => {
    if(rol == "Administrador"){
    cy.get("#accordionSidebar").find(".nav-item").eq(4).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(4).find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Historial de Venta")
    }
    if(rol == "Supervisor"){
    cy.get("#accordionSidebar").find(".nav-item").eq(2).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(2).find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Historial de Venta")
    }
    if(rol == "Empleado"){
    cy.get("#accordionSidebar").find(".nav-item").eq(1).click()
    cy.get("#accordionSidebar").find(".nav-item").eq(1).find("a").eq(2).click()
    cy.get(".m-0").should("have.text", "Historial de Venta")
    }
    
});
Cypress.Commands.add('inReporte', () => {});