import { Block, CalendarToday, Call, DateRange, Done, EventAvailable, SwapVert } from '@material-ui/icons';
import React from 'react';
import { EstadoAgenda } from '../enums';

export const obtenerIconoEstadoLlamada = () => {
    return (
        <Call />
    );
}

export const obtenerIconoEstadoAgenda = (estado: EstadoAgenda) => {
    switch (estado) {
        case EstadoAgenda.AGENDADO:
            return (
                <CalendarToday />
            );

        case EstadoAgenda.ATENDIDA:
            return (
                <Done />
            );

        case EstadoAgenda.CANCELADA:
            return (
                <Block />
            );

        case EstadoAgenda.REAGENDADA:
            return (
                <SwapVert />
            );

        case EstadoAgenda.AGENDADO_CONFIRMADO:
            return (
                <EventAvailable />
            );

        case EstadoAgenda.AGENDADO_ATENDER:
            return (
                <DateRange />
            );

        default:
            return null;
    }
};

export const obtenerDescripcionXestado = (estado: EstadoAgenda) => {
    switch (estado) {
        case EstadoAgenda.AGENDADO:
            return "Agendado por confirmar";

        case EstadoAgenda.ATENDIDA:
            return "Atendida";

        case EstadoAgenda.CANCELADA:
            return "Cancelada";

        case EstadoAgenda.REAGENDADA:
            return "Reagendada";

        case EstadoAgenda.AGENDADO_CONFIRMADO:
            return "Asistencia confirmada";

        case EstadoAgenda.AGENDADO_ATENDER:
            return "Agendado por atender";

        default:
            return "No definido";
    }
}