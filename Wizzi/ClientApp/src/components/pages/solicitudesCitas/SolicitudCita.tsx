
import { Accordion, AccordionSummary, Avatar, Badge, Button, createStyles, Grid, IconButton, makeStyles, Typography } from '@material-ui/core';
import { green, red } from '@material-ui/core/colors';
import { Call, ExpandMore } from '@material-ui/icons';
import dayjs from 'dayjs';
import React, { Fragment, MouseEvent, useCallback, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import ImageViewer from "react-simple-image-viewer";
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { VerSolicitudCitaMedicaDto } from '../../../dtos/SolicitudesCitasMedicas';
import { citasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { Loader } from '../../UI';
import DialogAtencion from './DialogAtencion';
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
        subcampaniaAvatar: {
            marginRight: 10,
            width: theme.spacing(3),
            height: theme.spacing(3),
        }
    }),
);

export interface SolicitudCitaProps {
    info: VerSolicitudCitaMedicaDto,
    onSolicitudEliminada: (codigoSolicitud: string) => void,
    onSolicitudAgendada: (codigoSolicitud: string) => void,
    notifications: any,
}

function SolicitudCita(props: SolicitudCitaProps) {
    const {
        info,
        onSolicitudEliminada,
        onSolicitudAgendada,
        notifications,
    } = props;
    const { codigo, localizacion, sucursal, } = info;

    const { subcampaniaOrigen } = info;

    const classes = useStyles();
    const [isOpenDialogMovimientos, setIsOpenDialogMovimientos] = useState(false);
    const [isOpenDialogAtencionSolicitud, setIsOpenDialogAtencionSolicitud] = useState(false);
    const [isLoadingMovimientos, setIsLoadingMovimientos] = useState(false);
    const [cantidadLlamadasUltimoProceso, setCantidadLlamadasUltimoProceso] = useState(0);
    const [viewerIsOpen, setViewerIsOpen] = useState(false);


    useEffect(() => {
        setCantidadLlamadasUltimoProceso(info.cantidadLlamadasUltimoProceso);
    }, []);

    const convertirFecha = (fecha: Date) => {
        const fechaSolicitud = dayjs(fecha);
        const fechaActual = dayjs();
        if (fechaSolicitud.year() == fechaActual.year()) {
            return fechaSolicitud.format("DD MMMM");
        } else {
            return fechaSolicitud.format("DD MMMM YYYY");
        }
    }

    const clickLlamadasCita = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        setIsOpenDialogMovimientos(true);
    }

    const dialogMovimientosCerrado = () => {
        setIsOpenDialogMovimientos(false);
    }

    const clickAtender = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        setIsOpenDialogAtencionSolicitud(true);
    }

    const dialogAtencionSolicitudCerrado = () => {
        setIsOpenDialogAtencionSolicitud(false);
    }

    const llamadaRegistrada = () => {
        setIsLoadingMovimientos(true);
        citasService.getSolicitudCita(codigo)
            .then(
                (resultados: VerSolicitudCitaMedicaDto) => {
                    setCantidadLlamadasUltimoProceso(resultados.cantidadLlamadasUltimoProceso);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setIsLoadingMovimientos(false);
            });
    }

    const openLightbox = useCallback((event: MouseEvent<HTMLDivElement>) => {
        event.stopPropagation();
        if (subcampaniaOrigen) {
            setViewerIsOpen(true);
        }
    }, []);

    const closeLightbox = () => {
        setViewerIsOpen(false);
    };

    return (
        <Fragment>
            <Accordion key={info.codigo}>
                <AccordionSummary
                    expandIcon={<ExpandMore />}
                    aria-controls={`panel${info.codigo}-content`}
                    id={`panel${info.codigo}-header`}
                >
                    <Grid container spacing={1}>
                        <Grid container item xs>
                            <Grid item xs={12}>
                                <Typography variant="subtitle2">
                                    {`${info.nombrePaciente} ${info.apellidoPaciente} `}
                                </Typography>
                                <Typography variant="subtitle2">
                                    {`Email: ${info.emailPaciente.length > 0 ? info.emailPaciente : 'N/A'} - 
                                    Telf: ${info.telefonoPaciente.length > 0 ? info.telefonoPaciente: 'N/A'}`}
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
                                        `${convertirFecha(info.fechaRegistro)}`
                                    }
                                </Typography>
                            </Grid>
                            <Grid item xs>
                                <Typography variant="caption">
                                    Fecha de solicitud
                                </Typography>
                            </Grid>
                        </Grid>
                        <Grid container item xs={3} alignItems="center" justify="center" spacing={1}>
                            <Avatar
                                className={classes.subcampaniaAvatar}
                                alt={subcampaniaOrigen?.descripcion}
                                src={`Imagenes/${subcampaniaOrigen?.imagen}`}
                                onClick={openLightbox}
                            />
                            {
                                isLoadingMovimientos ?
                                    <Loader />
                                    :
                                    <>
                                        <IconButton
                                            aria-label="reagendas"
                                            onClick={clickLlamadasCita}
                                            size="small"
                                            className={classes.btnResumen}
                                            disabled={cantidadLlamadasUltimoProceso == 0}
                                        >
                                            <Badge
                                                badgeContent={cantidadLlamadasUltimoProceso}
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
                                    </>
                            }

                        </Grid>
                    </Grid>
                </AccordionSummary>
            </Accordion>
            <DialogAtencion
                open={isOpenDialogAtencionSolicitud}
                onClose={dialogAtencionSolicitudCerrado}
                datosPrecargados={info}
                onLlamadaRegistrada={llamadaRegistrada}
                onSolicitudAgendada={onSolicitudAgendada}
                onSolicitudEliminada={onSolicitudEliminada}
            />
            <DialogMovimientos
                open={isOpenDialogMovimientos}
                onClose={dialogMovimientosCerrado}
                codigoGrupoCita={info.codigo}
            />
            {
                viewerIsOpen && subcampaniaOrigen ?
                    <ImageViewer
                        src={[`Imagenes/${subcampaniaOrigen.imagen}`]}
                        currentIndex={0}
                        onClose={closeLightbox}
                        backgroundStyle={{
                            backgroundColor: "rgba(0,0,0,0.9)",
                            zIndex: 10000
                        }}
                    />
                    :
                    null
            }

        </Fragment>
    );
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(SolicitudCita);