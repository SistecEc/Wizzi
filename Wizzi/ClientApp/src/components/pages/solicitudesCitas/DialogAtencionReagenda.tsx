
import { Button, ButtonBase, createStyles, DialogActions, DialogContent, DialogTitle, Grid, makeStyles, Paper } from '@material-ui/core';
import { blue, green, grey, red } from '@material-ui/core/colors';
import { Call, Cancel, Event, History } from '@material-ui/icons';
import clsx from 'clsx';
import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { DocumentoAligar, TiposForm } from '../../../enums';
import { agendasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog, ResponsiveDialogProps } from '../../UI';
import DialogFormCita from '../citas/DialogFormCita';
import DialogMovimientos from './DialogMovimientos';
import DialogRegistrarLlamada from './DialogRegistrarLlamada';

const useStyles = makeStyles((theme) =>
    createStyles({
        contenedorBoton: {
            margin: 0,
            width: 125,
            [theme.breakpoints.down('xs')]: {
                width: 200,
            },
            borderRadius: "inherit",
            "&:hover": {
                backgroundColor: grey[100],
            }
        },
        textoBoton: {
            [theme.breakpoints.down('xs')]: {
                textAlign: "left",
            },
        },
        btnHistorial: {
            color: grey[500],
            "&:hover": {
                color: "white",
                backgroundColor: grey[500],
            }
        },
        btnLlamada: {
            color: blue[500],
            "&:hover": {
                color: "white",
                backgroundColor: blue[500],
            }
        },
        btnAgendaCita: {
            color: green[500],
            "&:hover": {
                color: "white",
                backgroundColor: green[500],
            }
        },
        btnCancelar: {
            color: red[500],
            "&:hover": {
                color: "white",
                backgroundColor: red[500],
            }
        },
    }),
);

export interface DialogAtencionReagendaProps extends ResponsiveDialogProps {
    datosPrecargados: VerAgendaCitaMedicaDto,
    onLlamadaRegistrada: () => void,
    onProcesoReagendado: (codigo: string) => void,
    onSolicitudEliminada: (codigo: string) => void,
    notifications: any,
}

