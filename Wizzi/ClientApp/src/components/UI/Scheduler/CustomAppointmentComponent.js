import { Appointments } from '@devexpress/dx-react-scheduler-material-ui';
import { Badge, withStyles } from '@material-ui/core';
import React from 'react';
import { EstadoAgenda } from '../../../enums';
import { obtenerIconoEstadoAgenda } from '../../../helpers';
import { customAppointmentStyles } from './Styles';

const CustomAppointmentComponentBase = ({ children, beforeClick, onClick: clickFn, data: datos, classes, condicionMarcarEditable, ...restProps }) => {
    const clickAgenda = (info) => {
        if (beforeClick) {
            beforeClick(info, clickFn)
        } else {
            clickFn(info);
        }
    }

    const getBadgeClass = (estado) => {
        switch (estado) {
            case EstadoAgenda.AGENDADO:
                return classes.iconoAgendada;

            case EstadoAgenda.ATENDIDA:
                return classes.iconoAtendida;

            case EstadoAgenda.CANCELADA:
                return classes.iconoCancelada;

            case EstadoAgenda.REAGENDADA:
                return classes.iconoReagendada;

            case EstadoAgenda.AGENDADO_CONFIRMADO:
                return classes.iconoAgendadoConfirmado;

            case EstadoAgenda.AGENDADO_ATENDER:
                return classes.iconoAgendadoAtender;

            default:
                return null;
        }
    }

    const getCustomClassName = () => {
        let result = null;
        if (condicionMarcarEditable) {
            if (condicionMarcarEditable(datos)) {
                result += ` ${classes.AgendaEditable}`
            }
        }
        return result;
    }

    return (
        <Badge
            badgeContent={obtenerIconoEstadoAgenda(datos.estado)}
            classes={{ root: classes.badge, badge: `${classes.iconContainer} ${getBadgeClass(datos.estado)}` }}
            anchorOrigin={{ horizontal: "left", vertical: "top" }}
        >
            <Appointments.Appointment
                {...restProps}
                onClick={clickAgenda}
                data={datos}
                className={getCustomClassName()}
            >
                {children}
            </Appointments.Appointment>
        </Badge>
    );
};

export const CustomAppointmentComponent = withStyles(customAppointmentStyles, { name: 'badged' })(CustomAppointmentComponentBase);