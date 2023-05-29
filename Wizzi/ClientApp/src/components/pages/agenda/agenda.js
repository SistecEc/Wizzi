import { connectProps } from '@devexpress/dx-react-core';
import { EditingState, ViewState } from '@devexpress/dx-react-scheduler';
import { AllDayPanel, AppointmentForm, Appointments, AppointmentTooltip, ConfirmationDialog, CurrentTimeIndicator, DateNavigator, DayView, DragDropProvider, EditRecurrenceMenu, MonthView, Resources, Scheduler, TodayButton, Toolbar, ViewSwitcher, WeekView } from '@devexpress/dx-react-scheduler-material-ui';
import { Button, CircularProgress, DialogActions, DialogContent, DialogTitle, Grid, Paper } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import * as dayjs from 'dayjs';
import * as React from 'react';
import { Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { EstadoAgenda, RazonesCambioAutocomplete, TiposForm } from '../../../enums';
import { colorPalette, localizacionAllDayPanel, localizacionBtnHoy, localizacionConfirmationDialog, localizacionFormAgenda } from '../../../helpers';
import { checkEmptyObject } from '../../../helpers/utils';
import { agendasService, empleadosService, tiposCitasMedicasService } from '../../../services';
import { fuentesRemisionService } from '../../../services/fuentesRemision.service';
import { notificationActions } from '../../../store/actions';
import { ResponsiveDialog } from '../../UI';
import BuscadorCitaPaciente from '../../UI/BuscadorCitaPaciente';
import CmbCiudadSucursales from '../../UI/CmbCiudadSucursales';
import CmbEmpleados from '../../UI/CmbEmpleados';
import CmbSucursales from '../../UI/CmbSucursales';
import FiltroEstadoAgenda from '../../UI/FiltroEstadoAgenda';
import { ClickableDayScaleCellComponent, CustomAppointmentTooltipContent, CustomDayTimeTableCellComponent, CustomMonthDayCellComponent, CustomWeekTimeTableCellComponent, ToolbarWithLoading } from '../../UI/Scheduler';
import { CommandButtonComponent } from '../../UI/Scheduler/CommandButtonComponent';
import { CustomAppointmentComponent } from '../../UI/Scheduler/CustomAppointmentComponent';
import { CustomAppointmentContent } from '../../UI/Scheduler/CustomAppointmentContent';
import FormCliente from '../clientes/FormCliente';

const initialState = {
    loading: false,
    agendaModificableSeleccionada: false,
    FormAgendarsoloLectura: false,
    fechaActual: new Date(),
    vistaActual: 'Week',
    localizacion: 'es-EC',
    agendas: [],
    recursosEmpleados: {},
    recursosTiposCitasMedicas: {},
    recursosFuentesRemision: {},
    ciudadFiltrar: '',
    sucursalFiltrar: '',
    empleadoFiltrar: '',
    abrirPopupCliente: false,
    agendaCreada: {},
    filtroEstadosAgendaBloqueados: [EstadoAgenda.AGENDADO_ATENDER],
    filtroEstadosAgendaSeleccionados: [EstadoAgenda.AGENDADO, EstadoAgenda.AGENDADO_CONFIRMADO],
};

const mapAgendas = agenda => ({
    startDate: new Date(agenda.fechaInicio),
    endDate: new Date(agenda.fechaFin),
    title: agenda.titulo,
    allDay: agenda.todoElDia,
    id: agenda.codigo,
    rRule: agenda.reglaRecurrencia,
    exDate: agenda.fechasExcluidasRecurrencia,
    notes: agenda.descripcion,
    codigoEmpleado: agenda.empleado.codigo,
    tipoAgenda: parseInt(agenda.tipoAgenda.codigo),
    tipoCitaMedica: agenda.cita.tipoCitaMedica,
    estado: parseInt(agenda.estado),
    fuenteRemision: agenda.cita.fuenteRemision,
    cliente: agenda.cita.cliente,
});

const mapEmpleados = empleado => (
    {
        id: empleado.codigo,
        text: `${empleado.apellido.toUpperCase()} ${empleado.nombre.toUpperCase()}`,
    }
);

const mapTiposCitas = tipoCita => (
    {
        id: tipoCita.codigo,
        text: tipoCita.descripcion,
    }
);

const mapFuenteRemision = fuenteRemision => (
    {
        id: fuenteRemision.codigo,
        text: fuenteRemision.descripcion,
    }
);

const EsElMismoFiltro = (filtroNuevo, UltimoFiltro) => {
    var esMismoFiltroEstados = UltimoFiltro?.estados.length === filtroNuevo?.estados.length &&
        UltimoFiltro?.estados.every((e, i) => filtroNuevo?.estados.includes(e));

    return filtroNuevo.inicio === UltimoFiltro?.inicio
        && filtroNuevo.fin === UltimoFiltro?.fin
        && filtroNuevo?.empleado === UltimoFiltro?.empleado
        && esMismoFiltroEstados
        && filtroNuevo?.ciudad === UltimoFiltro?.ciudad
        && filtroNuevo?.sucursal === UltimoFiltro?.sucursal;
}

class Agenda extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = initialState;
        this.MainAgendaRef = React.createRef();

        this.CommandButton = connectProps(CommandButtonComponent, () => {
            const { agendaModificableSeleccionada } = this.state;
            return {
                mostrarVisualizar: agendaModificableSeleccionada,
            };
        });

        this.CustomDayTimeTableCell = connectProps(CustomDayTimeTableCellComponent, () => {
            return {
                ValidadorContinuarDblClick: this.sePuedeAgendarEnFecha,
            };
        });

        this.CustomWeekTimeTableCell = connectProps(CustomWeekTimeTableCellComponent, () => {
            return {
                ValidadorContinuarDblClick: this.sePuedeAgendarEnFecha,
            };
        });

        this.ClickableDayScaleCell = connectProps(ClickableDayScaleCellComponent, () => {
            return {
                onClick: this.cambiarVistaAdia,
            };
        });

        this.CustomMonthDayCell = connectProps(CustomMonthDayCellComponent, () => {
            return {
                customOnDoubleClick: this.cambiarVistaAdiaDesdeMes,
            };
        });

        this.appointmentComponent = connectProps(CustomAppointmentComponent, () => {
            return {
                beforeClick: this.beforeAppointmentClick,
            };
        });

    }

    componentDidMount() {
        this.cargarRecursos();
        this.cargarAgendas();
    }

    componentDidUpdate() {
        this.CommandButton.update();
        const filtroAgenda = this.obtenerFiltroAgenda();
        if (!EsElMismoFiltro(filtroAgenda, this.ultimoFiltro)) {
            this.cargarAgendas(filtroAgenda);
        }
    }

    cargarRecursos() {
        empleadosService.getEmpleados({ paginado: false, soloRol: false, soloPuedeAgendar: true })
            .then(
                empleados => {
                    let instanciaEmpleados = empleados ? empleados.map(mapEmpleados) : [];
                    const recursosEmpleados = {
                        fieldName: 'codigoEmpleado',
                        title: 'Fonoaudiólogo atiende',
                        instances: instanciaEmpleados,
                    };
                    tiposCitasMedicasService.getTiposCitasMedicas()
                        .then(
                            tiposCitas => {
                                const instanciaTiposCita = tiposCitas ? tiposCitas.map(mapTiposCitas) : [];
                                const recursosTiposCitasMedicas = {
                                    fieldName: 'tipoCitaMedica',
                                    title: 'Tipo de cita',
                                    instances: instanciaTiposCita,
                                };
                                fuentesRemisionService.getFuentesRemision()
                                    .then(
                                        fuentesRemision => {
                                            const instanciaFuentesRemision = fuentesRemision ? fuentesRemision.map(mapFuenteRemision) : [];
                                            const recursosFuentesRemision = {
                                                fieldName: 'fuenteRemision',
                                                title: 'Fuente de remisión',
                                                instances: instanciaFuentesRemision,
                                            };
                                            this.setState({
                                                recursosEmpleados,
                                                recursosTiposCitasMedicas,
                                                recursosFuentesRemision,
                                            });
                                        },
                                        error => {
                                            return error;
                                        });
                            },
                            error => {
                                return error;
                            });
                },
                error => {
                    this.props.notifications.error(error);
                    this.setState({ loading: false });
                });
    }

    obtenerFiltroAgenda = () => {
        const { fechaActual, vistaActual, empleadoFiltrar, ciudadFiltrar, sucursalFiltrar, filtroEstadosAgendaBloqueados, filtroEstadosAgendaSeleccionados } = this.state;
        const inicio = dayjs(fechaActual).startOf(vistaActual.toLowerCase());
        const fin = inicio.clone().endOf(vistaActual.toLowerCase());
        return {
            inicio: inicio.toISOString(),
            fin: fin.toISOString(),
            empleado: empleadoFiltrar,
            estados: [...filtroEstadosAgendaBloqueados, ...filtroEstadosAgendaSeleccionados],
            ciudad: ciudadFiltrar,
            sucursal: sucursalFiltrar,
        };
    };

    vistaActualCambiada = (vistaActual) => {
        this.setState({ vistaActual });
    };

    fechaActualCambiada = (fechaActual) => {
        this.setState({ fechaActual });
    };

    cambiarVistaAdia = (info) => {
        this.setState({
            vistaActual: 'Day',
            fechaActual: info.endDate,
        });
    }

    cambiarVistaAdiaDesdeMes = (e, info) => {
        this.setState({
            vistaActual: 'Day',
            fechaActual: info.startDate,
        });
    }

    cargarAgendas = (filtroAgenda) => {
        filtroAgenda = filtroAgenda || this.obtenerFiltroAgenda();
        this.setState({ agendas: [], loading: true });
        agendasService.getAgendas(filtroAgenda)
            .then(
                agendasGrabadas => {
                    const agendas = agendasGrabadas ? agendasGrabadas.map(mapAgendas) : [];
                    this.setState({
                        agendas,
                    },
                        this.setState({ loading: false })
                    );
                },
                error => {
                    this.props.notifications.error(error);
                    this.setState({ loading: false });
                })
        this.ultimoFiltro = filtroAgenda;
    }

    ejecutarCambiosAgenda = ({ added, changed, deleted }) => {
        const modeloAgenda = {
            startDate: undefined,
            endDate: undefined,
            title: '',
            allDay: false,
            rRule: '',
            exDate: '',
            notes: '',
            codigoEmpleado: '',
            tipoCitaMedica: 1,
            fuenteRemision: '',
        };
        let nuevosDatosAgenda = {};
        if (added) {
            nuevosDatosAgenda = { ...modeloAgenda, ...added };
            this.setState({
                abrirPopupCliente: true,
                agendaCreada: nuevosDatosAgenda
            });
        } else if (changed) {
            const idModificado = Object.keys(changed)[0];
            const agenda = this.state.agendas.find(agenda => agenda.id === idModificado);
            nuevosDatosAgenda = { ...agenda, ...changed[idModificado] };
            this.actualizarAgenda(nuevosDatosAgenda);
        } else {
            this.eliminarAgenda(deleted);
        }
    }

    actualizarAgenda = ({ id: idReagendar, ...restPropsAgenda }) => {
        const infoReagendar = {
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
        }
        agendasService.patchReagendar(idReagendar, infoReagendar)
            .then(
                datosReagenda => {
                    this.props.notifications.success("Se ha reagendado la cita correctamente");
                    const datosReagendaMapeada = mapAgendas(datosReagenda);
                    const agendasCargadas = this.state.agendas.filter(agenda => agenda.id !== idReagendar);
                    this.setState({ agendas: [...agendasCargadas, datosReagendaMapeada] });
                },
                error => {
                    this.props.notifications.error(error);
                })
    }

    eliminarAgenda = (codigoAgenda) => {
        const cancelarAgendaDto = {
            reagendar: false,
            desdeLlamada: false,
            observacion: "",
        };
        agendasService.deleteAgenda(codigoAgenda, cancelarAgendaDto)
            .then(
                respuesta => {
                    this.props.notifications.success("Se ha cancelado la agenda correctamente");
                    const agendasCargadas = this.state.agendas.filter(agenda => agenda.id !== codigoAgenda);
                    this.setState({ agendas: agendasCargadas });
                },
                error => {
                    this.props.notifications.error(error);
                })
    }

    abrirAgregarNuevaAgenda = () => {
        this.setState({ FormAgendarsoloLectura: false });
    }

    abrirEditarAgenda = (info) => {
        this.setState({ FormAgendarsoloLectura: !this.sePuedeModificar(info) });
    }

    sePuedeModificar = (datosAgenda) => {
        const hoy = dayjs(new Date());
        if (datosAgenda) {
            const { endDate, estado } = datosAgenda;
            return estado == EstadoAgenda.AGENDADO || !dayjs(endDate).isBefore(hoy);
        } else {
            return false;
        }
    }

    popUpClienteCerrado = () => {
        this.setState({ abrirPopupCliente: false });
    }

    popUpClienteCancelado = () => {
        this.setState({ abrirPopupCliente: false });
    };

    citaAgendada = (infoAgenda) => {
        this.setState({ abrirPopupCliente: false });
        this.cargarAgendas();
    }

    handleChangeCmbCiudad = e => {
        const { value: ciudadFiltrar } = e.target;
        this.setState({ ciudadFiltrar, sucursalFiltrar: '' });
    }

    handleChangeCmbSucursal = e => {
        const { value: sucursalFiltrar } = e.target;
        this.setState({ sucursalFiltrar, empleadoFiltrar: '' });
    }

    handleChangeCmbEmpleado = e => {
        const { value } = e.target;
        this.setState({ empleadoFiltrar: value });
    }

    beforeAppointmentClick = (info, clickFn) => {
        const agendaModificableSeleccionada = this.sePuedeModificar(info.data);
        this.setState({ agendaModificableSeleccionada }, clickFn(info));
    }

    agendaSeleccionadaBuscador = (event, agenda, reason) => {
        if (reason == RazonesCambioAutocomplete.OPCION_SELECCIONADA) {
            this.setState({ fechaActual: agenda.fechaInicio });
        }
    }

    filtroEstadoAgendaCambiado = (filtroEstadosAgendaSeleccionados) => {
        this.setState({
            filtroEstadosAgendaSeleccionados
        })
        this.cargarAgendas();
    }

    render() {
        const {
            agendas,
            loading,
            fechaActual,
            vistaActual,
            localizacion,
            FormAgendarsoloLectura,
            recursosEmpleados,
            agendaModificableSeleccionada,
            abrirPopupCliente,
            agendaCreada,
            recursosTiposCitasMedicas,
            ciudadFiltrar,
            sucursalFiltrar,
            empleadoFiltrar,
            recursosFuentesRemision,
            filtroEstadosAgendaBloqueados,
            filtroEstadosAgendaSeleccionados,
        } = this.state;
        const empleadosCargados = !checkEmptyObject(recursosEmpleados);
        const tiposCitasCargadas = !checkEmptyObject(recursosTiposCitasMedicas);
        const fuentesRemisionCargadas = !checkEmptyObject(recursosFuentesRemision);
        let recursos = empleadosCargados ? [recursosEmpleados] : [];
        if (tiposCitasCargadas) {
            recursos.push(recursosTiposCitasMedicas);
        }
        if (fuentesRemisionCargadas) {
            recursos.push(recursosFuentesRemision);
        }

        return (
            <Grid container spacing={1}>
                <Grid item xs={12}>
                    <BuscadorCitaPaciente
                        onChange={this.agendaSeleccionadaBuscador}
                    />
                </Grid>
                <Grid item xs={4}>
                    <CmbCiudadSucursales
                        incluirItemTodas={true}
                        valorInicialSeleccionado={ciudadFiltrar}
                        onValorSeleccionado={this.handleChangeCmbCiudad}
                        soloParaAgendar={true}
                        formControlProps={{
                            variant: "outlined",
                            fullWidth: true,
                            size: "small",
                        }}
                    />
                </Grid>
                <Grid item xs={4}>
                    <CmbSucursales
                        incluirItemTodas={true}
                        valorInicialSeleccionado={sucursalFiltrar}
                        ciudadFiltrar={ciudadFiltrar}
                        onValorSeleccionado={this.handleChangeCmbSucursal}
                        soloParaAgendar={true}
                        formControlProps={{
                            variant: "outlined",
                            fullWidth: true,
                            size: "small",
                        }}
                    />
                </Grid>
                <Grid item xs={4}>
                    <CmbEmpleados
                        incluirItemTodas={true}
                        valorInicialSeleccionado={empleadoFiltrar}
                        ciudadFiltrar={ciudadFiltrar}
                        sucursalFiltrar={sucursalFiltrar}
                        soloPuedeAgendar={true}
                        onValorSeleccionado={this.handleChangeCmbEmpleado}
                        formControlProps={{
                            variant: "outlined",
                            fullWidth: true,
                            size: "small",
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <FiltroEstadoAgenda
                        onChange={this.filtroEstadoAgendaCambiado}
                        fixedOptions={filtroEstadosAgendaBloqueados}
                        selectedOptions={filtroEstadosAgendaSeleccionados}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Paper>
                        <Scheduler
                            data={agendas}
                            locale={localizacion}
                            firstDayOfWeek={1}
                            height={window.innerHeight - 205}
                        >
                            <EditingState onCommitChanges={this.ejecutarCambiosAgenda} onAddedAppointmentChange={this.abrirAgregarNuevaAgenda} onEditingAppointmentChange={this.abrirEditarAgenda} />
                            <ViewState
                                currentDate={fechaActual}
                                currentViewName={vistaActual}
                                onCurrentViewNameChange={this.vistaActualCambiada}
                                onCurrentDateChange={this.fechaActualCambiada}
                            />
                            <DayView
                                displayName="Día"
                                startDayHour={7}
                                endDayHour={22}
                                timeTableCellComponent={this.CustomDayTimeTableCell}
                            />
                            <WeekView
                                displayName="Semana"
                                startDayHour={7}
                                endDayHour={22}
                                timeTableCellComponent={this.CustomWeekTimeTableCell}
                                dayScaleCellComponent={this.ClickableDayScaleCell}
                            />
                            <MonthView
                                displayName="Mes"
                                timeTableCellComponent={this.CustomMonthDayCell}
                            />
                            <Appointments
                                appointmentComponent={this.appointmentComponent}
                                appointmentContentComponent={CustomAppointmentContent}
                            />
                            <Toolbar
                                {...loading ? { rootComponent: ToolbarWithLoading } : null}
                            />
                            <DateNavigator />
                            <TodayButton messages={localizacionBtnHoy} />
                            <ViewSwitcher />
                            <EditRecurrenceMenu />
                            <AppointmentTooltip
                                showDeleteButton={agendaModificableSeleccionada}
                                showOpenButton
                                showCloseButton
                                commandButtonComponent={this.CommandButton}
                                contentComponent={CustomAppointmentTooltipContent}
                            />
                            <AppointmentForm
                                messages={localizacionFormAgenda}
                                commandLayoutComponent={this.CommandLayout}
                                overlayComponent={this.Overlay}
                                onVisibilityChange={this.cambioVisibilidadFormAgenda}
                                readOnly={FormAgendarsoloLectura || loading}
                                dateEditorComponent={this.dateEditorComponent}
                            />
                            <AllDayPanel messages={localizacionAllDayPanel} />
                            <DragDropProvider allowDrag={this.sePuedeModificar} allowResize={this.sePuedeModificar} />
                            <Resources data={recursos} palette={colorPalette} />
                            <ConfirmationDialog messages={localizacionConfirmationDialog} overlayComponent={this.OverlayConfirmationDialog} />
                            <CurrentTimeIndicator shadePreviousAppointments shadePreviousCells />
                        </Scheduler>
                        <DialogClienteAgenda
                            datosAgenda={agendaCreada}
                            open={abrirPopupCliente}
                            onClose={this.popUpClienteCerrado}
                            onCancelClick={this.popUpClienteCancelado}
                            onCitaAgendada={this.citaAgendada}
                        />
                    </Paper>
                </Grid>
            </Grid>
        );
    }

    sePuedeAgendarEnFecha = ({ endDate }) => {
        const sePuedeAgendar = dayjs(endDate).isAfter(new Date());
        if (!sePuedeAgendar) {
            this.props.notifications.error("No se puede crear una agenda en una fecha anterior a la actual");
        }
        return sePuedeAgendar;
    }

    dateEditorComponent = (props) => {
        return <AppointmentForm.DateEditor
            {...props}
            inputVariant="outlined"
            ampm={false}
            minDateMessage="La fecha no puede ser menor a la actual"
            disablePast={!this.state.FormAgendarsoloLectura}
            format={props.excludeTime ? "DD/MM/YYYY" : "DD/MM/YYYY HH:mm"}
        />
    }

    OverlayConfirmationDialog = (props) => {
        return <ConfirmationDialog.Overlay
            {...props}
            target={this.MainAgendaRef}
        />
    };

    Overlay = (props) => {
        return <AppointmentForm.Overlay
            {...props}
            target={this.MainAgendaRef}
        />
    };

    CommandLayout = (props) => {
        return <AppointmentForm.CommandLayout
            {...props}
            hideDeleteButton={!this.state.agendaModificableSeleccionada}
        />
    };
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(Agenda);


const DialogClienteStyles = theme => ({
    circularInsideButton: {
        marginRight: theme.spacing(),
    },
});

class DialogClienteBase extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            puedeGrabar: false,
            grabando: false,
            datosCliente: {},
        };
    }

    componentDidMount() {
    }

    componentDidUpdate(prevProps) {

    }

    cambioModoFormCliente = (modo) => {
        this.setState({ puedeGrabar: modo === TiposForm.VISUALIZAR });
    }

    clienteCargado = (datosCliente) => {
        this.setState({ datosCliente });
    }

    grabarCita = () => {
        this.setState({ grabando: true });
        const { datosAgenda: agendaCreada } = this.props;
        const datosEnviar = {
            cliente: this.state.datosCliente.codigo,
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
        };
        agendasService.postAgendarCita(datosEnviar)
            .then(
                agendaGrabada => {
                    this.props.notifications.success("Se ha agendado la cita correctamente");
                    this.props.onCitaAgendada(agendaGrabada);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ grabando: false });
            });
    }

    render() {
        const { classes } = this.props;
        const { puedeGrabar, grabando } = this.state;

        return (
            <ResponsiveDialog
                aria-labelledby="tituloDialogCliente"
                open={this.props.open}
                onClose={this.props.onClose}
                fullWidth={true}
                maxWidth="lg"
            >
                <DialogTitle id="tituloDialogCliente">
                    Seleccionar cliente
                </DialogTitle>
                <DialogContent>
                    <FormCliente
                        onCambioModoForm={this.cambioModoFormCliente}
                        onClienteSeleccionado={this.clienteCargado}
                        onClienteEditado={this.clienteCargado}
                        onClienteRegistrado={this.clienteCargado}
                    />
                </DialogContent>
                <DialogActions>
                    <Grid container direction="row" justify="space-between">
                        <Button onClick={this.props.onCancelClick} color="secondary">
                            Cancelar
                        </Button>
                        <div>
                            <Button onClick={this.grabarCita} color="primary" disabled={grabando || !puedeGrabar} autoFocus>
                                {
                                    grabando ?
                                        <Fragment>
                                            <CircularProgress size={14} color="primary" className={classes.circularInsideButton} />
                                            Grabando...
                                        </Fragment>
                                        :
                                        'Grabar'
                                }
                            </Button>
                        </div>
                    </Grid>
                </DialogActions>
            </ResponsiveDialog>
        );
    }
}

export const DialogClienteAgenda = connect(null, mapDispatchToProps)(withStyles(DialogClienteStyles)(DialogClienteBase));