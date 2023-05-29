import { AppointmentTooltip } from '@devexpress/dx-react-scheduler-material-ui';
import { Grid, makeStyles } from '@material-ui/core';
import { Phone } from '@material-ui/icons';
import React from 'react';

const contentStyles = makeStyles(theme => {
    return {
        icon: {
            color: theme.palette.action.active,
        },
        textCenter: {
            textAlign: 'center',
        },
    }
});

export const CustomAppointmentTooltipContent = ({ children, appointmentData, ...restProps }) => {
    const classes = contentStyles();

    return (
        <AppointmentTooltip.Content {...restProps} appointmentData={appointmentData}>
            <Grid container alignItems="center">
                <Grid item xs={2} className={classes.textCenter}>
                    <Phone className={classes.icon} />
                </Grid>
                <Grid item xs={10}>
                    <span>{appointmentData.cliente?.telefono}</span>
                </Grid>
            </Grid>
        </AppointmentTooltip.Content>
    );
}