function DialogAtencionReagenda(props: DialogAtencionReagendaProps) {
    const {
        open,
        onClose,
        datosPrecargados,
        onLlamadaRegistrada,
        onProcesoReagendado,
        onSolicitudEliminada,
        notifications,
    } = props;
    const [isOpenDialogAtender, setIsOpenDialogAtender] = useState(open);
    const [isOpenMovimientos, setIsOpenMovimientos] = useState(false);
    const [isOpenRegistrarLlamada, setIsOpenRegistrarLlamada] = useState(false);
    const [isOpenFormCita, setIsOpenFormCita] = useState(false);
    const classes = useStyles();
    const { cita } = datosPrecargados;
    const citaCreada = cita ? true : false;

    useEffect(() => {
        setIsOpenDialogAtender(open);
    }, [open])

    const clickVerHistorial = (e: any) => {
        setIsOpenMovimientos(true);
        cerrarPopupAtender();
    }

    const movimientosCerrado = () => {
        setIsOpenMovimientos(false);
        setIsOpenDialogAtender(true);
    }

    const clickRegistrarLlamada = () => {
        setIsOpenRegistrarLlamada(true);
        cerrarPopupAtender();
    }

    const registrarLlamadaCerrado = () => {
        setIsOpenRegistrarLlamada(false);
        setIsOpenDialogAtender(true);
    }

    //#region  Agendamiento
    const clickAgendar = () => {
        setIsOpenFormCita(true);
        cerrarPopupAtender();
    }

    const formCitaCerrado = () => {
        setIsOpenFormCita(false);
        setIsOpenDialogAtender(true);
    }

    const citaAgendada = (codigoGrupoCitas: string) => {
        setIsOpenFormCita(false);
        setIsOpenDialogAtender(true);
    }


    const solicitudAgendada = (codigoSolicitud: string) => {
        setIsOpenFormCita(false);
        onProcesoReagendado(codigoSolicitud);
    }

    //#endregion


    const clickCitar = () => {
        cerrarPopupAtender();
    }

    const clickCancelar = () => {
        const codigoGrupo = datosPrecargados.codigo;
        const cancelarAgendaDto = {
            reagendar: false,
            desdeLlamada: false,
            observacion: '',
        };
        agendasService.PutCancelarAgenda(codigoGrupo, cancelarAgendaDto)
            .then(
                resultados => {
                    notifications.success("Se ha eliminado la solicitud correctamente");
                    onSolicitudEliminada(datosPrecargados.codigo);
                },
                error => {
                    notifications.error(error);
                });
    }

    const cerrarPopupAtender = () => {
        setIsOpenDialogAtender(false);
    }

    const cerrarPopupYdispararOnClose = () => {
        cerrarPopupAtender();
        if (onClose) {
            onClose({}, "backdropClick");
        }
    }

    return (
        <>
            <ResponsiveDialog
                open={isOpenDialogAtender}
                onClose={onClose}
                aria-labelledby="atencion-dialog-title"
                aria-describedby="atencion-dialog-description"
                fullWidth={true}
                maxWidth={"md"}
            >
                <DialogTitle id="atencion-dialog-title">
                    Escoja la acción a realizar
                </DialogTitle>
                <DialogContent dividers={false}>
                    <Grid container spacing={2} alignItems="center" justify="center">
                        <Grid container item xs={12} sm={3} md={2} justify="center">
                            <ButtonBase onClick={clickVerHistorial}>
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnHistorial)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            <History />
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            Historial
                                    </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                        <Grid container item xs={12} sm={3} md={2} justify="center">
                            <ButtonBase onClick={clickRegistrarLlamada}>
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnLlamada)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            <Call />
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            Registrar llamada
                                    </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                        <Grid container item xs={12} sm={3} md={2} justify="center">
                            {
                                citaCreada ?
                                    <ButtonBase onClick={clickAgendar}>
                                        <Paper elevation={1}>
                                            <Grid container className={clsx(classes.contenedorBoton, classes.btnAgendaCita)} alignItems="center" spacing={2}>
                                                <Grid item xs={3} sm={12}>
                                                    <Event />
                                                </Grid>
                                                <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                                    Agendar
                                                </Grid>
                                            </Grid>
                                        </Paper>
                                    </ButtonBase>
                                    :
                                    <ButtonBase onClick={clickCitar}>
                                        <Paper elevation={1}>
                                            <Grid container className={clsx(classes.contenedorBoton, classes.btnAgendaCita)} alignItems="center" spacing={2}>
                                                <Grid item xs={3} sm={12}>
                                                    <Event />
                                                </Grid>
                                                <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                                    Citar
                                                </Grid>
                                            </Grid>
                                        </Paper>
                                    </ButtonBase>
                            }
                        </Grid>
                        <Grid container item xs={12} sm={3} md={2} justify="center">
                            <ButtonBase onClick={clickCancelar}>
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnCancelar)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            <Cancel />
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            Cancelar solicitud
                                    </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={cerrarPopupYdispararOnClose} color="primary">
                        Cerrar
                    </Button>
                </DialogActions>
            </ResponsiveDialog>
            <DialogMovimientos
                open={isOpenMovimientos}
                onClose={movimientosCerrado}
                codigoGrupoCita={cita.solicitud ? cita.solicitud : datosPrecargados.codigo}
            />
            <DialogRegistrarLlamada
                open={isOpenRegistrarLlamada}
                onClose={registrarLlamadaCerrado}
                onLlamadaRegistrada={onLlamadaRegistrada}
                codigoProceso={datosPrecargados.cita.codigo}
                numeroTelefono={datosPrecargados.cita.cliente.telefono}
                documentoAligar={DocumentoAligar.CITA}
                esNuevoInicioProceso={true}
            />
            <DialogFormCita
                open={isOpenFormCita}
                onClose={formCitaCerrado}
                codigoGrupoCitas={cita.solicitud ? cita.solicitud : datosPrecargados.codigo}
                documentoAligar={DocumentoAligar.CITA}
                esNuevoInicioProceso={true}
                modo={TiposForm.EDICION}
                codigoClienteCargar={cita.cliente.codigo}
                onCitaAgendada={citaAgendada}
                onSolicitudAgendada={solicitudAgendada}
                onCancelClick={formCitaCerrado}
            />
        </>
    )
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogAtencionReagenda);
