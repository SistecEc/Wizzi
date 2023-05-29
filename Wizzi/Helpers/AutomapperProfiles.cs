using AutoMapper;
using System;
using Wizzi.Helpers.MpperProfiles;

namespace Wizzi.Helpers
{
    public static class AutomapperProfiles
    {
        public static Action<IMapperConfigurationExpression> ProfilesConfig =
            new Action<IMapperConfigurationExpression>(cfg =>
                {
                    cfg.AddProfile<AgendasProfile>();
                    cfg.AddProfile<CampaniasProfile>();
                    cfg.AddProfile<CitasMedicasProfile>();
                    cfg.AddProfile<ClientesProfile>();
                    cfg.AddProfile<EmpleadosProfile>();
                    cfg.AddProfile<LocalizacionesProfile>();
                    cfg.AddProfile<SolicitudesCitasMedicasProfile>();
                    cfg.AddProfile<SubCampaniasProfile>();
                    cfg.AddProfile<SucursalesProfile>();
                    cfg.AddProfile<TiposIdentificacionProfile>();
                    cfg.AddProfile<RelacionRepresentantePacienteProfile>();
                });

    }
}
