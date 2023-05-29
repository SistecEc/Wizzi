import { amber, blue, blueGrey, brown, cyan, deepOrange, deepPurple, green, indigo, lightBlue, lightGreen, orange, pink, purple, red, teal } from '@material-ui/core/colors';
import { EstadoAgenda } from '../enums';

export const colorPalette = [
    red,
    pink,
    purple,
    deepPurple,
    indigo,
    blue,
    lightBlue,
    cyan,
    teal,
    green,
    lightGreen,
    orange,
    deepOrange,
    brown,
    '#827717',
    blueGrey,
]

export const coloresIconoEstadoLlamada = {
    realizado: "#FFF",
}

export const coloresFondoEstadoLlamada = {
    realizado: blue[500],
}
export const coloresIconoEstadoAgenda = {
    agendado: "#000",
    atendida: "#FFF",
    cancelada: "#FFF",
    reagendada: "#FFF",
    agendado_confirmado: "#000",
    agendado_atender: "#FFF",
}

export const coloresFondoEstadoAgenda = {
    agendado: "#FFF",
    atendida: green[500],
    cancelada: red[500],
    reagendada: blue[500],
    agendado_confirmado: amber[500],
    agendado_atender: green[500],
}

export const obtenerColorXestado = (estado) => {
    switch (estado) {
        case EstadoAgenda.AGENDADO:
            return coloresFondoEstadoAgenda.agendado;

        case EstadoAgenda.ATENDIDA:
            return coloresFondoEstadoAgenda.atendida;

        case EstadoAgenda.CANCELADA:
            return coloresFondoEstadoAgenda.cancelada;

        case EstadoAgenda.REAGENDADA:
            return coloresFondoEstadoAgenda.reagendada;

        case EstadoAgenda.AGENDADO_CONFIRMADO:
            return coloresFondoEstadoAgenda.agendado_confirmado;

        case EstadoAgenda.AGENDADO_ATENDER:
            return coloresFondoEstadoAgenda.agendado_atender;

        default:
            return null;
    }
}
