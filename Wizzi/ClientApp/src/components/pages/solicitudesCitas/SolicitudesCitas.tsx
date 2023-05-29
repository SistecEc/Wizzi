import {
    Button, Checkbox, Container,

    createStyles,

    Divider,

    Fab,

    FormControlLabel,
    Grid, makeStyles,  Typography
} from '@material-ui/core';
import { grey } from '@material-ui/core/colors';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import React, { Fragment, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { VerSolicitudCitaMedicaDto } from '../../../dtos/SolicitudesCitasMedicas';
import { EstadoAgenda, FormaCompletarAgenda } from '../../../enums';
import { replaceWithoutMutate } from '../../../helpers';
import { BaseResultadoPaginado, ResultadoPaginado } from '../../../models';
import { agendasService, citasService, ordenesInstalacionService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { Loader } from '../../UI';
import CmbCiudadSucursales from '../../UI/CmbCiudadSucursales';
import CmbSucursales from '../../UI/CmbSucursales';
import AgendaCompletar from './AgendaCompletar';
import { ReactComponent as CitasBg } from './CitasBG.svg';
import OrdenReagendar from './OrdenReagendar';
import SolicitudCita from './SolicitudCita';
import Tooltip from 'react-tooltip-lite';
import GraficoBarra from './GraficoBarra';
import TextField from '@material-ui/core/TextField';
import dayjs from 'dayjs';
import SearchIcon from '@material-ui/icons/Search';
const useStyles = makeStyles((theme) => 
    createStyles({
        banner: {
            maxHeight: '25vh',
        },
        subtitulo: {
            marginBottom: theme.spacing(),
        },
        leyendaSinResultados: {
            color: grey[500],
            textAlign: "center",
        },
    }),
);

export interface SolicitudesCitasProps {
    notifications: any,
}


const SolicitudesCitas = (props: SolicitudesCitasProps) => {
    const classes = useStyles();
    const {
        notifications
    } = props;
    const [infoPaginado, setInfoPaginado] = useState<BaseResultadoPaginado | null>(null);
    const [infoPaginadoPorReagendar, setInfoPaginadoPorReagendar] = useState<BaseResultadoPaginado | null>(null);
    const [infoPaginadoPorConfirmar, setInfoPaginadoPorConfirmar] = useState<BaseResultadoPaginado | null>(null);
    const [infoPaginadoPorCompletar, setInfoPaginadoPorCompletar] = useState<BaseResultadoPaginado | null>(null);
    const [solicitudes, setSolicitudes] = useState<VerSolicitudCitaMedicaDto[]>([]);
    const [filtrarSolicitudesHoy, setFiltrarSolicitudesHoy] = useState(false);
    const [ordenesPorReagendar, setOrdenesPorReagendar] = useState<VerAgendaCitaMedicaDto[]>([]);
    const [agendasPorConfirmar, setAgendasPorConfirmar] = useState<VerAgendaCitaMedicaDto[]>([]);
    const [agendasPorCompletar, setAgendasPorCompletar] = useState<VerAgendaCitaMedicaDto[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [cargandoOrdenesPorReagendar, setCargandoOrdenesPorReagendar] = useState(false);
    const [cargandoAgendasPorConfirmar, setCargandoAgendasPorConfirmar] = useState(false);
    const [cargandoAgendasPorCompletar, setCargandoAgendasPorCompletar] = useState(false);
    const [ciudadFiltrar, setCiudadFiltrar] = useState('');
    const [sucursalFiltrar, setSucursalFiltrar] = useState('');
    var date = new Date();
    const [fechainicio, setfechainicio] = useState(dayjs(date.setDate(date.getDate() - 5)).format("YYYY-MM-DD").toString());
    const [fechafinal, setfechafinal] = useState(dayjs(new Date()).format("YYYY-MM-DD").toString())
    useEffect(() => {
        cargarDatosIniciales();
    }, []);

    useEffect(() => {
        cargarDatosIniciales();
    }, [ciudadFiltrar, sucursalFiltrar]);

    useEffect(() => {
        cargarSolicitudes(1, true);
        const totalsolicitudes = solicitudes.length + ordenesPorReagendar.length + agendasPorConfirmar.length + agendasPorCompletar.length

    }, [filtrarSolicitudesHoy]);

    const cargarDatosIniciales = (aplicarfiltrofecha= false) => {
        cargarOrdenesPorReagendar(1, true,aplicarfiltrofecha);
        cargarSolicitudes(1, true,aplicarfiltrofecha);
        cargarAgendasPorConfirmar(1, true, aplicarfiltrofecha);
        cargarAgendasPorCompletar(1, true,aplicarfiltrofecha);
        const totalsolicitudes = solicitudes.length + ordenesPorReagendar.length + agendasPorConfirmar.length + agendasPorCompletar.length
    }

    const cargarSolicitudes = async (pagina: number, limpiarDatosPrevios: boolean = false, aplicarfiltrofecha:boolean= false) => {
        setIsLoading(true);
        const params: any = { p: pagina, ciudad: ciudadFiltrar, sucursal: sucursalFiltrar,fechaRegistroInicio: fechainicio, fechaRegistroFinal:fechafinal, aplicarfiltrofecha: aplicarfiltrofecha  };
        if (filtrarSolicitudesHoy) {
            params.fechaRegistro = new Date().toISOString();
        }

        await citasService.getSolicitudesCitas(params)
            .then(
                (resultados: ResultadoPaginado<VerSolicitudCitaMedicaDto>) => {
                    const { resultados: SiguientesSolicitudes, ...infoPaginado } = resultados;
                    setInfoPaginado(infoPaginado);
                    setSolicitudes(limpiarDatosPrevios ? SiguientesSolicitudes : [...solicitudes, ...SiguientesSolicitudes]);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setIsLoading(false);
            });
    }

    const cargarOrdenesPorReagendar = (pagina: number, limpiarDatosPrevios: boolean = false, aplicarfiltrofecha:boolean= false) => {
        setCargandoOrdenesPorReagendar(true);
        const params: any = { p: pagina, ciudad: ciudadFiltrar, sucursal: sucursalFiltrar, fechaRegistroInicio: fechainicio, fechaRegistroFinal:fechafinal, aplicarfiltrofecha: aplicarfiltrofecha  };
        ordenesInstalacionService.getPorReagendar(params)
            .then(
                (resultados: ResultadoPaginado<VerAgendaCitaMedicaDto>) => {
                    const { resultados: SiguientesOrdenes, ...infoPaginadoPorReagendar } = resultados;
                    setInfoPaginadoPorReagendar(infoPaginadoPorReagendar);
                    setOrdenesPorReagendar(limpiarDatosPrevios ? SiguientesOrdenes : [...ordenesPorReagendar, ...SiguientesOrdenes]);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setCargandoOrdenesPorReagendar(false);

            });
    }

    const cargarAgendasPorConfirmar = (pagina: number, limpiarDatosPrevios: boolean = false, aplicarfiltrofecha:boolean= false) => {
        setCargandoAgendasPorConfirmar(true);
        const params: any = { p: pagina, ciudad: ciudadFiltrar, sucursal: sucursalFiltrar, fechaRegistroInicio: fechainicio, fechaRegistroFinal:fechafinal, aplicarfiltrofecha:aplicarfiltrofecha  };
        agendasService.getAgendasPorConfirmar(params)
            .then(
                (resultados: ResultadoPaginado<VerAgendaCitaMedicaDto>) => {
                    const { resultados: SiguientesAgendas, ...infoPaginadoPorConfirmar } = resultados;
                    setInfoPaginadoPorConfirmar(infoPaginadoPorConfirmar);
                    setAgendasPorConfirmar(limpiarDatosPrevios ? SiguientesAgendas : [...agendasPorConfirmar, ...SiguientesAgendas]);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setCargandoAgendasPorConfirmar(false);
            });
    }

    const cargarAgendasPorCompletar = (pagina: number, limpiarDatosPrevios: boolean = false, aplicarfiltrofecha:boolean = false) => {
        setCargandoAgendasPorCompletar(true);
        const params: any = {  p: pagina, ciudad: ciudadFiltrar, sucursal: sucursalFiltrar,fechaRegistroInicio: fechainicio, fechaRegistroFinal:fechafinal, aplicarfiltrofecha:aplicarfiltrofecha   };
        agendasService.getAgendasPorCompletar(params)
            .then(
                (resultados: ResultadoPaginado<VerAgendaCitaMedicaDto>) => {
                    const { resultados: SiguientesAgendas, ...infoPaginadoPorCompletar } = resultados;
                    setInfoPaginadoPorCompletar(infoPaginadoPorCompletar);
                    setAgendasPorCompletar(limpiarDatosPrevios ? SiguientesAgendas : [...agendasPorCompletar, ...SiguientesAgendas]);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setCargandoAgendasPorCompletar(false);
            });
    }

    const CargarSiguientePagina = () => {
        if (infoPaginado) {
            cargarSolicitudes(infoPaginado.paginaActual + 1);
        }
    }

    const CargarSiguientePaginaPorReagendar = () => {
        if (infoPaginadoPorReagendar) {
            cargarOrdenesPorReagendar(infoPaginadoPorReagendar.paginaActual + 1);
        }
    }

    const CargarSiguientePaginaAgendasCompletar = () => {
        if (infoPaginadoPorCompletar) {
            cargarAgendasPorCompletar(infoPaginadoPorCompletar.paginaActual + 1);
        }
    }

    const CargarSiguientePaginaAgendasConfirmar = () => {
        if (infoPaginadoPorConfirmar) {
            cargarAgendasPorConfirmar(infoPaginadoPorConfirmar.paginaActual + 1);
        }
    }

    const solicitudEliminada = (codigoSolicitud: string) => {
        const solicitudesFiltradas = solicitudes.filter(s => s.codigo != codigoSolicitud);
        setSolicitudes(solicitudesFiltradas);
    }

    const solicitudAgendada = (codigoSolicitud: string) => {
        const solicitudesFiltradas = solicitudes.filter(s => s.codigo != codigoSolicitud);
        setSolicitudes(solicitudesFiltradas);
        cargarAgendasPorCompletar(1, true);
        cargarAgendasPorConfirmar(1, true);
    }

    const handleChangeCmbCiudad = (e: React.ChangeEvent<{ value: any }>) => {
        const { value: ciudadFiltrar } = e.target;
        setCiudadFiltrar(ciudadFiltrar);
        setSucursalFiltrar('');
    }

    const handleChangeCmbSucursal = (e: React.ChangeEvent<{ value: any }>) => {
        const { value: sucursalFiltrar } = e.target;
        setSucursalFiltrar(sucursalFiltrar);
    }

    const handleChangeSoloHoySolicitudes = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { checked: filtrarSoloHoy } = event.target;
        setFiltrarSolicitudesHoy(filtrarSoloHoy);
    };

    const agendaCancelada = (reagendar: boolean) => {
        if (reagendar) {
            cargarOrdenesPorReagendar(1, true);
        }
        cargarAgendasPorCompletar(1, true);
        cargarAgendasPorConfirmar(1, true);
    }

    const asistenciaConfirmada = () => {
        cargarAgendasPorCompletar(1, true);
        cargarAgendasPorConfirmar(1, true);
    }

    const datosCompletados = () => {
        cargarAgendasPorCompletar(1, true);
    }

    const agendaCompletarEditada = (codigoAgenda: string) => {
        cargarAgendasPorConfirmar(1, true);
        cargarAgendasPorCompletar(1, true);
    }

    const agendaCompletarLlamadaRegistrada = (codigoAgenda: string) => {
        const agenda = agendasPorCompletar.filter(a => a.codigo == codigoAgenda)[0];
        const i = agendasPorCompletar.indexOf(agenda);

        if (i !== -1) {
            agendasService.getAgendaPorCompletar(codigoAgenda)
                .then(
                    (resultado: VerAgendaCitaMedicaDto) => {
                        setAgendasPorCompletar(replaceWithoutMutate(agendasPorCompletar, resultado, i));
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                });
        } else {
            cargarAgendasPorCompletar(1, true);
        }
    }

    const agendaConfirmarEditada = (codigoAgenda: string) => {
        cargarAgendasPorConfirmar(1, true);
        cargarAgendasPorCompletar(1, true);
    }

    const agendaConfirmarLlamadaRegistrada = (codigoAgenda: string) => {
        const agenda = agendasPorConfirmar.filter(a => a.codigo == codigoAgenda)[0];
        const i = agendasPorConfirmar.indexOf(agenda);

        if (i !== -1) {
            agendasService.getAgendaPorConfirmar(codigoAgenda)
                .then(
                    (resultado: VerAgendaCitaMedicaDto) => {
                        setAgendasPorConfirmar(replaceWithoutMutate(agendasPorConfirmar, resultado, i));
                    },
                    error => {
                        notifications.error(error);
                    })
                .finally(() => {
                });
        } else {
            cargarAgendasPorConfirmar(1, true);
        }
    }

    const procesoReagendado = (codigo: string) => {
        cargarOrdenesPorReagendar(1, true);
        cargarAgendasPorCompletar(1, true);
        cargarAgendasPorConfirmar(1, true);
    }

    const procesoEliminado = (codigo: string) => {
        const nuevasOrdenes = ordenesPorReagendar.filter(o => o.codigo !== codigo);
        setOrdenesPorReagendar(nuevasOrdenes);
    }

    const obtenerTotal = () =>{
       
        return  solicitudes.filter(item =>
            item.cita?.agenda.estado !== EstadoAgenda.AGENDADO
            && item.cita?.agenda.estado !== EstadoAgenda.AGENDADO_CONFIRMADO
            && item.cita?.agenda.estado !== EstadoAgenda.AGENDADO_ATENDER).length
    }
    const obtenerTotaldesolicitudes = () =>{
        return solicitudes.length + ordenesPorReagendar.length + agendasPorConfirmar.length + agendasPorCompletar.length
    }

    const obtenerDataGrafico =() =>{
        return [obtenerTotal(),
            (infoPaginadoPorReagendar !== null ? infoPaginadoPorReagendar.totalRegistros: 0),
            (infoPaginadoPorCompletar !== null ? infoPaginadoPorCompletar.totalRegistros: 0),
            (infoPaginadoPorConfirmar !== null ? infoPaginadoPorConfirmar.totalRegistros: 0)]
    }
   
    const tooltipestaditica =()=>{
        return (
            <Tooltip 
                content={(
                    <GraficoBarra datos={obtenerDataGrafico()} labels={['Solicitudes','Por Reagendar','Agen. Confirmada','Confirmar Asistencia']}  />
                )}
                direction="right"
                tagName="span"
                className="target"
                >
                    {/* <a style={{color:'blue'}}>Resumen</a> */}
                    <Fab  aria-label="add" style={{ border:'0px', boxShadow: "none", marginLeft:"10px", width:"29px", height:'25px', backgroundColor:'transparent'}}>
                        <img style={{width:'30px'}} src="https://img.icons8.com/external-itim2101-blue-itim2101/64/000000/external-statistics-network-technology-itim2101-blue-itim2101.png"/>
                    </Fab>
            </Tooltip>
        )
    }
    return (
        <Fragment>
            <Container maxWidth="lg">
                <Grid container md={12}>
                    <Grid item md={4} sm={12}>
                        <CitasBg className={classes.banner} />
                    </Grid>
                    <Grid item md={8} sm={12}>
                        <GraficoBarra datos={obtenerDataGrafico()} labels={['Solicitudes','Por Reagendar','Agen. Confirmada','Confirmar Asistencia']}  />
                    </Grid>
                </Grid>
                <br/>
                <Divider/>
                <Grid container spacing={2}>
                    <Grid item xs={6}>
                        <CmbCiudadSucursales
                            incluirItemTodas={true}
                            valorInicialSeleccionado={ciudadFiltrar}
                            onValorSeleccionado={handleChangeCmbCiudad}
                            soloParaAgendar={true}
                            formControlProps={{
                                variant: "outlined",
                                fullWidth: true,
                            }}
                            
                        />
                    </Grid>
                    <Grid item xs={6} lg={6}>
                        <CmbSucursales
                            incluirItemTodas={true}
                            valorInicialSeleccionado={sucursalFiltrar}
                            ciudadFiltrar={ciudadFiltrar}
                            onValorSeleccionado={handleChangeCmbSucursal}
                            soloParaAgendar={true}
                            formControlProps={{
                                variant: "outlined",
                                fullWidth: true,
                            }}
                        />
                    </Grid> 
                    <Grid item xs={12} lg={12}>
                        <Grid container>
                            <Grid item md={4} xs={12}>
                                    <TextField
                                        id="date"
                                        label="Ficha Inicio"
                                        type="date"
                                        defaultValue={fechainicio}
                                        InputLabelProps={{
                                        shrink: true
                                        }}
                                        value={fechainicio}
                                        onChange={(e) => setfechainicio(e.target.value)}
                                        style={{width:'90%'}}
                                    />
                            </Grid>
                            <Grid item md={4} xs={12}>
                                    <TextField
                                        id="date"
                                        label="Ficha Final"
                                        type="date"
                                        defaultValue={fechafinal}
                                        InputLabelProps={{
                                        shrink: true,
                                        }}
                                        value={fechafinal}
                                        onChange={(e) => setfechafinal(e.target.value)}
                                        style={{width:'90%'}}
                                    />
                            </Grid>
                            <Grid item md={4} xs={12}>
                                <Button  
                                        variant="contained"
                                        style={{width:'80%', marginTop:'10px'}}
                                        onClick={(e) => cargarDatosIniciales(true)}
                                        startIcon={<SearchIcon />}
                                        >
                                    <Typography >
                                        Consultar
                                    </Typography>
                                </Button>
                            </Grid>
                        </Grid>
                    </Grid>
                    
                    
                    <Grid container item xs={12} justify="space-between" alignItems="center">
                        <Grid item xs={12} md={12} sm={12}>
                            <Grid container >
                                <Grid item xs={8} md={3} sm={12}>
                                    <Typography variant="subtitle2" className={classes.subtitulo}>
                                        SOLICITUDES: { solicitudes.length > 0 ? (infoPaginado !== null ?  obtenerTotal()  : 0):0}
                                    </Typography>
                                </Grid>
                                <Grid item xs={3} md={3} sm={12}>
                                    
                                </Grid>
                            </Grid>
                        </Grid>
                        <Grid item >
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={filtrarSolicitudesHoy}
                                        onChange={handleChangeSoloHoySolicitudes}
                                        name="chkSoloHoySolicitudes"
                                        color="primary"
                                    />
                                }
                                label="Solo hoy"
                            />
                        </Grid>
                    </Grid>
                    <Grid item xs={12} style={{maxHeight: 658, overflow: 'auto'}}>
                        {
                            solicitudes.length > 0 ?
                                solicitudes.map((solicitud, i) => {
                                    const estado = solicitud.cita?.agenda.estado;
                                    if (estado !== EstadoAgenda.AGENDADO
                                        && estado !== EstadoAgenda.AGENDADO_CONFIRMADO
                                        && estado !== EstadoAgenda.AGENDADO_ATENDER
                                    ) {
                                        return (
                                            <SolicitudCita
                                                key={solicitud.codigo}
                                                info={solicitud}
                                                onSolicitudEliminada={solicitudEliminada}
                                                onSolicitudAgendada={solicitudAgendada}
                                            />
                                        );
                                    } else {
                                        return null;
                                    }
                                })
                                :
                                isLoading ?
                                    null
                                    :
                                    <Typography variant="caption" className={classes.leyendaSinResultados}>
                                        No hay solicitudes
                                        </Typography>
                        }
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        {
                            isLoading ?
                                <Loader />
                                :
                                solicitudes.length > 0 && infoPaginado?.paginaActual !== infoPaginado?.cantidadPaginas ?
                                    <Button
                                        variant="text"
                                        color="primary"
                                        endIcon={<ExpandMoreIcon />}
                                        onClick={CargarSiguientePagina}
                                    >
                                        Cargar más
                                        </Button>
                                    :
                                    null
                        }
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="subtitle2" className={classes.subtitulo}>
                            POR REAGENDAR: { ordenesPorReagendar.length > 0 ?
                                             (infoPaginadoPorReagendar !== null ? infoPaginadoPorReagendar.totalRegistros: 0)
                                             : 0 }
                            </Typography>
                    </Grid>
                    <Grid item xs={12} style={{maxHeight: 658, overflow: 'auto'}}>
                        {
                            ordenesPorReagendar.length > 0 ?
                                ordenesPorReagendar.map((orden, i) => (
                                    <OrdenReagendar
                                        key={i}
                                        info={orden}
                                        onProcesoReagendado={procesoReagendado}
                                        onProcesoEliminado={procesoEliminado}
                                    />
                                ))
                                :
                                cargandoOrdenesPorReagendar ?
                                    null
                                    :
                                    <Typography variant="caption" className={classes.leyendaSinResultados}>
                                        No hay resultados
                                    </Typography>
                        }
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        {
                            cargandoOrdenesPorReagendar ?
                                <Loader />
                                :
                                ordenesPorReagendar.length > 0 && infoPaginadoPorReagendar?.paginaActual !== infoPaginadoPorReagendar?.cantidadPaginas ?
                                    <Button
                                        variant="text"
                                        color="primary"
                                        endIcon={<ExpandMoreIcon />}
                                        onClick={CargarSiguientePaginaPorReagendar}
                                    >
                                        Cargar más
                                        </Button>
                                    :
                                    null
                        }
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="subtitle2" className={classes.subtitulo}>
                            AGENDA CONFIRMADA: { agendasPorCompletar.length > 0 ? 
                                                (infoPaginadoPorCompletar !== null ? infoPaginadoPorCompletar.totalRegistros: 0)
                                                : 0}
                        </Typography>
                    </Grid>
                    <Grid item xs={12} style={{maxHeight: 658, overflow: 'auto'}}>
                        {
                            agendasPorCompletar.length > 0 ?
                                agendasPorCompletar.map((agenda, i) => (
                                    <AgendaCompletar
                                        key={i}
                                        info={agenda}
                                        formaCompletar={FormaCompletarAgenda.COMPLETAR_DATOS}
                                        onAgendaCancelada={agendaCancelada}
                                        onAsistenciaConfirmada={asistenciaConfirmada}
                                        onDatosCompletados={datosCompletados}
                                        onAgendaEditada={agendaCompletarEditada}
                                        onLlamadaRegistrada={agendaCompletarLlamadaRegistrada}
                                    />
                                ))
                                :
                                cargandoAgendasPorCompletar ?
                                    null
                                    :
                                    <Typography variant="caption" className={classes.leyendaSinResultados}>
                                        No hay agendas por completar datos
                                        </Typography>
                        }
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        {
                            cargandoAgendasPorCompletar ?
                                <Loader />
                                :
                                agendasPorCompletar.length > 0 && infoPaginadoPorCompletar?.paginaActual !== infoPaginadoPorCompletar?.cantidadPaginas ?
                                    <Button
                                        variant="text"
                                        color="primary"
                                        endIcon={<ExpandMoreIcon />}
                                        onClick={CargarSiguientePaginaAgendasCompletar}
                                    >
                                        Cargar más
                                        </Button>
                                    :
                                    null
                        }
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="subtitle2" className={classes.subtitulo}>
                            CONFIRMAR ASISTENCIA: { agendasPorConfirmar.length > 0 ?  
                                                    (infoPaginadoPorConfirmar !== null ? infoPaginadoPorConfirmar.totalRegistros: 0)
                                                    : 0}
                        </Typography>
                    </Grid>
                    <Grid item xs={12} style={{maxHeight: 658, overflow: 'auto'}}>
                        {
                            agendasPorConfirmar.length > 0 ?
                                agendasPorConfirmar.map((agenda, i) => (
                                    <AgendaCompletar
                                        key={i}
                                        info={agenda}
                                        formaCompletar={FormaCompletarAgenda.CONFIRMAR_ASISTENCIA}
                                        onAgendaCancelada={agendaCancelada}
                                        onAsistenciaConfirmada={asistenciaConfirmada}
                                        onDatosCompletados={datosCompletados}
                                        onAgendaEditada={agendaConfirmarEditada}
                                        onLlamadaRegistrada={agendaConfirmarLlamadaRegistrada}
                                    />
                                ))
                                :
                                cargandoAgendasPorConfirmar ?
                                    null
                                    :
                                    <Typography variant="caption" className={classes.leyendaSinResultados}>
                                        No hay agendas por confirmar
                                        </Typography>
                        }
                    </Grid>
                    <Grid container item xs={12} justify="center">
                        {
                            cargandoAgendasPorConfirmar ?
                                <Loader />
                                :
                                agendasPorConfirmar.length > 0 && infoPaginadoPorConfirmar?.paginaActual !== infoPaginadoPorConfirmar?.cantidadPaginas ?
                                    <Button
                                        variant="text"
                                        color="primary"
                                        endIcon={<ExpandMoreIcon />}
                                        onClick={CargarSiguientePaginaAgendasConfirmar}
                                    >
                                        Cargar más
                                        </Button>
                                    :
                                    null
                        }
                    </Grid>
                </Grid>
            </Container>
        </Fragment>
    );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(SolicitudesCitas);
