import { Button, DialogActions, DialogContent, DialogTitle, Grid, TextField } from '@material-ui/core';
import { Call } from '@material-ui/icons';
import React, { useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { RegistrarLlamadaDto } from '../../../dtos/Llamadas';
import { DocumentoAligar } from '../../../enums';
import { citasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog, ResponsiveDialogProps } from '../../UI';
export interface DialogRegistrarLlamada extends ResponsiveDialogProps {
    codigoProceso: string,
    numeroTelefono: string,
    onLlamadaRegistrada: () => void,
    documentoAligar: DocumentoAligar,
    esNuevoInicioProceso: boolean,
    notifications: any,
}

function DialogRegistrarLlamada(props: DialogRegistrarLlamada) {
    const {
        open,
        onClose,
        codigoProceso,
        numeroTelefono,
        onLlamadaRegistrada,
        documentoAligar,
        esNuevoInicioProceso,
        notifications
    } = props;
    const [observacion, setObservacion] = useState('Llamada no atendida');
    const [grabando, setGrabando] = useState(false);

    const clickRegistrarLlamada = () => {
        setGrabando(true);
        const registrarLlamadaDto: RegistrarLlamadaDto = {
            observacion,
            esNuevoInicioProceso,
        };
        if (documentoAligar == DocumentoAligar.CITA) {
            citasService.postRegistrarLlamadaCita(codigoProceso, registrarLlamadaDto)
                .then(
                    resultado => {
                        notifications.success("Se ha registrado la llamada correctamente");
                        onLlamadaRegistrada();
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                    setGrabando(false);
                    cerrarPopup();
                });
        } else {
            citasService.postRegistrarLlamadaSolicitudCita(codigoProceso, registrarLlamadaDto)
                .then(
                    resultado => {
                        notifications.success("Se ha registrado la llamada correctamente");
                        onLlamadaRegistrada();
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                    setGrabando(false);
                    cerrarPopup();
                });
        }
    }

    const cerrarPopup = () => {
        if (onClose) {
            onClose({}, "backdropClick");
        }
    }

    return (
        <ResponsiveDialog
            open={open}
            onClose={onClose}
            aria-labelledby="llamada-dialog-title"
            aria-describedby="llamada-dialog-description"
            fullWidth={true}
            maxWidth={"md"}
            disableBackdropClick={grabando}
            disableEscapeKeyDown={grabando}
        >
            <DialogTitle id="llamada-dialog-title">
                <Grid container justify="space-between">
                    <Grid item xs={6}>
                        Registrar llamada
                    </Grid>
                    <Grid container item xs={6} spacing={1} alignItems="center" justify="flex-end">
                        <Call />
                        <Grid item>
                            {
                                numeroTelefono.startsWith("593") ?
                                    numeroTelefono
                                    :
                                    `+593 ${numeroTelefono}`
                            }
                        </Grid>
                    </Grid>
                </Grid>
            </DialogTitle>
            <DialogContent dividers={false}>
                <TextField
                    id="txtObservacion"
                    name="observacion"
                    label="Observación"
                    multiline
                    rows={4}
                    variant="outlined"
                    value={observacion}
                    onChange={e => setObservacion(e.target.value)}
                    autoFocus
                    fullWidth
                    disabled={grabando}
                />
            </DialogContent>
            <DialogActions>
                <Button
                    variant="text"
                    onClick={cerrarPopup}
                    color="secondary"
                    disabled={grabando}
                >
                    Cancelar
                </Button>
                <Button
                    onClick={clickRegistrarLlamada}
                    color="primary"
                    disabled={grabando}
                >
                    {
                        grabando ?
                            "Registrando..."
                            :
                            "Registrar"
                    }
                </Button>
            </DialogActions>
        </ResponsiveDialog>
    )
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogRegistrarLlamada);
