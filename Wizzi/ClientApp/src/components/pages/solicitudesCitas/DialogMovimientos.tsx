import { Button, createStyles, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, makeStyles, Paper, Typography } from '@material-ui/core';
import { Theme } from '@material-ui/core/styles';
import { Bookmark, Edit } from '@material-ui/icons';
import Timeline from '@material-ui/lab/Timeline';
import TimelineConnector from '@material-ui/lab/TimelineConnector';
import TimelineContent from '@material-ui/lab/TimelineContent';
import TimelineDot from '@material-ui/lab/TimelineDot';
import TimelineItem from '@material-ui/lab/TimelineItem';
import TimelineOppositeContent from '@material-ui/lab/TimelineOppositeContent';
import TimelineSeparator from '@material-ui/lab/TimelineSeparator';
import dayjs from 'dayjs';
import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerMovimientosDto } from '../../../dtos/Movimientos';
import { EstadoAgenda, TipoMovimientoCita } from '../../../enums';
import { coloresFondoEstadoAgenda, coloresFondoEstadoLlamada, coloresIconoEstadoAgenda, coloresIconoEstadoLlamada, esPar, obtenerIconoEstadoAgenda, obtenerIconoEstadoLlamada } from '../../../helpers';
import { gruposCitasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { CustomToolTip, Loader, ResponsiveDialog, ResponsiveDialogProps } from '../../UI';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        iconMargin: {
            marginRight: theme.spacing(),
            marginLeft: theme.spacing(),
        },
        paper: {
            padding: '6px 16px',
            display: "flex",
            flexDirection: "column",
        },
        alignSelfLeft: {
            alignSelf: "flex-start",
        },
        alignSelfRight: {
            alignSelf: "flex-end",
        },
        iconoLlamadaRealizada: {
            color: coloresIconoEstadoLlamada.realizado,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoLlamada.realizado,
        },
        iconoInicio: {
            color: "#000",
            borderColor: "transparent",
            backgroundColor: "#FFF",
        },
        iconoAgendada: {
            color: coloresIconoEstadoAgenda.agendado,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.agendado,
        },
        iconoAtendida: {
            color: coloresIconoEstadoAgenda.atendida,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.atendida,
        },
        iconoCancelada: {
            color: coloresIconoEstadoAgenda.cancelada,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.cancelada,
        },
        iconoReagendada: {
            color: coloresIconoEstadoAgenda.reagendada,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.reagendada,
        },
        iconoAgendadoConfirmado: {
            color: coloresIconoEstadoAgenda.agendado_confirmado,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.agendado_confirmado,
        },
        iconoAgendadoAtender: {
            color: coloresIconoEstadoAgenda.agendado_atender,
            borderColor: "transparent",
            backgroundColor: coloresFondoEstadoAgenda.agendado_atender,
        },
        ladoOpuestoTimeLine: {
            margin: "auto",
        },
    }),
);

export interface DialogMovimientosProps extends ResponsiveDialogProps {
    codigoGrupoCita: string,
    notifications: any,
}

