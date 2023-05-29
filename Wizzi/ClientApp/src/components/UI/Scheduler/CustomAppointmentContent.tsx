import { Appointments } from '@devexpress/dx-react-scheduler-material-ui';
import { createStyles, Theme, withStyles } from '@material-ui/core';
import { WithStyles } from '@material-ui/styles';
import clsx from 'clsx';
import React from 'react';

const customAppointmentContentStyles = ({ palette }: Theme) => createStyles({
    text: {
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
    },
    content: {
        opacity: 0.7,
    },
    container: {
        width: '100%',
        lineHeight: 1.2,
        height: '100%',
    },
});

type AppointmentContentProps = Appointments.AppointmentContentProps & WithStyles<typeof customAppointmentContentStyles>;

const CustomAppointmentContentBase = ({ data, classes, ...restProps }: AppointmentContentProps) => {
    console.log(data);
    const { allDay, title, startDate, endDate, cliente } = data;

    const obtenerHoraVisualizar = (fecha: Date) => {
        return fecha.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    }
    return (
        <Appointments.AppointmentContent {...restProps} data={data}>
            <div className={classes.container}>
                <div className={classes.text}>
                    {
                        cliente ?
                            `${cliente.apellido} ${cliente.nombre}`
                            :
                            title
                    }
                </div>
                {
                    !allDay &&
                    <div className={clsx(classes.text, classes.content)}>
                        {`${obtenerHoraVisualizar(startDate as Date)} - ${obtenerHoraVisualizar(endDate as Date)}`}
                    </div>
                }
            </div>
        </Appointments.AppointmentContent>
    );
};

export const CustomAppointmentContent = withStyles(customAppointmentContentStyles, { name: 'AppointmentContent' })(CustomAppointmentContentBase);