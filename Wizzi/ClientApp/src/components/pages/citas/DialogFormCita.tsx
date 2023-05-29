import { Button, CircularProgress, createStyles, DialogActions, DialogContent, DialogTitle, Grid, makeStyles, Step, StepLabel, Stepper } from '@material-ui/core';
import React, { Fragment, useEffect, useRef, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { AgendarCitaDto } from '../../../dtos/Agendas';
import { VerClienteDto } from '../../../dtos/Clientes';
import { DocumentoAligar, TiposEdicion, TiposForm } from '../../../enums';
import { DatosPrecargadosRegistrar, ModeloAgenda } from '../../../models';
import { citasService, clientesService, gruposCitasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog, ResponsiveDialogProps } from '../../UI';
import FormCliente from '../clientes/FormCliente';
import FormAgendar from './FormAgendar';

const useStyles = makeStyles((theme) =>
    createStyles({
        root: {
            width: '100%',
        },
        stepperRoot: {
            padding: theme.spacing(),
        },
        circularInsideButton: {
            marginRight: theme.spacing(),
        },
    }),
);

function getSteps() {
    return ['Paciente', 'Cita'];
}

export interface DialogFormCitaProps extends ResponsiveDialogProps {
    codigoGrupoCitas: string,
    documentoAligar: DocumentoAligar,
    esNuevoInicioProceso?: boolean,
    datosClienteRegistrar?: DatosPrecargadosRegistrar,
    fechaTentativa?: Date,
    modo: TiposForm,
    codigoClienteCargar?: string,
    onCitaAgendada: (codigoGrupoCitas: string) => void,
    onSolicitudAgendada: (codigoSolicitud: string) => void,
    onCancelClick: () => void,
    notifications: any,
}

function DialogFormCita(props: DialogFormCitaProps) {
    const {
        open,
        onClose,
        codigoGrupoCitas,
        documentoAligar,
        esNuevoInicioProceso,
        datosClienteRegistrar,
        fechaTentativa,
        modo,
        codigoClienteCargar,
        onCitaAgendada,
        onSolicitudAgendada,
        onCancelClick,
        notifications,
    } = props;
    let DialogFormCitaRef = useRef(null);
    const [modoFormCliente, setModoFormCliente] = useState(TiposForm.BUSCADOR);
    const [pasosCompletados, setPasosCompletados] = useState<number[]>([]);
    const [activeStep, setActiveStep] = useState(0);
    const [grabando, setGrabando] = useState(false);
    const [datosClienteLigar, setDatosClienteLigar] = useState<VerClienteDto | null>();
    const [agendaCreada, setAgendaCreada] = useState<ModeloAgenda | null>(null);


    const classes = useStyles();

    useEffect(() => {
        if (open) {
            if (modo === TiposForm.EDICION) {
                setModoFormCliente(TiposForm.VISUALIZAR);
                clientesService.getCliente(codigoClienteCargar)
                    .then(
                        datosCliente => {
                            cargarCliente(datosCliente);
                        },
                        error => {
                            notifications.error(error);
                        });
            }
        }
    }, [open]);

    const cargarCliente = (datosClienteLigar: VerClienteDto) => {
        setDatosClienteLigar(datosClienteLigar);
        setModoFormCliente(TiposForm.VISUALIZAR);
        completarPasoActual();
    }

    const completarPasoActual = () => {
        setPasosCompletados([...pasosCompletados, activeStep]);
    }

    const quitarCompletoPasoActual = () => {
        setPasosCompletados(pasosCompletados.filter(pc => pc !== activeStep));
    }

    const cambioModoFormRegistroPaciente = (modo: TiposForm) => {
        switch (modo) {
            case TiposForm.EDICION:
                quitarCompletoPasoActual()
                break;

            case TiposForm.VISUALIZAR:
                completarPasoActual()
                break;

            default:
        }
    }

    const onAgendaCreada = (datosAgenda: ModeloAgenda) => {
        setAgendaCreada(datosAgenda);
        completarPasoActual();
    }

    const agendaModificada = (datosAgenda: ModeloAgenda, tipo: TiposEdicion) => {
        switch (tipo) {
            case TiposEdicion.MODIFICADO:
                setAgendaCreada({ ...agendaCreada, ...datosAgenda })
                break;

            case TiposEdicion.ELIMINADO:
                setAgendaCreada(null);
                quitarCompletoPasoActual();
                break;
        }
    }

    const getStepContent = (step: number) => {
        switch (step) {
            case 0:
                return (
                    <FormCliente
                        modoForm={modoFormCliente}
                        datosPrecargados={datosClienteLigar}
                        buscarAlMontar={`${datosClienteRegistrar?.nombre} ${datosClienteRegistrar?.apellido}`}
                        onCambioModoForm={cambioModoFormRegistroPaciente}
                        onClienteSeleccionado={cargarCliente}
                        onClienteEditado={cargarCliente}
                        onClienteRegistrado={cargarCliente}
                        datosPrecargadosRegistrar={datosClienteRegistrar}
                    />
                );

            case 1:
                return (
                    <FormAgendar
                        refOverlay={DialogFormCitaRef}
                        onAgendaCreada={onAgendaCreada}
                        onAgendaModificada={agendaModificada}
                        fechaResaltar={fechaTentativa}
                    />
                );
            default:
                return 'Desconocido';
        }
    }

    const anteriorPaso = () => {
        setActiveStep(activeStep - 1);
    }

    const siguientePaso = () => {
        setActiveStep(activeStep + 1);
    }

    const grabarCita = () => {
        if (datosClienteLigar && agendaCreada) {
            setGrabando(true);
            const datosEnviar: AgendarCitaDto = {
                cliente: datosClienteLigar.codigo,
                agenda: {
                    fechaInicio: agendaCreada.startDate,
                    fechaFin: agendaCreada.endDate,
                    titulo: agendaCreada.title,
                    esTodoElDia: agendaCreada.allDay,
                    reglaRecurrencia: agendaCreada.rRule,
                    fechasExluidasRecurrencia: agendaCreada.exDate,
                    descripcion: agendaCreada.notes,
                    doctorAtiende: agendaCreada.codigoEmpleado,
                    tipoCitaMedica: agendaCreada.tipoCitaMedica,
                    fuenteRemision: agendaCreada.fuenteRemision,
                },
                esNuevoInicioProceso: esNuevoInicioProceso ?? false,
            };
            if (documentoAligar == DocumentoAligar.SOLICITUD) {
                citasService.postAgendarSolicitudCita(codigoGrupoCitas, datosEnviar)
                    .then(
                        citaGrabada => {
                            notifications.success("Se ha citado la solicitud de cita correctamente");
                            onSolicitudAgendada(codigoGrupoCitas);
                        },
                        error => {
                            notifications.error(error);
                        })
                    .finally(() => {
                        setGrabando(false);
                    });
            } else {
                gruposCitasService.postAgendarCita(codigoGrupoCitas, datosEnviar)
                    .then(
                        citaAgendada => {
                            notifications.success("Se ha agendado la cita correctamente");
                            onCitaAgendada(codigoGrupoCitas);
                        },
                        error => {
                            notifications.error(error);
                        })
                    .finally(() => {
                        setGrabando(false);
                    });
            }
        } else {
            notifications.error("Por favor complete todos los datos de cliente y agenda");
        }
    }

    const steps = getSteps();
    return (
        <ResponsiveDialog
            aria-labelledby="tituloDialogCita"
            open={open}
            onClose={onClose}
            fullWidth={true}
            maxWidth="lg"
        >
            <DialogTitle id="tituloDialogCita">
                {
                    modo === TiposForm.NUEVO ?
                        `Agendar cita`
                        :
                        `Editar cita`
                }
            </DialogTitle>
            <DialogContent>
                <div className={classes.root}>
                    <Stepper activeStep={activeStep} alternativeLabel classes={{ root: classes.stepperRoot }}>
                        {steps.map((label, i) => {
                            return (
                                <Step key={i}>
                                    <StepLabel>{label}</StepLabel>
                                </Step>
                            );
                        })}
                    </Stepper>
                    <div>
                        {getStepContent(activeStep)}
                    </div>
                </div>
            </DialogContent>
            {
                pasosCompletados.indexOf(activeStep) >= 0 ?
                    <DialogActions>
                        <Grid container direction={"row"} justify={"space-between"}>
                            <Button onClick={onCancelClick} color="secondary">
                                Cancelar
                            </Button>
                            <div>
                                <Button disabled={grabando || activeStep === 0} onClick={anteriorPaso} color="secondary">
                                    Regresar
                                    </Button>
                                {
                                    activeStep === steps.length - 1 ?
                                        <Button onClick={grabarCita} color="primary" disabled={grabando} autoFocus>
                                            {grabando ?
                                                <Fragment>
                                                    <CircularProgress size={14} color="primary" className={classes.circularInsideButton} />
                                                        Grabando...
                                                    </Fragment>
                                                :
                                                'Grabar'}
                                        </Button>
                                        :
                                        <Button onClick={siguientePaso} color="primary" autoFocus>
                                            Siguiente
                                        </Button>
                                }
                            </div>
                        </Grid>
                    </DialogActions>
                    :
                    null
            }
        </ResponsiveDialog>
    );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogFormCita);