function DialogMovimientos(props: DialogMovimientosProps) {
    const { open, onClose, codigoGrupoCita, notifications } = props;
    const classes = useStyles();
    const [isLoading, setIsLoading] = useState(false);
    const [activeStep, setActiveStep] = useState(0);
    const [movimientos, setMovimientos] = useState<VerMovimientosDto[]>([]);

    useEffect(() => {
        if (open) {
            cargarMovimientos(codigoGrupoCita);
        }
    }, [open]);

    const cargarMovimientos = (codigoGrupoCita: string) => {
        setIsLoading(true);
        gruposCitasService.getMovimientosDeSolicitud(codigoGrupoCita)
            .then(
                (movimientos: VerMovimientosDto[]) => {
                    setMovimientos(movimientos || []);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setIsLoading(false);
            });
    }

    const convertirFecha = (fecha: Date) => {
        return dayjs(fecha).format("DD MMMM");
    }

    const convertirFechaAgenda = (fecha: Date) => {
        return dayjs(fecha).format("DD MMM. YY");
    }

    const convertirFechaHoraAgenda = (fecha: Date) => {
        return dayjs(fecha).format("DD MMM. YYYY HH:mm:ss");
    }

    const convertirFechaHoraCorta = (fecha: Date) => {
        return dayjs(fecha).format("DD MMM. YYYY HH:mm");
    }

    const handleStep = (_activeStep: number) => () => {
        setActiveStep(_activeStep);
    };

    const cerrarPopup = () => {
        if (onClose) {
            onClose({}, "backdropClick");
        }
    }

    const renderIconEstadoAgenda = (tipoMovimiento: TipoMovimientoCita, estado: EstadoAgenda) => {
        switch (tipoMovimiento) {
            case TipoMovimientoCita.AGENDA:
                switch (estado) {
                    case EstadoAgenda.AGENDADO:
                        return (
                            <TimelineDot className={classes.iconoAgendada}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    case EstadoAgenda.ATENDIDA:
                        return (
                            <TimelineDot className={classes.iconoAtendida}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    case EstadoAgenda.CANCELADA:
                        return (
                            <TimelineDot className={classes.iconoCancelada}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    case EstadoAgenda.REAGENDADA:
                        return (
                            <TimelineDot className={classes.iconoReagendada}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    case EstadoAgenda.AGENDADO_CONFIRMADO:
                        return (
                            <TimelineDot className={classes.iconoAgendadoConfirmado}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    case EstadoAgenda.AGENDADO_ATENDER:
                        return (
                            <TimelineDot className={classes.iconoAgendadoAtender}>
                                {obtenerIconoEstadoAgenda(estado)}
                            </TimelineDot>
                        );

                    default:
                        return null;
                }

            case TipoMovimientoCita.LLAMADA:
                return (
                    <TimelineDot className={classes.iconoLlamadaRealizada}>
                        {obtenerIconoEstadoLlamada()}
                    </TimelineDot>
                );

            default:
                break;
        }
    }

    return (
        <ResponsiveDialog
            open={open}
            onClose={onClose}
            scroll="paper"
            aria-labelledby="movimiento-dialog-title"
            aria-describedby="movimiento-dialog-description"
            fullWidth={true}
            maxWidth={"md"}
        >
            <DialogTitle id="movimiento-dialog-title">Movimientos</DialogTitle>
            <DialogContent dividers={false}>
                <DialogContentText
                    id="movimiento-dialog-description"
                    component="div"
                >
                    {
                        isLoading ?
                            <Loader />
                            :
                            movimientos.length > 0 ?
                                <Timeline align="alternate">
                                    {movimientos?.map((datoMovimiento: VerMovimientosDto, index: number) => (
                                        <TimelineItem key={datoMovimiento.codigo}>
                                            <TimelineOppositeContent className={classes.ladoOpuestoTimeLine}>
                                                <Grid container spacing={2} alignItems={"center"}>
                                                    <Grid item container spacing={2} justify={esPar(index) ? "flex-end" : "flex-start"} alignItems={"center"}>
                                                        <CustomToolTip arrow title="Fecha de registro" placement="left">
                                                            <Bookmark fontSize="small" className={classes.iconMargin} />
                                                        </CustomToolTip>
                                                        <Typography variant="body2" color="textSecondary">
                                                            {convertirFechaHoraAgenda(datoMovimiento.fechaRegistro)}
                                                        </Typography>
                                                    </Grid>
                                                    <Grid item container spacing={2} justify={esPar(index) ? "flex-end" : "flex-start"} alignItems={"center"}>
                                                        <CustomToolTip arrow title="Última modificación" placement="left">
                                                            <Edit fontSize="small" className={classes.iconMargin} />
                                                        </CustomToolTip>
                                                        <Typography variant="body2" color="textSecondary">
                                                            {convertirFechaHoraAgenda(datoMovimiento.fechaUltimaModificacion)}
                                                        </Typography>
                                                    </Grid>
                                                </Grid>
                                            </TimelineOppositeContent>
                                            <TimelineSeparator>
                                                {
                                                    renderIconEstadoAgenda(datoMovimiento.tipoMovimiento, datoMovimiento.estado)
                                                }
                                                {index < movimientos.length - 1 ? <TimelineConnector /> : null}
                                            </TimelineSeparator>
                                            <TimelineContent>
                                                <Paper elevation={3} className={classes.paper}>
                                                    <Typography variant="h6">
                                                        {datoMovimiento.titulo}
                                                    </Typography>
                                                    <Typography variant="body1">
                                                        {datoMovimiento.descripcion}
                                                    </Typography>
                                                    <Typography variant="caption" color="textSecondary">
                                                        {`${datoMovimiento.apellidoEmpleadoAsignado} ${datoMovimiento.nombreEmpleadoAsignado}`}
                                                    </Typography>
                                                    <Typography variant="caption" color="textSecondary">
                                                        {`${convertirFechaHoraCorta(datoMovimiento.fechaInicio)} a ${convertirFechaHoraCorta(datoMovimiento.fechaFin)}`}
                                                    </Typography>
                                                    <div className={esPar(index) ? classes.alignSelfRight : classes.alignSelfLeft}>
                                                        <CustomToolTip arrow title={`${datoMovimiento.apellidoEmpleadoAsigna} ${datoMovimiento.nombreEmpleadoAsigna}`} placement={esPar(index) ? "right" : "left"}>
                                                            <Typography variant="caption" color="textSecondary">
                                                                {
                                                                    `Registrado por: `
                                                                }
                                                                <b>
                                                                    {datoMovimiento.usuarioAsigna}
                                                                </b>
                                                            </Typography>
                                                        </CustomToolTip>
                                                    </div>
                                                </Paper>
                                            </TimelineContent>
                                        </TimelineItem>
                                    ))}
                                </Timeline>
                                :
                                <Typography variant="caption" color="textSecondary">
                                    No se han registrado movimientos
                                </Typography>
                    }
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={cerrarPopup} color="primary">
                    Cerrar
                </Button>
            </DialogActions>
        </ResponsiveDialog>
    );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogMovimientos);
