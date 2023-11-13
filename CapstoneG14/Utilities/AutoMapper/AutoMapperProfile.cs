using System.Globalization;
using AutoMapper;
using CapstoneG14.Models;
using CapstoneG14.Models.ViewModels;

namespace CapstoneG14.Utilities.AutoMapper
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      #region Rol
      CreateMap<Rol, VMRol>().ReverseMap();
      #endregion
      #region Usuario
      CreateMap<Usuario, VMUsuario>()
          .ForMember(dest =>
          dest.EsActivo,
          opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
          )
          .ForMember(destino =>
          destino.NombreRol,
          opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion)
           );
      CreateMap<VMUsuario, Usuario>()
          .ForMember(dest =>
            dest.EsActivo,
            opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
            )
          .ForMember(dest => dest.IdRolNavigation,
          opt => opt.Ignore()
          );
      #endregion
      #region Menu
      CreateMap<Menu, VMMenu>()
          .ForMember(dest =>
           dest.SubMenus,
           opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation)
            );
      #endregion
      #region Negocio
      CreateMap<Negocio, VMNegocio>()
          .ForMember(dest =>
          dest.PorcentajeImpuesto,
          opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-PE")))
           );
      CreateMap<VMNegocio, Negocio>()
          .ForMember(dest =>
          dest.PorcentajeImpuesto,
          opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto, new CultureInfo("es-PE")))
           );
      #endregion
      #region Genero
      CreateMap<Genero, VMGenero>()
          .ForMember(dest =>
          dest.EsActivo,
          opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
          );
      CreateMap<VMGenero, Genero>()
      .ForMember(dest =>
       dest.EsActivo,
       opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
        );

      #endregion
      #region Editorial
      CreateMap<Editorial, VMEditorial>()
          .ForMember(dest =>
          dest.EsActivo,
          opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
          );
      CreateMap<VMEditorial, Editorial>()
      .ForMember(dest =>
       dest.EsActivo,
       opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
        );
      #endregion
      #region TiendaE
      CreateMap<Tiendum, VMTienda>()
          .ForMember(dest =>
          dest.EsActivo,
          opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
          );
      CreateMap<VMTienda, Tiendum>()
      .ForMember(dest =>
       dest.EsActivo,
       opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
        );
      #endregion
      #region Libro
      CreateMap<Libro, VMLibro>()
        .ForMember(dest => dest.EsActivo,
        opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
        )
        .ForMember(dest => dest.NombreEditorial,
        opt => opt.MapFrom(origen => origen.IdEditorialNavigation.Descripcion)
        )
        .ForMember(dest => dest.NombreGenero,
        opt => opt.MapFrom(origen => origen.IdGeneroNavigation.Descripcion)
        )
        .ForMember(dest => dest.Precio,
         opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
         );

      CreateMap<VMLibro, Libro>()
      .ForMember(dest => dest.EsActivo,
      opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
      )
      .ForMember(dest => dest.IdEditorialNavigation,
      opt => opt.Ignore()
      )
      .ForMember(dest => dest.IdGeneroNavigation,
      opt => opt.Ignore()
      )
      .ForMember(dest => dest.Precio,
      opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
      );
      #endregion
      #region TipoDocumento
      CreateMap<TipoDocumentoVentum, VMTipoDocumentoVenta>().ReverseMap();
      #endregion
      #region Venta
      CreateMap<Ventum, VMVenta>()
          .ForMember(dest =>
           dest.TipoDocumentoVenta,
           opt => opt.MapFrom(origen => origen.IdTipoDocumentoVentaNavigation.Descripcion)
           )
          .ForMember(dest =>
          dest.Usuario,
          opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
          )
          .ForMember(dest =>
           dest.SubTotal,
           opt => opt.MapFrom(origen => Convert.ToString(origen.SubTotal.Value, new CultureInfo("es-PE")))
           )
          .ForMember(dest =>
           dest.ImpuestoTotal,
           opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-PE")))
           )
           .ForMember(dest =>
          dest.Total,
         opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
         )
           .ForMember(dest =>
          dest.FechaRegistro,
         opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
         );

      CreateMap<VMVenta, Ventum>()

         .ForMember(dest =>
          dest.SubTotal,
          opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-PE")))
          )
         .ForMember(dest =>
          dest.ImpuestoTotal,
          opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-PE")))
          )
          .ForMember(dest =>
         dest.Total,
        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
        );
      #endregion
      #region DetalleVenta
      CreateMap<DetalleVentum, VMDetalleVenta>()
          .ForMember(dest =>
           dest.Precio,
           opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.Total,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            );
      CreateMap<VMDetalleVenta, DetalleVentum>()
          .ForMember(dest =>
           dest.Precio,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.Total,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
            );
      CreateMap<DetalleVentum, VMReporteVenta>()
          .ForMember(dest =>
            dest.FechaReigstro,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            )
          .ForMember(dest =>
            dest.NumeroVenta,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroVenta)
            )
          .ForMember(dest =>
            dest.TipoDocumento,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.IdTipoDocumentoVentaNavigation.Descripcion)
            )
          .ForMember(dest =>
            dest.TipoDocumentoCliente,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.DocumentoCliente)
            )
          .ForMember(dest =>
            dest.NombreCliente,
            opt => opt.MapFrom(origen => origen.IdVentaNavigation.NombreCliente)
            )
          .ForMember(dest =>
            dest.SubtotalVenta,
            opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.SubTotal.Value, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.ImpuestoTotalVenta,
            opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.ImpuestoTotal.Value, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.TotalVenta,
            opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.Libro,
            opt => opt.MapFrom(origen => origen.TituloLibro)
            )
          .ForMember(dest =>
            dest.Precio,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            )
          .ForMember(dest =>
            dest.Total,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            );

      #endregion
      #region Pedido
      CreateMap<Pedido, VMPedido>()
          .ForMember(dest =>
           dest.FechaRegistro,
           opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
           )
          .ForMember(dest =>
           dest.FechaEntrega,
           opt => opt.MapFrom(origen => origen.FechaEntrega.Value.ToString("dd/MM/yyyy"))
           )
          .ForMember(dest =>
           dest.Estado,
           opt => opt.MapFrom(origen => origen.Estado==true? 1: 0)
           )
          .ForMember(dest =>
           dest.Tienda,
           opt => opt.MapFrom(origen => origen.IdTiendaNavigation.Descripcion)
           )
          .ForMember(dest =>
           dest.Usuario,
           opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre)
           )
           .ForMember(dest =>
           dest.TipoDocumentoPedido,
            opt => opt.MapFrom(origen => origen.IdTipoDocumentoPedidoNavigation.Descripcion)
            )
           .ForMember(dest =>
          dest.Total,
         opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
         );
         CreateMap<VMPedido, Pedido>()
         .ForMember(dest =>
         dest.Total,
        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
         ).ForMember(dest =>dest.Estado,
          opt => opt.MapFrom(origen => origen.Estado==1? true: false)
          );
      #endregion
      #region DetallePedido
      CreateMap<DetallePedido,VMDetallePedido>()
       .ForMember(dest =>
           dest.Precio,
           opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            )
        .ForMember(dest =>
           dest.Total,
           opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
         );
      CreateMap<VMDetallePedido, DetallePedido>()
      .ForMember(dest =>
           dest.Precio,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
            )
        .ForMember(dest =>
           dest.Total,
           opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-PE")))
         );
      #endregion
    }
  }
}