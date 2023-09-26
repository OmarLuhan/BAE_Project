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
    }
  }
}