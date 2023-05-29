import { Accordion, AccordionSummary, Badge, Button, createStyles, Grid, IconButton, makeStyles, Typography } from '@material-ui/core';
import { Theme } from '@material-ui/core/styles';
import { Call, ExpandMore, SwapVert } from '@material-ui/icons';
import dayjs from 'dayjs';
import React, { Fragment, MouseEvent, useState } from 'react';
import { VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import DialogAtencionReagenda from './DialogAtencionReagenda';
import DialogMovimientos from './DialogMovimientos';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        btnResumen: {
            marginRight: 10,
        },
    }),
);

export interface OrdenReagendar {
    info: VerAgendaCitaMedicaDto,
    onProcesoReagendado: (codigo: string) => void,
    onProcesoEliminado: (codigo: string) => void,
}

export default function OrdenReagendar(props: OrdenReagendar) {
    const {
        info,
        onProcesoReagendado,
        onProcesoEliminado,
    } = props;
    const [isOpenDialogAtencionReagenda, setIsOpenDialogAtencionReagenda] = useState(false);
    const [isOpenMovimientos, setIsOpenMovimientos] = useState(false);
    const classes = useStyles();

    const { codigo, sucursal, cita, cantidadMovimientos, fechaRegistro, cantidadLlamadasUltimoProceso } = info;
    const { localizacion } = sucursal;
    const { cliente } = cita;

    const convertirFecha = (fecha: Date) => {
        const fechaSolicitud = dayjs(fecha);
        const fechaActual = dayjs();
        if (fechaSolicitud.year() == fechaActual.year()) {
            return fechaSolicitud.format("DD MMMM");
        } else {
            return fechaSolicitud.format("DD MMMM YYYY");
        }
    }

    const clickAtender = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        setIsOpenDialogAtencionReagenda(true);
    }

    const dialogAtencionReagendaClosed = () => {
        setIsOpenDialogAtencionReagenda(false);
    }

    const llamadaRegistrada = () => {

    }

    const procesoReagendado = (codigo: string) => {
        setIsOpenDialogAtencionReagenda(false);
        onProcesoReagendado(codigo);
    }

    const procesoEliminado = (codigo: string) => {
        setIsOpenDialogAtencionReagenda(false);
        onProcesoEliminado(codigo);
    }

    const clickLlamadasCita = (e: MouseEvent<HTMLButtonElement>) => {
        e.stopPropagation();
        setIsOpenMovimientos(true);
    }

    const dialogMovimientosClosed = () => {
        setIsOpenMovimientos(!isOpenMovimientos);
    }

    return (
        <Fragment>
            <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMore />}
                    aria-controls={`panel${codigo}-content`}
                    id={`panel${codigo}-header`}
                >
                    <Grid container spacing={1}>
                        <Grid container item xs>
                            <Grid item xs={12}>
                                <Typography variant="subtitle2">
                                    {`${cliente.nombre} ${cliente.apellido}`}
                                </Typography>
                                <Typography variant="subtitle2">
                                    {`Email: ${cliente.email.length > 0 ? cliente.email: 'N/A' } - 
                                    Telf: ${cliente.telefono.length > 0 ? cliente.telefono: 'N/A'}`}
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
                                        `${convertirFecha(fechaRegistro)}`
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
                            <IconButton
                                aria-label="reagendas"
                                onClick={clickLlamadasCita}
                                size="small"
                                className={classes.btnResumen}
                                disabled={cantidadMovimientos == 0}
                            >
                                <Badge
                                    badgeContent={cantidadMovimientos}
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
                        </Grid>
                    </Grid>
                </AccordionSummary>
            </Accordion>
            <DialogAtencionReagenda
                open={isOpenDialogAtencionReagenda}
                onClose={dialogAtencionReagendaClosed}
                datosPrecargados={info}
                onLlamadaRegistrada={llamadaRegistrada}
                onProcesoReagendado={procesoReagendado}
                onSolicitudEliminada={procesoEliminado}
            />
            <DialogMovimientos
                open={isOpenMovimientos}
                onClose={dialogMovimientosClosed}
                codigoGrupoCita={cita.solicitud ? cita.solicitud : codigo}
            />
        </Fragment>
    );
}