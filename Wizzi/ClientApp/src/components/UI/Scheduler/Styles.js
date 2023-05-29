import { blue, lightBlue } from '@material-ui/core/colors';
import { coloresFondoEstadoAgenda, coloresIconoEstadoAgenda } from '../../../helpers/colorPalette';

export const customCellStyles = theme => ({
    celdaResaltada: {
        backgroundColor: theme.palette.type === 'light' ? lightBlue[50] : '#185f8c',
        '&:hover': {
            backgroundColor: lightBlue[theme.palette.type === 'light' ? 100 : 600],
        },
    },
});

export const customAppointmentStyles = theme => ({
    AgendaEditable: {
        borderStyle: 'dashed',
        borderWidth: 3,
        borderColor: blue[500],
        borderRadius: 5,
    },
    badge: {
        width: "100%",
        height: "100%"
    },
    iconContainer: {
        width: "20px",
        height: "20px",
        '&> svg': {
            padding: "2px",
            maxHeight: 18,
        },
    },
    iconoAgendada: {
        color: coloresIconoEstadoAgenda.agendado,
        backgroundColor: coloresFondoEstadoAgenda.agendado,
    },
    iconoAtendida: {
        color: coloresIconoEstadoAgenda.atendida,
        backgroundColor: coloresFondoEstadoAgenda.atendida,
    },
    iconoCancelada: {
        color: coloresIconoEstadoAgenda.cancelada,
        backgroundColor: coloresFondoEstadoAgenda.cancelada,
    },
    iconoReagendada: {
        color: coloresIconoEstadoAgenda.reagendada,
        backgroundColor: coloresFondoEstadoAgenda.reagendada,
    },
    iconoAgendadoConfirmado: {
        color: coloresIconoEstadoAgenda.agendado_confirmado,
        backgroundColor: coloresFondoEstadoAgenda.agendado_confirmado,
    },
    iconoAgendadoAtender: {
        color: coloresIconoEstadoAgenda.agendado_atender,
        backgroundColor: coloresFondoEstadoAgenda.agendado_atender,
    },
});
