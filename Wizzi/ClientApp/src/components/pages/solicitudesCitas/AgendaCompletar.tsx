import { Accordion, AccordionDetails, AccordionSummary, Badge, Button, Card, CardContent, createStyles, Grid, IconButton, makeStyles, Typography } from '@material-ui/core';
import { green, red } from '@material-ui/core/colors';
import { AssignmentInd, Bookmark, Call, CallToAction, Close, Directions, Done, Edit, ExpandMore, History, Mail, Phone, SwapVert, Wc } from '@material-ui/icons';
import dayjs from 'dayjs';
import React, { Fragment, MouseEvent, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { VerCitaMedicaDto, VerClienteDto } from '../../../dtos/CitasMedicas';
import { EstadoAgenda, FormaCompletarAgenda } from '../../../enums';
import { obtenerColorXestado, obtenerDescripcionXestado, obtenerIconoEstadoAgenda } from '../../../helpers';
import { from_VerAgendaCitaMedicaDto_to_ModeloAgendaGrabada } from '../../../mappers';
import { ModeloAgendaGrabada } from '../../../models/modeloAgendaGrabada';
import { agendasService, citasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { Loader } from '../../UI';
import DialogFormReagendar from '../citas/DialogFormReagendar';
import DialogEditarCliente from '../clientes/DialogEditarCliente';
import DialogCompletarDatos from './DialogCompletarDatos';
import DialogConfirmarAsistencia from './DialogConfirmarAsistencia';
import DialogMovimientos from './DialogMovimientos';

const useStyles = makeStyles((theme) =>
    createStyles({
        agendado: {
            color: green[400],
        },
        sinAgendar: {
            color: red[400],
        },
        iconMargin: {
            marginRight: theme.spacing(),
        },
        btnCancelarCita: {
            backgroundColor: red[400],
        },
        circularProgress: {
            marginRight: theme.spacing(),
        },
        btnResumen: {
            marginRight: 10,
        },
    }),
);

export interface AgendaCompletarProps {
    info: VerAgendaCitaMedicaDto;
    onDatosCompletados: () => void;
    onAgendaCancelada: (reagendar: boolean) => void;
    onAsistenciaConfirmada: () => void;
    onAgendaEditada: (codigoAgenda: string) => void;
    onLlamadaRegistrada: (codigoAgenda: string) => void;
    formaCompletar: FormaCompletarAgenda,
    notifications: any,
}

function AgendaCompletar(props: AgendaCompletarProps) {
    const {
        info,
        onDatosCompletados,
        onAgendaCancelada,
        onAsistenciaConfirmada,
        onAgendaEditada,
        onLlamadaRegistrada,
        formaCompletar,
        notifications,
    } = props;
    const { cita, sucursal } = info;
    const { localizacion } = sucursal;

    const classes = useStyles();
    const [isExpanded, setIsExpanded] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [movimientosDialogOpen, setMovimientosDialogOpen] = useState(false);
    const [detalleCita, setDetalleCita] = useState<VerCitaMedicaDto>();
    const [cancelando, setCancelando] = useState(false);
    const [generandoDocumentoAtencionEmpleado, setGenerandoDocumentoAtencionEmpleado] = useState(false);
    const [popupEditarClienteAbierto, setPopupEditarClienteAbierto] = useState(false);
    const [popupConfirmarAsistenciaAbierto, setPopupConfirmarAsistenciaAbierto] = useState(false);
    const [popupCompletarDatosAbierto, setPopupCompletarDatosAbierto] = useState(false);
    const [popupReagendarAbierto, setPopupReagendarAbierto] = useState(false);
    const [infoCitaReagendar, setInfoCitaReagendar] = useState<ModeloAgendaGrabada | null>(null);

    useEffect(() => {
        if (isExpanded) {
            cargarDetalleSolicitud();
        }
    }, [isExpanded]);

    const convertirFecha = (fecha: Date) => {
        const fechaSolicitud = dayjs(fecha);
        const fechaActual = dayjs();
        if (fechaSolicitud.year() == fechaActual.year()) {
            return fechaSolicitud.format("DD MMMM HH:mm");
        } else {
            return fechaSolicitud.format("DD MMMM YYYY HH:mm");
        }
    }

    const convertirFechaAgenda = (fecha: Date) => {
        return dayjs(fecha).format("DD MMM. YY");
    }

    const convertirHoraAgenda = (fecha: Date) => {
        return dayjs(fecha).format("HH:mm");
    }

    const convertirFechaHoraAgenda = (fecha: Date) => {
        return dayjs(fecha).format("DD MMM. YYYY HH:mm:ss");
    }

    const mostrarDetalleSolicitud = (event: {}, _isExpanded: boolean) => {
        setIsExpanded(_isExpanded);
    }

    const cargarDetalleSolicitud = () => {
        setIsLoading(true);
        citasService.getCitaMedica(info.cita.codigo)
            .then(
                (detalleCita: VerCitaMedicaDto) => {
                    setDetalleCita(detalleCita);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setIsLoading(false);
            });
    }

    const toggleOpenDialogMovimientos = () => {
        setMovimientosDialogOpen(!movimientosDialogOpen);
    }

    const clickEditarCita = () => {
        setInfoCitaReagendar(from_VerAgendaCitaMedicaDto_to_ModeloAgendaGrabada(info));
        setPopupReagendarAbierto(true);
    }

    const clickEditarCliente = () => {
        setPopupEditarClienteAbierto(true);
    }

    const clickAtender = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        if (formaCompletar == FormaCompletarAgenda.COMPLETAR_DATOS) {
            setPopupCompletarDatosAbierto(true);
        } else {
            setPopupConfirmarAsistenciaAbierto(true);
        }
    }


    const clienteActualizado = (datosCliente: VerClienteDto) => {
        setGenerandoDocumentoAtencionEmpleado(true);
        setPopupEditarClienteAbierto(false);
        agendasService.PostGenerarDocumentoEmpleado(info.codigo)
            .then(
                respuesta => {
                    notifications.success("Se ha generado el documento de atención al fonoaudiólogo correctamente");
                    onDatosCompletados();
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setGenerandoDocumentoAtencionEmpleado(false);
            });

    }

    const popupCompletarDatosCerrado = () => {
        setPopupCompletarDatosAbierto(false);
    };

    const clickCompletarDatos = () => {
        setPopupEditarClienteAbierto(true);
    }

    const inasistenciaRegistrada = (reagendar: boolean) => {
        onAgendaCancelada(reagendar);
    }

    const popupConfirmarAsistenciaCerrado = () => {
        setPopupConfirmarAsistenciaAbierto(false);
    };

    const llamadaRegistrada = (datosPrecargados: VerAgendaCitaMedicaDto) => {
        // if (isExpanded) {
        //     cargarDetalleSolicitud();
        // }
        onLlamadaRegistrada(datosPrecargados.codigo);
    }

    const asistenciaConfirmada = () => {
        onAsistenciaConfirmada();
    }

    const citaCanceladaConfirmacion = (reagendar: boolean, observacion: string) => {
        setCancelando(true);
        const cancelarAgendaDto = {
            reagendar,
            desdeLlamada: true,
            observacion,
        };
        agendasService.PutCancelarAgenda(info.codigo, cancelarAgendaDto)
            .then(
                respuesta => {
                    setCancelando(false);
                    notifications.success("Se ha cancelado la agenda correctamente");
                    onAgendaCancelada(reagendar);
                },
                error => {
                    notifications.error(error);
                })
    }

    const clickLlamadasCita = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        setMovimientosDialogOpen(true);
    }

    const popupReagendarCerrado = () => {
        setPopupReagendarAbierto(false);
    }

    const popupReagendarCancelado = () => {
        setPopupReagendarAbierto(false);
    };

    const citaReagendada = (infoReagenda: VerAgendaCitaMedicaDto) => {
        setPopupReagendarAbierto(false);
        onAgendaEditada(infoReagenda.codigo);
    }

    const popupEditarClienteCerrado = () => {
        setPopupEditarClienteAbierto(false);
    }

    const popupEditarClienteCancelado = () => {
        setPopupEditarClienteAbierto(false);
    };

    const renderAgenda = () => {
        if (detalleCita) {
            const { agenda, cantidadReagendados, pacienteLlego } = detalleCita;
            const color = obtenerColorXestado(agenda.estado);
            return (
                <Fragment>
                    <Grid item container xs={12} alignItems="center" direction="row">
                        <Grid container item xs={4} direction="column" alignItems="center">
                            <IconButton aria-label="reagendas"
                                onClick={toggleOpenDialogMovimientos}
                                disabled={cantidadReagendados == 0}
                            >
                                <Badge badgeContent={cantidadReagendados} color="primary" showZero={true}>
                                    <SwapVert />
                                </Badge>
                            </IconButton>
                            <Typography variant="body2" color="textSecondary">
                                Movimientos
                            </Typography>
                        </Grid>
                        <Grid container item xs={4} direction="column" alignItems="center">
                            <IconButton aria-label="reagendas" style={{ color: color ?? undefined }}>
                                {
                                    obtenerIconoEstadoAgenda(agenda.estado)
                                }
                            </IconButton>
                            <Typography variant="body2" color="textSecondary">
                                {
                                    obtenerDescripcionXestado(agenda.estado)
                                }
                            </Typography>
                        </Grid>
                        <Grid container item xs={4} direction="column" alignItems="center">
                            <IconButton aria-label="reagendas">
                                {
                                    pacienteLlego
                                        ? <Done />
                                        : <Close />
                                }
                            </IconButton>
                            <Typography variant="body2" color="textSecondary">
                                cliente llegó
                            </Typography>
                        </Grid>
                    </Grid>
                    <Card variant="outlined">
                        <CardContent>
                            <Grid container spacing={1}>
                                <Grid item container justify="space-around" direction="row" spacing={1}>
                                    <Grid container item xs={5} alignItems="center" direction="column">
                                        <Typography variant="h5">
                                            {convertirFechaAgenda(agenda.fechaInicio)}
                                        </Typography>
                                        <Typography variant="subtitle1">
                                            {convertirHoraAgenda(agenda.fechaInicio)}
                                        </Typography>
                                    </Grid>
                                    <Grid item container xs={7} alignItems="center">
                                        <Typography color="textSecondary" variant="body2">
                                            {`${agenda.empleado.nombre} ${agenda.empleado.apellido}`}
                                        </Typography>
                                    </Grid>
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="subtitle2">
                                        {agenda.titulo}
                                    </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <Typography variant="body2">
                                        {agenda.descripcion}
                                    </Typography>
                                </Grid>
                                <Grid item container xs={12} alignItems="center" direction="row">
                                    <Grid item xs={1}>
                                        <Typography variant="caption" color="textSecondary">
                                            <Bookmark fontSize="small" className={classes.iconMargin} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={11} sm={5}>
                                        <Typography variant="caption" color="textSecondary">
                                            {convertirFechaHoraAgenda(agenda.fechaRegistro)}
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={1}>
                                        <Typography variant="caption" color="textSecondary">
                                            <History fontSize="small" className={classes.iconMargin} />
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={11} sm={5}>
                                        <Typography variant="caption" color="textSecondary">
                                            {convertirFechaHoraAgenda(agenda.fechaUltimaModificacion)}
                                        </Typography>
                                    </Grid>
                                </Grid>
                            </Grid>
                        </CardContent>
                    </Card>
                </Fragment>
            )
        } else {
            return (
                <Typography variant="subtitle1" color={"error"}>
                    La cita no tiene una agenda vigente.
                </Typography>
            );
        }
    }

    const renderCita = () => {
        if (detalleCita?.codigo) {
            const { cliente, agenda } = detalleCita;
            return (
                <Grid container spacing={1}>
                    <Grid item container xs={12} justify="space-between">
                        <Typography variant="h4">
                            Cliente
                        </Typography>
                        {
                            formaCompletar != FormaCompletarAgenda.COMPLETAR_DATOS ?
                                <IconButton aria-label="Editar" color="primary" onClick={clickEditarCliente}>
                                    <Edit />
                                </IconButton>
                                :
                                null
                        }
                    </Grid>
                    <Grid item container xs={6} alignItems="center">
                        <CallToAction fontSize="small" className={classes.iconMargin} />
                        {cliente.numeroIdentificacion}
                    </Grid>
                    <Grid item container xs={6} alignItems="center">
                        <AssignmentInd fontSize="small" className={classes.iconMargin} />
                        {cliente.prioridadNombreComercial ?
                            cliente.nombreComercial
                            :
                            `${cliente.nombre} ${cliente.apellido}`
                        }
                    </Grid>
                    <Grid item container xs={6} alignItems="center">
                        <Phone fontSize="small" className={classes.iconMargin} />
                        {cliente.telefono}
                    </Grid>
                    <Grid item container xs={6} alignItems="center">
                        <Wc fontSize="small" className={classes.iconMargin} />
                        {cliente.sexo === 'M' ?
                            'Masculino'
                            :
                            'Femenino'
                        }
                    </Grid>
                    <Grid item container xs={12} alignItems="center">
                        <Mail fontSize="small" className={classes.iconMargin} />
                        {cliente.mail}
                    </Grid>
                    <Grid item container xs={12} alignItems="center">
                        <Directions fontSize="small" className={classes.iconMargin} />
                        {cliente.direccion}
                    </Grid>
                    <Grid item container xs={12} justify="space-between">
                        <Typography variant="h4">
                            Cita
                        </Typography>
                        <IconButton aria-label="Editar" color="primary" onClick={clickEditarCita} disabled={!agenda}>
                            <Edit />
                        </IconButton>
                    </Grid>
                    <Grid container item xs={12} direction="row" alignItems="center" spacing={2}>
                        <Grid container item sm={6} justify={detalleCita.activa ? "center" : "space-around"}>
                            {
                                detalleCita.activa ?
                                    <Grid container alignItems={"center"} direction={"column"}>
                                        <Typography variant="h5" color={"textSecondary"}>
                                            {
                                                detalleCita.agenda.estado != EstadoAgenda.AGENDADO
                                                    ? "Cita atendida"
                                                    : "La cita no ha sido atendida."
                                            }
                                        </Typography>
                                    </Grid>
                                    :
                                    <Grid container>
                                        <Grid item xs={12}>
                                            <Typography variant="h5">
                                                Observación de cita
                                                </Typography>
                                        </Grid>
                                        <Grid item xs={12}>
                                            {detalleCita.diagnostico}
                                        </Grid>
                                    </Grid>
                            }
                        </Grid>
                        <Grid item container sm={6} direction="column" alignItems="center" spacing={1}>
                            {
                                renderAgenda()
                            }
                        </Grid>
                    </Grid>
                </Grid>
            )
        } else {
            return (
                <Typography variant="subtitle1">
                    No se ha creado una cita
                </Typography>
            )
        }
    }

    return (
        <Fragment>
            <Accordion key={info.codigo} onChange={mostrarDetalleSolicitud}>
                <AccordionSummary
                    expandIcon={<ExpandMore />}
                    aria-controls={`panel${info.codigo}-content`}
                    id={`panel${info.codigo}-header`}
                >
                    <Grid container spacing={1}>
                        <Grid container item xs>
                            <Grid item xs={12}>
                                <Typography variant="subtitle2">
                                    {`${cita.cliente.nombre} ${cita.cliente.apellido}`}
                                </Typography>
                                <Typography variant="subtitle2">
                                    {`Email: ${cita.cliente.email.length > 0 ? cita.cliente.email: 'N/A'} - 
                                    Telf: ${cita.cliente.telefono.length > 0 ? cita.cliente.telefono: 'N/A'}`}
                                    
                                </Typography>
                            </Grid>
                            <Grid item xs={12}>
                                <Typography variant="caption">
                                    {`${localizacion?.provincia.descripcion} - ${localizacion?.canton.descripcion} (${sucursal ? sucursal.nombre : 'Sin sucursal'})`}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Grid container item xs justify="center" alignItems="center" direction={"column"}>
                            <Grid item xs>
                                <Typography variant="subtitle2" style={{ textAlign: "center" }}>
                                    {
                                        `${convertirFecha(info.fechaInicio)}`
                                    }
                                </Typography>
                            </Grid>
                            <Grid item xs>
                                <Typography variant="caption">
                                    Fecha de agenda
                            </Typography>
                            </Grid>
                        </Grid>
                        <Grid container item xs={3} alignItems="center" justify="center" spacing={1}>
                            <IconButton
                                aria-label="reagendas"
                                onClick={clickLlamadasCita}
                                size="small"
                                className={classes.btnResumen}
                                disabled={info.cantidadMovimientos == 0}
                            >
                                <Badge
                                    badgeContent={info.cantidadMovimientos}
                                    color="primary"
                                >
                                    <SwapVert fontSize="inherit" />
                                </Badge>
                            </IconButton>
                            <IconButton
                                aria-label="reagendas"
                                onClick={clickLlamadasCita}
                                size="small"
                                className={classes.btnResumen}
                                disabled={info.cantidadLlamadasUltimoProceso == 0}
                            >
                                <Badge
                                    badgeContent={info.cantidadLlamadasUltimoProceso}
                                    color="primary"
                                >
                                    <Call fontSize="inherit" />
                                </Badge>
                            </IconButton>
                            <Button
                                variant="outlined"
                                color="primary"
                                onClick={clickAtender}
                                aria-label="Atender agenda"
                                size="small"
                            >
                                Atender
                            </Button>
                        </Grid>
                    </Grid>
                </AccordionSummary>
                <AccordionDetails>
                    {isLoading ?
                        <Loader />
                        :
                        renderCita()
                    }
                </AccordionDetails>
            </Accordion>
            <DialogEditarCliente
                open={popupEditarClienteAbierto}
                onClose={popupEditarClienteCerrado}
                onCancelClick={popupEditarClienteCerrado}
                onClienteActualizado={clienteActualizado}
                codigoClienteEditar={cita.cliente.codigo}
            />
            <DialogCompletarDatos
                open={popupCompletarDatosAbierto}
                onClose={popupCompletarDatosCerrado}
                datosPrecargados={info}
                onClickCompletarDatos={clickCompletarDatos}
                onInasistenciaRegistrada={inasistenciaRegistrada}
                mostrarProcesando={generandoDocumentoAtencionEmpleado}
            />
            <DialogConfirmarAsistencia
                open={popupConfirmarAsistenciaAbierto}
                onClose={popupConfirmarAsistenciaCerrado}
                datosPrecargados={info}
                onLlamadaRegistrada={llamadaRegistrada}
                onConfirmarAsistencia={asistenciaConfirmada}
                onCancelarCita={citaCanceladaConfirmacion}
            />
            <DialogMovimientos
                open={movimientosDialogOpen}
                onClose={toggleOpenDialogMovimientos}
                codigoGrupoCita={cita.solicitud ? cita.solicitud : info.codigo}
            />
            <DialogFormReagendar
                open={popupReagendarAbierto}
                onClose={popupReagendarCerrado}
                onCancelClick={popupReagendarCancelado}
                onCitaReagendada={citaReagendada}
                infoCita={infoCitaReagendar as ModeloAgendaGrabada}
            />
        </Fragment>
    );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(AgendaCompletar);
