using CapstoneG14.Services.Interfaces;
using CapstoneG14.Models;
using static CapstoneG14.Repositories.Interfaces.IGenericRepository;

namespace CapstoneG14.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<RolMenu> _rolMenuRepository;
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        public MenuService(IGenericRepository<Menu> menuRepository, IGenericRepository<RolMenu> rolMenuRepository, IGenericRepository<Usuario> usuarioRepository)
        {
            _menuRepository = menuRepository;
            _rolMenuRepository = rolMenuRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<List<Menu>> ObtenerMenus(int idUsuario)
        {
            IQueryable<Usuario> tblUsuario = await _usuarioRepository.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<RolMenu> tblRolMenu = await _rolMenuRepository.Consultar();
            IQueryable<Menu> tblMenu = await _menuRepository.Consultar();

            IQueryable<Menu> MenuPadre = (from u in tblUsuario
                                          join rm in tblRolMenu on u.IdRol equals rm.IdRol
                                          join m in tblMenu on rm.IdMenu equals m.IdMenu
                                          join mpadre in tblMenu on m.IdMenuPadre equals mpadre.IdMenu
                                          select mpadre).Distinct().AsQueryable();
            IQueryable<Menu> MenuHijos = (from u in tblUsuario
                                          join rm in tblRolMenu on u.IdRol equals rm.IdRol
                                          join m in tblMenu on rm.IdMenu equals m.IdMenu
                                          where m.IdMenu != m.IdMenuPadre
                                          select m).Distinct().AsQueryable();
            List<Menu> menus = new List<Menu>(
                from mpadre in MenuPadre
                select new Menu()
                {
                    Descripcion = mpadre.Descripcion,
                    Icono = mpadre.Icono,
                    Controlador = mpadre.Controlador,
                    PaginaAccion = mpadre.PaginaAccion,
                    InverseIdMenuPadreNavigation = (
                        from mhijo in MenuHijos
                        where mhijo.IdMenuPadre == mpadre.IdMenu
                        select mhijo).ToList()
                }

            ).ToList();

            return menus;
        }

        public async Task<bool> PermisoMenu(int idUsuario, string controlador, string accion)
        {
            IQueryable<Usuario> tblUsuario = await _usuarioRepository.Consultar(u => u.IdUsuario == idUsuario);
            IQueryable<RolMenu> tblRolMenu = await _rolMenuRepository.Consultar();
            IQueryable<Menu> tblMenu = await _menuRepository.Consultar();

            Menu menu_encontrado = (from u in tblUsuario
                                    join rm in tblRolMenu on u.IdRol equals rm.IdRol
                                    join m in tblMenu on rm.IdMenu equals m.IdMenu
                                    where m.Controlador == controlador && m.PaginaAccion == accion
                                    select m).FirstOrDefault();
            if (menu_encontrado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}