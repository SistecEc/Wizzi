import { Button, ButtonBase, CircularProgress, createStyles, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, makeStyles, Paper, TextField } from '@material-ui/core';
import { blue, green, grey, red } from '@material-ui/core/colors';
import { Call, Cancel, Check, Phone } from '@material-ui/icons';
import clsx from 'clsx';
import React, { Fragment, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { agendasService, citasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog, ResponsiveDialogProps } from '../../UI';

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
        btnLlamar: {
            color: blue[500],
            "&:hover": {
                color: "white",
                backgroundColor: blue[500],
            }
        },
        btnConfirmar: {
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

export interface DialogConfirmarAsistenciaProps extends ResponsiveDialogProps {
    datosPrecargados: VerAgendaCitaMedicaDto,
    onLlamadaRegistrada: (datosPrecargados: VerAgendaCitaMedicaDto) => void,
    onConfirmarAsistencia: (datosPrecargados: VerAgendaCitaMedicaDto) => void,
    onCancelarCita: (reagendar: boolean, observacion: string) => void,
    notifications: any
}

function DialogConfirmarAsistencia(props: DialogConfirmarAsistenciaProps) {
    const { open, onClose, datosPrecargados, onLlamadaRegistrada, onConfirmarAsistencia, onCancelarCita, notifications } = props;
    const classes = useStyles();
    const [observacion, setObservacion] = useState('');
    const [registrandoLlamada, setRegistrandoLlamada] = useState(false);
    const [confirmandoAsistencia, setConfirmandoAsistencia] = useState(false);
    const [mostrarOpcionesCancelar, setMostrarOpcionesCancelar] = useState(false);

    const clickRegistrarLlamada = () => {
        if (puedeContinuar()) {
            setRegistrandoLlamada(true);
            const registrarLlamadaDto = {
                observacion
            };
            citasService.postRegistrarLlamadaCita(datosPrecargados.cita.codigo, registrarLlamadaDto)
                .then(
                    resultado => {
                        notifications.success("Se ha registrado la llamada correctamente");
                        onLlamadaRegistrada(datosPrecargados);
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                    setRegistrandoLlamada(false);
                    setObservacion("");
                    cerrarPopup();
                });
        } else {
            notifications.alert("Por favor ingrese la observación");
        }
    }

    const clickConfirmarAsistencia = () => {
        if (puedeContinuar()) {
            setConfirmandoAsistencia(true);
            const confirmarAsistenciaDto = {
                observacion
            };
            agendasService.putConfirmarAsistencia(datosPrecargados.codigo, confirmarAsistenciaDto)
                .then(
                    resultado => {
                        notifications.success("Se ha confirmado la asistencia correctamente");
                        onConfirmarAsistencia(datosPrecargados);
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                    setConfirmandoAsistencia(false);
                    setObservacion("");
                    cerrarPopup();
                });
        } else {
            notifications.alert("Por favor ingrese la observación");
        }
    }

    const clickCancelar = () => {
        if (puedeContinuar()) {
            setMostrarOpcionesCancelar(true);
        } else {
            notifications.alert("Por favor ingrese la observación");
        }
    }

    const clickReagendarCita = () => {
        onCancelarCita(true, observacion);
    }

    const clickNoReagendarCita = () => {
        onCancelarCita(false, observacion);
    }

    const onCloseOpcionesCancelar = () => {
        setMostrarOpcionesCancelar(false);
    }

    const puedeContinuar = () => {
        return observacion.trim() !== "";
    }

    const cerrarPopup = () => {
        if (onClose) {
            onClose({}, "backdropClick");
        }
    }

    return (
        <Fragment>
            <ResponsiveDialog
                open={open}
                onClose={onClose}
                aria-labelledby="atencion-dialog-title"
                aria-describedby="atencion-dialog-description"
                maxWidth={"md"}
                disableBackdropClick={registrandoLlamada || confirmandoAsistencia}
                disableEscapeKeyDown={registrandoLlamada || confirmandoAsistencia}
            >
                <DialogTitle id="atencion-dialog-title">
                    <Grid container justify="space-between">
                        <Grid item xs={6}>
                            Confirmar asistencia de cita
                        </Grid>
                        <Grid container item xs={6} spacing={1} alignItems="center" justify="flex-end">
                            <Call />
                            <Grid item>
                                {
                                    datosPrecargados.cita.cliente.telefono.startsWith("593") ?
                                        datosPrecargados.cita.cliente.telefono
                                        :
                                        `+593 ${datosPrecargados.cita.cliente.telefono}`
                                }
                            </Grid>
                        </Grid>
                    </Grid>
                </DialogTitle>
                <DialogContent dividers={false}>
                    <Grid container spacing={2} alignItems="center" justify="center">
                        <Grid item xs={12}>
                            <TextField
                                id="txtObservacion"
                                name="observacion"
                                label="Observación"
                                placeholder="Ingrese la observación de la llamada"
                                helperText="Se grabará la observación cuando escoja una acción a realizar"
                                multiline
                                rows={4}
                                variant="outlined"
                                value={observacion}
                                onChange={e => setObservacion(e.target.value)}
                                autoFocus
                                fullWidth
                                disabled={registrandoLlamada || confirmandoAsistencia}
                            />
                        </Grid>
                        <Grid container item xs={4} justify="center">
                            <ButtonBase
                                onClick={clickRegistrarLlamada}
                                disabled={registrandoLlamada || confirmandoAsistencia}
                            >
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnLlamar)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            {
                                                registrandoLlamada ?
                                                    <CircularProgress />
                                                    :
                                                    <Phone />
                                            }
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            {
                                                registrandoLlamada ?
                                                    "Registrando..."
                                                    :
                                                    "Registrar llamada"
                                            }
                                        </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                        <Grid container item xs={4} justify="center">
                            <ButtonBase
                                onClick={clickConfirmarAsistencia}
                                disabled={registrandoLlamada || confirmandoAsistencia}
                            >
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnConfirmar)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            {
                                                confirmandoAsistencia ?
                                                    <CircularProgress />
                                                    :
                                                    <Check />
                                            }
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            {
                                                confirmandoAsistencia ?
                                                    "Confirmando..."
                                                    :
                                                    "Confirmar asistencia"
                                            }
                                        </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                        <Grid container item xs={4} justify="center">
                            <ButtonBase
                                onClick={clickCancelar}
                                disabled={registrandoLlamada || confirmandoAsistencia}
                            >
                                <Paper elevation={1}>
                                    <Grid container className={clsx(classes.contenedorBoton, classes.btnCancelar)} alignItems="center" spacing={2}>
                                        <Grid item xs={3} sm={12}>
                                            <Cancel />
                                        </Grid>
                                        <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                            Cancelar cita
                                    </Grid>
                                    </Grid>
                                </Paper>
                            </ButtonBase>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={cerrarPopup}
                        color="primary"
                        disabled={registrandoLlamada || confirmandoAsistencia}
                    >
                        Cerrar
                </Button>
                </DialogActions>
            </ResponsiveDialog>
            <Dialog
                open={mostrarOpcionesCancelar}
                onClose={onCloseOpcionesCancelar}
                aria-labelledby="opcionesCancelar-dialog-title"
                aria-describedby="opcionesCancelar-dialog-description"
            >
                <DialogTitle id="opcionesCancelar-dialog-title">
                    ¿Desea reagendar la cita?
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="opcionesCancelar-dialog-description">
                        Se cancelará la agenda y se podrá atender nuevamente desde la solicitud de cita
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Grid container direction={"row"} justify={"space-between"}>
                        <Button onClick={onCloseOpcionesCancelar} color="secondary">
                            Cancelar
                        </Button>
                        <div>
                            <Button onClick={clickNoReagendarCita} color="primary">
                                No Reagendar
                            </Button>
                            <Button onClick={clickReagendarCita} color="primary" autoFocus>
                                Reagendar
                            </Button>
                        </div>
                    </Grid>
                </DialogActions>
            </Dialog>
        </Fragment>
    )
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogConfirmarAsistencia);
