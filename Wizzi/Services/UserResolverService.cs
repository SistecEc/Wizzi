using Microsoft.AspNetCore.Http;
using Wizzi.Entities;
using Wizzi.Interfaces;

namespace Wizzi.Services
{
    public class UserResolverService
    {
        private IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserResolverService(
            IUserService userService,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _userService = userService;
            _httpContextAccesor = httpContextAccesor;
        }

        public string GetCode()
        {
            return _httpContextAccesor.HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        }

        public string GetUserName()
        {
            return _httpContextAccesor.HttpContext.User?.Identity?.Name;
        }

        public Empleados GetEmpleado()
        {
            string codigoEmpleado = GetCode();
            if (codigoEmpleado != null)
            {
                return _userService.GetByIdUntracked(GetCode());
            }
            else
            {
                return null;
            }
        }
    }
}
