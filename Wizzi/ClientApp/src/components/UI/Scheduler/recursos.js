import { tiposCitasMedicasService } from '../../../services';

export const obtenerRecursosTiposCitasMedicas = () => {
    tiposCitasMedicasService.getTiposCitasMedicas()
        .then(
            tiposCitas => {
                return {
                    fieldName: 'tipoCitaMedica',
                    title: 'Tipo de cita',
                    instances: tiposCitas.map(tipoCita => (
                        {
                            id: tipoCita.codigo,
                            text: tipoCita.descripcion,
                        }
                    )),
                }
            },
            error => {
                return error;
            });

}
