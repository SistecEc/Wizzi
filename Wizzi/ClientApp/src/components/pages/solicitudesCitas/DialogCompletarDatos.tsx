import { Button, ButtonBase, CircularProgress, createStyles, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, Grid, makeStyles, Paper, TextField } from '@material-ui/core';
import { blue, green, grey, red } from '@material-ui/core/colors';
import { EventBusy, Person } from '@material-ui/icons';
import clsx from 'clsx';
import React, { Fragment, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { agendasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { Loader, ResponsiveDialog, ResponsiveDialogProps } from '../../UI';

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

export interface DialogCompletarDatosProps extends ResponsiveDialogProps {
    onClickCompletarDatos: () => void,
    onInasistenciaRegistrada: (reagendar: boolean) => void,
    datosPrecargados: VerAgendaCitaMedicaDto,
    mostrarProcesando: boolean,
    notifications: any
}

function DialogCompletarDatos(props: DialogCompletarDatosProps) {
    const { open, onClose, onClickCompletarDatos, onInasistenciaRegistrada, datosPrecargados, mostrarProcesando, notifications, } = props;
    const classes = useStyles();
    const [observacion, setObservacion] = useState('');
    const [mostrarOpcionesNoAsistencia, setMostrarOpcionesNoAsistencia] = useState(false);
    const [grabando, setGrabando] = useState(false);

    const clickCompletarDatos = () => {
        onClickCompletarDatos();
    }

    const clickNoAsistencia = () => {
        setMostrarOpcionesNoAsistencia(true);
    }

    const clickReagendarCita = () => {
        if (puedeContinuar()) {
            registrarInasistencia(true);
        } else {
            notifications.alert("Por favor ingrese la observación");
        }
    }

    const clickNoReagendarCita = () => {
        if (puedeContinuar()) {
            registrarInasistencia(false);
        } else {
            notifications.alert("Por favor ingrese la observación");
        }
    }

    const registrarInasistencia = (reagendar: boolean) => {
        setGrabando(true);

        const registrarInasistenciaDto = {
            reagendar,
            observacion,
        };
        agendasService.putRegistrarInasistencia(datosPrecargados.codigo, registrarInasistenciaDto)
            .then(
                resultado => {
                    notifications.success("Se ha registrado la inasistencia correctamente");
                    onInasistenciaRegistrada(reagendar);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setGrabando(true);
                setObservacion("");
                cerrarPopup();
            });
    }

    const onCloseOpcionesNoAsistencia = () => {
        setMostrarOpcionesNoAsistencia(false);
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
                aria-labelledby="completarDatos-dialog-title"
                aria-describedby="completarDatos-dialog-description"
                maxWidth={"md"}
                disableEscapeKeyDown={mostrarProcesando}
                disableBackdropClick={mostrarProcesando}
            >
                <DialogTitle id="completarDatos-dialog-title">
                    Completar datos del cliente
                </DialogTitle>
                <DialogContent dividers={false}>
                    {
                        mostrarProcesando ?
                            <Loader />
                            :
                            <Grid container spacing={2} alignItems="center" justify="center">
                                <Grid container item xs={6} justify="center">
                                    <ButtonBase
                                        onClick={clickCompletarDatos}
                                    >
                                        <Paper elevation={1}>
                                            <Grid container className={clsx(classes.contenedorBoton, classes.btnConfirmar)} alignItems="center" spacing={2}>
                                                <Grid item xs={3} sm={12}>
                                                    <Person />
                                                </Grid>
                                                <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                                    Completar datos
                                                </Grid>
                                            </Grid>
                                        </Paper>
                                    </ButtonBase>
                                </Grid>
                                <Grid container item xs={6} justify="center">
                                    <ButtonBase
                                        onClick={clickNoAsistencia}
                                    >
                                        <Paper elevation={1}>
                                            <Grid container className={clsx(classes.contenedorBoton, classes.btnCancelar)} alignItems="center" spacing={2}>
                                                <Grid item xs={3} sm={12}>
                                                    <EventBusy />
                                                </Grid>
                                                <Grid item xs={9} sm={12} className={classes.textoBoton}>
                                                    Registrar inasistencia
                                                </Grid>
                                            </Grid>
                                        </Paper>
                                    </ButtonBase>
                                </Grid>
                            </Grid>
                    }
                </DialogContent>
                <DialogActions>
                    <Button
                        onClick={cerrarPopup}
                        color="primary"
                        disabled={mostrarProcesando}
                    >
                        Cerrar
                </Button>
                </DialogActions>
            </ResponsiveDialog>
            <Dialog
                open={mostrarOpcionesNoAsistencia}
                onClose={onCloseOpcionesNoAsistencia}
                aria-labelledby="opcionesNoAsistencia-dialog-title"
                aria-describedby="opcionesNoAsistencia-dialog-description"
                disableBackdropClick={grabando}
                disableEscapeKeyDown={grabando}
            >
                <DialogTitle id="opcionesNoAsistencia-dialog-title">
                    ¿Desea reagendar la cita?
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="opcionesNoAsistencia-dialog-description">
                        Se cancelará la agenda y se podrá atender nuevamente desde la solicitud de cita
                    </DialogContentText>
                    <Grid container spacing={2} alignItems="center" justify="center">
                        <Grid item xs={12}>
                            <TextField
                                id="txtObservacion"
                                name="observacion"
                                label="Observación"
                                placeholder="Ingrese una observación"
                                helperText="Se grabará la observación cuando escoja una acción a realizar"
                                multiline
                                rows={4}
                                variant="outlined"
                                value={observacion}
                                onChange={e => setObservacion(e.target.value)}
                                autoFocus
                                fullWidth
                                disabled={grabando}
                            />
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Grid container direction={"row"} justify={"space-between"}>
                        <Button
                            onClick={onCloseOpcionesNoAsistencia}
                            color="secondary"
                            disabled={grabando}
                        >
                            Cancelar
                        </Button>
                        {
                            grabando ?
                                <CircularProgress />
                                :
                                <div>
                                    <Button onClick={clickNoReagendarCita} color="primary">
                                        No Reagendar
                                    </Button>
                                    <Button onClick={clickReagendarCita} color="primary" autoFocus>
                                        Reagendar
                                    </Button>
                                </div>
                        }

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

export default connect(null, mapDispatchToProps)(DialogCompletarDatos);
