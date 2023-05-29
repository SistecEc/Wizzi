using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wizzi.Dtos.RelacionesRepresentantes;
using Wizzi.Entities;
using Wizzi.Helpers;

namespace Wizzi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RelacionesRepPacController : ControllerBase
    {
        private DataContext _wiseContext;
        private IMapper _mapper;

        public RelacionesRepPacController(
            DataContext wiseContext,
            IMapper mapper
            )
        {
            _wiseContext = wiseContext;
            _wiseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            IEnumerable<Relacionrepresentantepaciente> relaciones = _wiseContext.Relacionrepresentantepaciente.ToList();
            return Ok(_mapper.Map<IEnumerable<VerRelacionRepresentantePaciente>>(relaciones));

        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Relacionrepresentantepaciente relacion = _wiseContext.Relacionrepresentantepaciente
                                                    .Find(id);
            return Ok(_mapper.Map<VerRelacionRepresentantePaciente>(relacion));
        }

    }
}