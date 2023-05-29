import { connectProps } from '@devexpress/dx-react-core';
import { EditingState, ViewState } from '@devexpress/dx-react-scheduler';
import {
    AllDayPanel, AppointmentForm,
    Appointments,
    AppointmentTooltip,











    ConfirmationDialog,

    CurrentTimeIndicator, DateNavigator,
    DayView,







    DragDropProvider, EditRecurrenceMenu,
    MonthView,






    Resources, Scheduler,
    TodayButton,
    Toolbar,
    ViewSwitcher,
    WeekView
} from '@devexpress/dx-react-scheduler-material-ui';
import { Grid, Paper } from '@material-ui/core';
import { blue } from '@material-ui/core/colors';
import { withStyles } from '@material-ui/core/styles';
import * as dayjs from 'dayjs';
import * as React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { colorPalette, localizacionAllDayPanel, localizacionBtnHoy, localizacionConfirmationDialog, localizacionFormAgenda } from '../../../helpers';
import { checkEmptyObject } from '../../../helpers/utils';
import { agendasService, empleadosService, tiposCitasMedicasService } from '../../../services';
import { fuentesRemisionService } from '../../../services/fuentesRemision.service';
import { notificationActions } from '../../../store/actions';
import CmbCiudadSucursales from '../../UI/CmbCiudadSucursales';
import CmbEmpleados from '../../UI/CmbEmpleados';
import CmbSucursales from '../../UI/CmbSucursales';
import { ClickableDayScaleCellComponent, CustomAppointmentTooltipContent, CustomDayTimeTableCellComponent, CustomMonthDayCellComponent, CustomWeekTimeTableCellComponent, ToolbarWithLoading } from '../../UI/Scheduler';
import { CommandButtonComponent } from '../../UI/Scheduler/CommandButtonComponent';
import { CustomAppointmentComponent } from '../../UI/Scheduler/CustomAppointmentComponent';
import { CustomAppointmentContent } from '../../UI/Scheduler/CustomAppointmentContent';


const FormStyles = {
    AgendaEditable: {
        borderStyle: 'dashed',
        borderWidth: 3,
        borderColor: blue[500],
        borderRadius: 5,
    },
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

const initialState = {
    loading: true,
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
    agendaModificarSeleccionada: false,
    agendaModificar: {},
};

const EsElMismoFiltro = (filtroNuevo, UltimoFiltro) => {
    return filtroNuevo.inicio === UltimoFiltro?.inicio
        && filtroNuevo.fin === UltimoFiltro?.fin
        && filtroNuevo?.empleado === UltimoFiltro?.empleado
        && filtroNuevo?.ciudad === UltimoFiltro?.ciudad
        && filtroNuevo?.sucursal === UltimoFiltro?.sucursal;
}

class FormReagendar extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = initialState;

        this.CommandButton = connectProps(CommandButtonComponent, () => {
            const { agendaModificarSeleccionada } = this.state;
            return {
                mostrarVisualizar: agendaModificarSeleccionada,
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
                condicionMarcarEditable: this.esAgendaModificar,
            };
        });

    }

    componentDidMount() {
        this.cargarRecursos();
        this.cargarAgendas();
        const { agendaModificar } = this.props;
        this.setState({
            agendaModificar,
            fechaActual: agendaModificar.startDate
        });
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
        const { fechaActual, vistaActual, empleadoFiltrar, ciudadFiltrar, sucursalFiltrar } = this.state;
        const inicio = dayjs(fechaActual).startOf(vistaActual.toLowerCase());
        const fin = inicio.clone().endOf(vistaActual.toLowerCase());
        return {
            inicio: inicio.toISOString(),
            fin: fin.toISOString(),
            empleado: empleadoFiltrar,
            ciudad: ciudadFiltrar,
            sucursal: sucursalFiltrar,
            atendida: false,
            cancelada: false,
            reagendada: false,
        };
    };

    vistaActualCambiada = (vistaActual) => {
        this.setState({ vistaActual, loading: true });
    };

    fechaActualCambiada = (fechaActual) => {
        this.setState({ fechaActual, loading: true });
    };

    sePuedeAgendarEnFecha = ({ endDate }) => {
        const sePuedeAgendar = dayjs(endDate).isAfter(new Date());
        if (!sePuedeAgendar) {
            this.props.notifications.error("No se puede crear una agenda en una fecha anterior a la actual");
        }
        return sePuedeAgendar;
    }

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
                    const agendas = agendasGrabadas ? agendasGrabadas.filter(agenda => agenda.codigo !== this.state.agendaModificar.codigo).map(mapAgendas) : [];
                    this.setState({
                        agendas,
                    },
                        this.setState({ loading: false })
                    );
                },
                error => {
                    this.props.notifications.error(error);
                    this.setState({ loading: false });
                });
        this.ultimoFiltro = filtroAgenda;
    }

    ejecutarCambiosAgenda = ({ changed }) => {
        if (changed) {
            const { agendaModificar } = this.state;
            const nuevosDatosAgenda = { ...agendaModificar, ...changed[agendaModificar.id] };
            this.setState({ agendaModificar: nuevosDatosAgenda });
            this.props.onAgendaModificada(nuevosDatosAgenda);
        }
    }

    abrirAgregarNuevaAgenda = () => {
        this.setState({ FormAgendarsoloLectura: false });
    }

    abrirEditarAgenda = (info) => {
        this.setState({ FormAgendarsoloLectura: !this.esAgendaModificar(info) });
    }

    esAgendaModificar = (datosAgenda) => {
        return datosAgenda?.id === this.state.agendaModificar.id;
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
        const agendaModificarSeleccionada = this.esAgendaModificar(info.data);
        this.setState({ agendaModificarSeleccionada }, clickFn(info));
    }

    render() {
        const {
            agendas,
            agendaModificar,
            loading,
            fechaActual,
            vistaActual,
            localizacion,
            FormAgendarsoloLectura,
            recursosEmpleados,
            recursosTiposCitasMedicas,
            ciudadFiltrar,
            sucursalFiltrar,
            empleadoFiltrar,
            recursosFuentesRemision,
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
        const data = [...agendas, agendaModificar];

        return (
            <Grid container spacing={2}>
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
                    <Paper>
                        <Scheduler
                            data={data}
                            locale={localizacion}
                            firstDayOfWeek={1}
                            height={window.innerHeight - 300}
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
                                showDeleteButton={false}
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
                                readOnly={FormAgendarsoloLectura}
                                dateEditorComponent={this.dateEditorComponent}
                            />
                            <AllDayPanel messages={localizacionAllDayPanel} />
                            <DragDropProvider allowDrag={this.esAgendaModificar} allowResize={this.esAgendaModificar} />
                            <Resources data={recursos} palette={colorPalette} />
                            <ConfirmationDialog messages={localizacionConfirmationDialog} overlayComponent={this.OverlayConfirmationDialog} />
                            <CurrentTimeIndicator shadePreviousAppointments shadePreviousCells />
                        </Scheduler>
                    </Paper>
                </Grid>
            </Grid>
        );
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
            target={this.props.refOverlay}
        />
    };

    Overlay = (props) => {
        return <AppointmentForm.Overlay
            {...props}
            target={this.props.refOverlay}
        />
    };

    CommandLayout = (props) => {
        return <AppointmentForm.CommandLayout
            {...props}
            hideDeleteButton={true}
        />
    };

}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(FormStyles)(FormReagendar));