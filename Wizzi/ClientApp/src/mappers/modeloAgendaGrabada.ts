import { VerAgendaCitaMedicaDto } from "../dtos/Agendas";
import { VerCitaMedicaDto } from "../dtos/CitasMedicas";
import { ModeloAgendaGrabada } from "../models/modeloAgendaGrabada";
import { from_Cita_VerClienteDto_to_ClienteCitaMedicaAgendaDto } from "./";

export const from_VerAgendaCitaMedicaDto_to_ModeloAgendaGrabada = (infoAgenda: VerAgendaCitaMedicaDto): ModeloAgendaGrabada => {
    const { codigo, fechaInicio, fechaFin,
        titulo, todoElDia, reglaRecurrencia,
        fechasExcluidasRecurrencia, descripcion,
        empleado, tipoAgenda, cita
    } = infoAgenda;
    const { tipoCitaMedica, fuenteRemision, cliente } = cita;
    return {
        codigo,
        startDate: fechaInicio,
        endDate: fechaFin,
        title: titulo,
        allDay: todoElDia,
        rRule: reglaRecurrencia,
        exDate: fechasExcluidasRecurrencia,
        notes: descripcion,
        codigoEmpleado: empleado.codigo,
        tipoAgenda: tipoAgenda.codigo,
        tipoCitaMedica,
        fuenteRemision,
        cliente,
    }
}

export const from_VerCitaMedicaDto_to_ModeloAgendaGrabada = (infoCita: VerCitaMedicaDto): ModeloAgendaGrabada => {
    const { tipoCita, fuenteRemision, cliente, agenda } = infoCita;
    const { codigo, fechaInicio, fechaFin,
        titulo, todoElDia, reglaRecurrencia,
        fechasExcluidasRecurrencia, descripcion,
        empleado, tipoAgenda
    } = agenda;
    return {
        codigo,
        startDate: fechaInicio,
        endDate: fechaFin,
        title: titulo,
        allDay: todoElDia,
        rRule: reglaRecurrencia,
        exDate: fechasExcluidasRecurrencia,
        notes: descripcion,
        codigoEmpleado: empleado.codigo,
        tipoAgenda: tipoAgenda.codigo,
        tipoCitaMedica: tipoCita,
        fuenteRemision: fuenteRemision.codigo,
        cliente: from_Cita_VerClienteDto_to_ClienteCitaMedicaAgendaDto(cliente),
    }
}