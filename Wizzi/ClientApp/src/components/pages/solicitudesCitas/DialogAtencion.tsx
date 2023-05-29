import { Button, ButtonBase, createStyles, DialogActions, DialogContent, DialogTitle, Grid, makeStyles, Paper } from '@material-ui/core';
import { blue, green, grey, red } from '@material-ui/core/colors';
import { Call, Cancel, Event, History } from '@material-ui/icons';
import clsx from 'clsx';
import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerSolicitudCitaMedicaDto } from '../../../dtos/SolicitudesCitasMedicas';
import { DocumentoAligar, TiposForm } from '../../../enums';
import { DatosPrecargadosRegistrar } from '../../../models';
import { citasService } from '../../../services';
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

export interface DialogAtencionProps extends ResponsiveDialogProps {
    datosPrecargados: VerSolicitudCitaMedicaDto,
    onLlamadaRegistrada: () => void,
    onSolicitudAgendada: (codigoSolicitud: string) => void,
    onSolicitudEliminada: (codigoSolicitud: string) => void,
    notifications: any,
}

function DialogAtencion(props: DialogAtencionProps) {
    const {
        open,
        onClose,
        datosPrecargados,
        onLlamadaRegistrada,
        onSolicitudAgendada,
        onSolicitudEliminada,
        notifications,
    } = props;
    const [isOpenDialogAtender, setIsOpenDialogAtender] = useState(open);
    const [popUpCitaAbierto, setPopUpCitaAbierto] = useState(false);
    const [popupRegistrarLlamadaAbierto, setPopupRegistrarLlamadaAbierto] = useState(false);
    const [isOpenDialogMovimientos, setIsOpenDialogMovimientos] = useState(false);
    const [datosClienteRegistrar, setDatosClienteRegistrar] = useState<DatosPrecargadosRegistrar | null>(null);

    const classes = useStyles();
    const citaCreada = datosPrecargados.cita ? true : false;

    useEffect(() => {
        setIsOpenDialogAtender(open);
        if (open) {
            setDatosClienteRegistrar({
                nombre: datosPrecargados.nombrePaciente,
                apellido: datosPrecargados.apellidoPaciente,
                telefono: datosPrecargados.telefonoPaciente,
                email: datosPrecargados.emailPaciente,
                genero: datosPrecargados.generoPaciente,
                fechaNacimiento: datosPrecargados.fechaNacimiento,
                localizacion: {
                    pais: datosPrecargados.localizacion.pais.codigo,
                    provincia: datosPrecargados.localizacion.provincia.codigo,
                    canton: datosPrecargados.localizacion.canton.codigo,
                    parroquia: datosPrecargados.localizacion.parroquia?.codigo,
                },
            });
        }
    }, [open])

    const clickVerHistorial = () => {
        setIsOpenDialogMovimientos(true);
        cerrarPopupAtender();
    }

    const dialogMovimientosCerrado = () => {
        setIsOpenDialogMovimientos(false);
        setIsOpenDialogAtender(true);
    }

    const clickRegistrarLlamada = () => {
        setPopupRegistrarLlamadaAbierto(true);
        setIsOpenDialogAtender(false);
    }

    const clickAgendar = () => {
        setPopUpCitaAbierto(true);
        cerrarPopupAtender();
    }

    const popupCitaCerrado = () => {
        setPopUpCitaAbierto(false);
        setIsOpenDialogAtender(true);
    }

    const popupCitaCancelado = () => {
        setPopUpCitaAbierto(false);
    };

    const citaAgendada = (codigoGrupoCitas: string) => {
        setPopUpCitaAbierto(false);
    }

    const solicitudAgendada = (codigoSolicitud: string) => {
        setPopUpCitaAbierto(false);
        onSolicitudAgendada(codigoSolicitud);
    }

    const popupRegistrarLlamadaCerrado = () => {
        setPopupRegistrarLlamadaAbierto(false);
        setIsOpenDialogAtender(true);
    }

    const clickCancelar = () => {
        citasService.deleteSolicitudCita(datosPrecargados.codigo)
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
                open={isOpenDialogMovimientos}
                onClose={dialogMovimientosCerrado}
                codigoGrupoCita={datosPrecargados.codigo}
            />
            <DialogFormCita
                open={popUpCitaAbierto}
                onClose={popupCitaCerrado}
                codigoGrupoCitas={datosPrecargados.codigo}
                documentoAligar={DocumentoAligar.SOLICITUD}
                datosClienteRegistrar={datosClienteRegistrar ?? undefined}
                fechaTentativa={datosPrecargados.fechaTentativa}
                modo={TiposForm.NUEVO}
                onCitaAgendada={citaAgendada}
                onSolicitudAgendada={solicitudAgendada}
                onCancelClick={popupCitaCancelado}
            />
            <DialogRegistrarLlamada
                open={popupRegistrarLlamadaAbierto}
                onClose={popupRegistrarLlamadaCerrado}
                onLlamadaRegistrada={onLlamadaRegistrada}
                codigoProceso={datosPrecargados.codigo}
                numeroTelefono={datosPrecargados.esPaciente ? datosPrecargados.telefonoPaciente : datosPrecargados.telefonoRepresentante}
                documentoAligar={DocumentoAligar.SOLICITUD}
                esNuevoInicioProceso={false}
            />
        </>
    )
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogAtencion);