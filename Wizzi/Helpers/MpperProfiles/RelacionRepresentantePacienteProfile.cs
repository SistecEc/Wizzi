using AutoMapper;
using Wizzi.Dtos.RelacionesRepresentantes;
using Wizzi.Entities;

namespace Wizzi.Helpers.MpperProfiles
{
    public class RelacionRepresentantePacienteProfile : Profile
    {
        public RelacionRepresentantePacienteProfile()
        {
            CreateMap<Relacionrepresentantepaciente, VerRelacionRepresentantePaciente>()
                .ForMember(dest => dest.Codigo,
                            opt => opt.MapFrom(o => o.CodigoRelacionRepresentantePaciente))
                .ForMember(dest => dest.Descripcion,
                            opt => opt.MapFrom(o => o.DescripcionRelacionRepresentantePaciente));

        }
    }
}
