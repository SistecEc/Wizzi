using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Wizzi.Constants;
using Wizzi.Dtos.Clientes;
using Wizzi.Entities;
using Wizzi.Enums;
using Wizzi.Extensions;
using Wizzi.Helpers;
using Wizzi.Models;
using Wizzi.Services;

namespace Wizzi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;
        private readonly UserResolverService _currentUserService;

        public ClientesController(
            DataContext wiseContext,
            IMapper mapper,
            UserResolverService currentUserService
            )
        {
            _wiseContext = wiseContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpPost("")]
        public IActionResult registrarCliente([FromBody] RegistrarClienteDto clienteRecibido)
        {

            string empleadoActual = _currentUserService.GetCode();
            Clientes clienteGrabar = _mapper.Map<Clientes>(clienteRecibido);
            clienteGrabar.TelefonoUnoCliente = $"{clienteGrabar.TelefonoUnoCliente}";
            clienteGrabar.CodigoCliente = utils.generarCodigoFecha();
            clienteGrabar.Clienteslocalizaciones = new Clienteslocalizaciones()
            {
                PaisesClienteLocalizacion = clienteRecibido.localizacion.Pais,
                ProvinciasClienteLocalizacion = clienteRecibido.localizacion.Provincia,
                CantonesClienteLocalizacion = clienteRecibido.localizacion.Canton,
                ParroquiasClienteLocalizacion = clienteRecibido.localizacion.Parroquia
            };
            clienteGrabar.ObservacionCliente = "";
            clienteGrabar.CuentasContabilidadCliente = "0";
            clienteGrabar.TransportesCliente = "0";
            clienteGrabar.UsuariosCliente = User.Identity.Name;
            clienteGrabar.EmailDespahosCliente = "";
            clienteGrabar.RutasEntregasCliente = "0";

            List<Empresasclientes> empresasClientes = new List<Empresasclientes>();
            List<Clientesvendedores> clientesVendedores = new List<Clientesvendedores>();
            IEnumerable<Empresas> empresas = _wiseContext.Empresas.AsNoTracking().ToList();
            foreach (Empresas empresa in empresas)
            {
                empresasClientes.Add(new Empresasclientes()
                {
                    ClientesEmpresaCliente = clienteGrabar.CodigoCliente,
                    EmpresasEmpresaCliente = empresa.CodigoEmpresa,
                    OmitirCupoEmpresaCliente = "1",
                    OmitirDescuentoEmpresaCliente = "1"
                });

                clientesVendedores.Add(new Clientesvendedores()
                {
                    ClientesClienteVendedor = clienteGrabar.CodigoCliente,
                    VendedoresClienteVendedor = empleadoActual,
                    CobradoresClienteVendedor = empleadoActual,
                    EmpresasClienteVendedor = empresa.CodigoEmpresa,
                    ListaPrecioCliente = "0",
                    ListaPrecioMaximaCliente = "0",
                    EmpleadosClienteVendedor = empleadoActual
                });
            }

            _wiseContext.Clientesvendedores.AddRange(clientesVendedores);

            clienteGrabar.Empresasclientes = empresasClientes;

            if (int.Parse(clienteGrabar.TiposIdentificacionCliente) == (int)TipoIdentificacion.POTENCIAL)
            {
                IEnumerable<string> numeros = _wiseContext.Clientes
                                .Where(c => c.TiposIdentificacionCliente == ((int)TipoIdentificacion.POTENCIAL).ToString())
                                .Select(c => c.NumeroIdentificacionCliente)
                                .ToList();

                int secuencial = 1;
                if (numeros.Count() > 0)
                {
                    int temp;
                    secuencial = numeros
                                   .Select(n => int.TryParse(n, out temp) ? temp : 0)
                                   .Max() + 1;
                }
                clienteGrabar.NumeroIdentificacionCliente = secuencial.ToString();
            }
            clienteGrabar.TiposClienteCarteraCliente = "0";

            _wiseContext.Clientes.Add(clienteGrabar);
            try
            {
                _wiseContext.SaveChanges();
                return GetById(clienteGrabar.CodigoCliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new msjRespuesta { codigo = codigosMensajes.ERROR_AL_GRABAR, detalle = string.Concat(ex.Message, " - ", ex.InnerException) });
            }
        }

        [HttpPatch("")]
        public IActionResult actualizarCliente([FromBody] RegistrarClienteDto clienteRecibido)
        {
            Clientes clienteGrabado = _wiseContext.Clientes
                                        .Include(c => c.Clienteslocalizaciones)
                                        .Where(c => c.CodigoCliente == clienteRecibido.Codigo)
                                        .FirstOrDefault();
            if (clienteGrabado != null)
            {
                clienteGrabado.TiposIdentificacionCliente = clienteRecibido.TipoIdentificacion;
                clienteGrabado.NumeroIdentificacionCliente = clienteRecibido.Identificacion;
                clienteGrabado.NombreComercialCliente = clienteRecibido.NombreComercial;
                clienteGrabado.PrioridadNombreComercialCliente = clienteRecibido.PrioridadNombreComercial ? "1" : "0";
                clienteGrabado.NombreCliente = clienteRecibido.Nombre;
                clienteGrabado.ApellidoCliente = clienteRecibido.Apellido;
                clienteGrabado.DireccionUnoCliente = clienteRecibido.Direccion;
                clienteGrabado.TelefonoUnoCliente = clienteRecibido.Telefono;
                clienteGrabado.MailCliente = clienteRecibido.Email;
                clienteGrabado.SexoCliente = clienteRecibido.Genero;
                clienteGrabado.FechaNacimientoCliente = clienteRecibido.FechaNacimiento.ToTimeZoneTime();

                if (clienteGrabado.Clienteslocalizaciones == null)
                {
                    clienteGrabado.Clienteslocalizaciones = new Clienteslocalizaciones()
                    {
                        PaisesClienteLocalizacion = clienteRecibido.localizacion.Pais,
                        ProvinciasClienteLocalizacion = clienteRecibido.localizacion.Provincia,
                        CantonesClienteLocalizacion = clienteRecibido.localizacion.Canton,
                        ParroquiasClienteLocalizacion = clienteRecibido.localizacion.Parroquia
                    };
                }
                else
                {
                    clienteGrabado.Clienteslocalizaciones.PaisesClienteLocalizacion = clienteRecibido.localizacion.Pais;
                    clienteGrabado.Clienteslocalizaciones.ProvinciasClienteLocalizacion = clienteRecibido.localizacion.Provincia;
                    clienteGrabado.Clienteslocalizaciones.CantonesClienteLocalizacion = clienteRecibido.localizacion.Canton;
                    clienteGrabado.Clienteslocalizaciones.ParroquiasClienteLocalizacion = clienteRecibido.localizacion.Parroquia;
                }

                _wiseContext.Clientes.Update(clienteGrabado);
                if (_wiseContext.SaveChanges() > 0)
                {
                    return GetById(clienteGrabado.CodigoCliente);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult GetAll(int p = 1)
        {
            ResultadoPaginado<VerClienteDto> clientesDtos = _wiseContext.Clientes
                                                            .Include(c => c.TiposIdentificacionClienteNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.PaisesClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.ProvinciasClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.CantonesClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.ParroquiasClienteLocalizacionNavigation)
                                                            .Where(c => c.TiposIdentificacionCliente != ((int)TipoIdentificacion.POTENCIAL).ToString())
                                                            .GetPaged<Clientes, VerClienteDto>(p, 5, _mapper, AgregarExtrasClienteDto);
            return Ok(clientesDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Clientes cliente = _wiseContext.Clientes
                                .Include(c => c.TiposIdentificacionClienteNavigation)
                                .Include(c => c.Clienteslocalizaciones)
                                    .ThenInclude(ci => ci.PaisesClienteLocalizacionNavigation)
                                .Include(c => c.Clienteslocalizaciones)
                                    .ThenInclude(ci => ci.ProvinciasClienteLocalizacionNavigation)
                                .Include(c => c.Clienteslocalizaciones)
                                    .ThenInclude(ci => ci.CantonesClienteLocalizacionNavigation)
                                .Include(c => c.Clienteslocalizaciones)
                                    .ThenInclude(ci => ci.ParroquiasClienteLocalizacionNavigation)
                                .Where(C => C.CodigoCliente == id)
                                .FirstOrDefault();
            if (cliente != null)
            {
                VerClienteDto clienteDto = AgregarExtrasClienteDto(cliente);
                return Ok(clienteDto);
            }
            else
            {
                return NoContent();
            }
        }

        private VerClienteDto AgregarExtrasClienteDto(Clientes cliente)
        {
            VerClienteDto clienteDto = _mapper.Map<VerClienteDto>(cliente);
            clienteDto.PrioridadNombreComercial = cliente.PrioridadNombreComercialCliente == "1";
            return clienteDto;
        }

        [HttpGet("find")]
        public IActionResult Find(string query, int p = 1, int tp = 10)
        {
            string[] palabrasBuscar = HttpUtility.UrlDecode(query).Split(" ");

            Expression<Func<Clientes, bool>> condicionTipoIdentificacionCliente = c => c.TiposIdentificacionCliente != ((int)TipoIdentificacion.POTENCIAL).ToString();

            Expression<Func<Clientes, bool>> condicionNumeroIdentificacionCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NumeroIdentificacionCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNumeroIdentificacionCliente = condicionNumeroIdentificacionCliente == null ? e1 : condicionNumeroIdentificacionCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NombreCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreCliente = condicionNombreCliente == null ? e1 : condicionNombreCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionApellidoCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.ApellidoCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionApellidoCliente = condicionApellidoCliente == null ? e1 : condicionApellidoCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreComercialCliente = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => c.NombreComercialCliente.Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreComercialCliente = condicionNombreComercialCliente == null ? e1 : condicionNombreComercialCliente.And(e1);
                }
            }

            Expression<Func<Clientes, bool>> condicionNombreCompleto = null;
            foreach (var palabra in palabrasBuscar)
            {
                if (palabra.Length > 3)
                {
                    Expression<Func<Clientes, bool>> e1 = c => (c.NombreCliente + " " + c.ApellidoCliente).Contains(palabra, StringComparison.CurrentCultureIgnoreCase);
                    condicionNombreCompleto = condicionNombreCompleto == null ? e1 : condicionNombreCompleto.And(e1);
                }
            }

            ResultadoPaginado<VerClienteDto> clientesDtos = _wiseContext.Clientes
                                                            .Include(c => c.TiposIdentificacionClienteNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.PaisesClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.ProvinciasClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.CantonesClienteLocalizacionNavigation)
                                                            .Include(c => c.Clienteslocalizaciones)
                                                                .ThenInclude(ci => ci.ParroquiasClienteLocalizacionNavigation)
                                                            .Where(condicionTipoIdentificacionCliente
                                                                    .And(condicionNumeroIdentificacionCliente
                                                                    .Or(condicionNombreCliente)
                                                                    .Or(condicionNombreCliente)
                                                                    .Or(condicionApellidoCliente)
                                                                    .Or(condicionNombreComercialCliente)
                                                                    .Or(condicionNombreCompleto)))
                                                            .GetPaged<Clientes, VerClienteDto>(p, tp, _mapper, AgregarExtrasClienteDto);
            return Ok(clientesDtos);
        }
    }
}
