import { ClienteCitaMedicaAgendaDto } from "../dtos/Agendas";
import { VerClienteDto } from "../dtos/CitasMedicas";

export const from_Cita_VerClienteDto_to_ClienteCitaMedicaAgendaDto = (cliente: VerClienteDto): ClienteCitaMedicaAgendaDto => {
    const {
        codigo,
        numeroIdentificacion,
        nombreComercial,
        prioridadNombreComercial,
        nombre,
        apellido,
        direccion,
        telefono,
        mail,
        sexo,
    } = cliente;
    return {
        codigo,
        identificacion: numeroIdentificacion,
        nombreComercial,
        prioridadNombreComercial,
        nombre,
        apellido,
        direccion,
        telefono,
        email: mail,
        genero: sexo,
    }
}