import {
    CLOSE_COMMAND_BUTTON, DELETE_COMMAND_BUTTON, OPEN_COMMAND_BUTTON
} from '@devexpress/dx-scheduler-core';
import { Close, Delete, Edit, Visibility } from '@material-ui/icons';
import * as dayjs from 'dayjs';
import React from 'react';

export const getIcon = (id, sePuedeEditar) => {
    if (id === OPEN_COMMAND_BUTTON) {
        return sePuedeEditar ? <Edit /> : <Visibility />;
    } if (id === CLOSE_COMMAND_BUTTON) {
        return <Close />;
    } if (id === DELETE_COMMAND_BUTTON) {
        return <Delete />;
    } return null;
};

const compareUnit = "minute";

export const seDebeResaltarFecha = (fechaVerificar, fechaInicio, fechaFin) => {
    if (fechaVerificar) {
        fechaVerificar = dayjs(fechaVerificar);
        return (fechaVerificar.isAfter(fechaInicio, compareUnit) || fechaVerificar.isSame(fechaInicio, compareUnit)) && fechaVerificar.isBefore(fechaFin, compareUnit)
    } else {
        return false
    }
}