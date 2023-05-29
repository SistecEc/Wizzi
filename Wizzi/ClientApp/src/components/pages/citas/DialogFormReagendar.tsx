import { Button, CircularProgress, createStyles, DialogActions, DialogContent, DialogTitle, Grid, makeStyles } from '@material-ui/core';
import React, { Fragment, useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { AnyAction, bindActionCreators, Dispatch } from 'redux';
import { RegistrarAgendaDto, VerAgendaCitaMedicaDto } from '../../../dtos/Agendas';
import { ModeloAgendaGrabada } from '../../../models/modeloAgendaGrabada';
import { agendasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog, ResponsiveDialogProps } from '../../UI';
import FormReagendar from './FormReagendar';

const useStyles = makeStyles((theme) =>
    createStyles({
        root: {
            width: '100%',
        },
        circularInsideButton: {
            marginRight: theme.spacing(),
        },
    }),
);

export interface DialogFormReagendarProps extends ResponsiveDialogProps {
    infoCita: ModeloAgendaGrabada,
    onCitaReagendada: (agendaGrabada: VerAgendaCitaMedicaDto) => void,
    onCancelClick: () => void,
    notifications: any,
}

function DialogFormReagendar(props: DialogFormReagendarProps) {
    const {
        open,
        onClose,
        infoCita,
        onCitaReagendada,
        onCancelClick,
        notifications,
    } = props;
    const classes = useStyles();
    let DialogFormReagendar = React.createRef();
    const [isOpenDialogReagendar, setIsOpenDialogReagendar] = useState(open);
    const [actualizando, setActualizando] = useState(false);
    const [infoAgenda, setInfoAgenda] = useState<ModeloAgendaGrabada | null>(null);

    useEffect(() => {
        setIsOpenDialogReagendar(open);
    }, [open])

    useEffect(() => {
        if (open) {
            setInfoAgenda({
                codigo: infoCita.codigo,
                startDate: infoCita.startDate,
                endDate: infoCita.endDate,
                title: infoCita.title,
                allDay: infoCita.allDay,
                rRule: infoCita.rRule,
                exDate: infoCita.exDate,
                notes: infoCita.notes,
                codigoEmpleado: infoCita.codigoEmpleado,
                tipoAgenda: infoCita.tipoAgenda,
                tipoCitaMedica: infoCita.tipoCitaMedica,
                fuenteRemision: infoCita.fuenteRemision,
                cliente: infoCita.cliente,
            });
        }
    }, [infoCita])

    const AgendaModificada = (infoAgenda: ModeloAgendaGrabada) => {
        setInfoAgenda(infoAgenda);
    }

    const reagendarCita = () => {
        setActualizando(true);
        const { codigo, ...restPropsAgenda } = infoAgenda as ModeloAgendaGrabada;
        const infoReagendar: RegistrarAgendaDto = {
            fechaInicio: restPropsAgenda.startDate,
            fechaFin: restPropsAgenda.endDate,
            titulo: restPropsAgenda.title,
            esTodoElDia: restPropsAgenda.allDay,
            reglaRecurrencia: restPropsAgenda.rRule,
            fechasExluidasRecurrencia: restPropsAgenda.exDate,
            descripcion: restPropsAgenda.notes,
            doctorAtiende: restPropsAgenda.codigoEmpleado,
            tipoCitaMedica: restPropsAgenda.tipoCitaMedica,
            fuenteRemision: restPropsAgenda.fuenteRemision,
        };
        agendasService.patchReagendar(codigo, infoReagendar)
            .then(
                (agendaGrabada: VerAgendaCitaMedicaDto) => {
                    notifications.success("Se ha reagendado la cita correctamente");
                    onCitaReagendada(agendaGrabada);
                },
                error => {
                    notifications.error(error);
                })
            .finally(() => {
                setActualizando(false);
            });
    }


    return (
        <ResponsiveDialog
            aria-labelledby="tituloDialogReagendarCita"
            open={open}
            onClose={onClose}
            fullWidth={true}
            maxWidth="lg"
        >
            <DialogTitle id="tituloDialogReagendarCita">
                Reagendar cita
            </DialogTitle>
            <DialogContent>
                <FormReagendar
                    refOverlay={DialogFormReagendar}
                    agendaModificar={infoAgenda}
                    onAgendaModificada={AgendaModificada}
                />
            </DialogContent>
            <DialogActions>
                <Grid container direction="row" justify="space-between">
                    <Button onClick={onCancelClick} color="secondary">
                        Cancelar
                    </Button>
                    <div>
                        <Button onClick={reagendarCita} color="primary" disabled={actualizando} autoFocus>
                            {
                                actualizando ?
                                    <Fragment>
                                        <CircularProgress size={14} color="primary" className={classes.circularInsideButton} />
                                        Actualizando...
                                    </Fragment>
                                    :
                                    'Actualizar'
                            }
                        </Button>
                    </div>
                </Grid>
            </DialogActions>
        </ResponsiveDialog>
    );

}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogFormReagendar);
