import { Button, Checkbox, CircularProgress, FormControl, FormControlLabel, Grid, InputAdornment, InputLabel, MenuItem, Select, TextField, Typography, withStyles } from '@material-ui/core';
import { Cached, Close, Edit, Search } from '@material-ui/icons/';
import { KeyboardDatePicker } from '@material-ui/pickers';
import * as dayjs from 'dayjs';
import React, { Component, Fragment } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { TipoIdentificacion, TiposForm } from '../../../enums';
import { clientesService, tiposIdentificacionService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import Cascadalocalizacion from '../../UI/Cascadalocalizacion';
import BuscadorCliente from '../clientes/BuscadorCliente';

const initialState = {
    modoForm: TiposForm.BUSCADOR,
    grabando: false,
    cargandoTiposIdentificacion: false,
    listaTiposIdentificacion: [],
    codigo: '',
    tipoIdentificacion: '',
    identificacion: '',
    nombreComercial: '',
    prioridadNombreComercial: false,
    nombre: '',
    apellido: '',
    direccion: '',
    telefono: '',
    email: '',
    genero: '',
    fechaNacimiento: new Date(),
    localizacion: {
        pais: '',
        provincia: '',
        canton: '',
        parroquia: ''
    },
};

const styles = theme => ({
    innerCircularProgress: {
        marginRight: theme.spacing(),
    },
    cancelarActualizar: {
        marginRight: theme.spacing(),
    }
});

class FormCliente extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        this.cargarTiposIdentificacion();
        this.cargarEstado();
    }

    componentDidUpdate(prevProps) {
        const { datosPrecargados: datosAntiguos, modoForm: modoFormAntiguo } = prevProps;
        const { datosPrecargados, modoForm } = this.props;

        if (JSON.stringify(datosPrecargados) !== JSON.stringify(datosAntiguos) || modoFormAntiguo !== modoForm) {
            this.cargarEstado();
        }
    }

    cargarTiposIdentificacion = () => {
        this.setState({ cargandoTiposIdentificacion: true });
        tiposIdentificacionService.getTiposIdentificacion()
            .then(
                listaTiposIdentificacion => {
                    this.setState({ listaTiposIdentificacion });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargandoTiposIdentificacion: false });
            });
    }

    cargarEstado = () => {
        const { datosPrecargados, modoForm } = this.props;
        if (datosPrecargados) {
            const { tipoIdentificacion, localizacion, ...infoCliente } = datosPrecargados;
            let nuevoEstado = {
                modoForm,
                ...infoCliente,
            };
            if (tipoIdentificacion) {
                nuevoEstado.tipoIdentificacion = tipoIdentificacion.codigo;
            }
            if (localizacion) {
                nuevoEstado.localizacion = {
                    pais: localizacion.pais.codigo,
                    provincia: localizacion.provincia.codigo,
                    canton: localizacion.canton.codigo,
                    parroquia: localizacion.parroquia?.codigo,
                }
            }
            this.setState(nuevoEstado);
        }
    }

    handleChangetxt = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value.toUpperCase() });
    }

    handleChangetxtTelefono = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value.slice(0, 9) });
    }

    handleChangeChk = (e) => {
        const { name, checked } = e.target;
        this.setState({ [name]: checked });
    }

    handleChangeCmb = e => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    };

    handleChangeCmbTipoIdentificacion = e => {
        const { name, value } = e.target;
        if (value == TipoIdentificacion.POTENCIAL) {
            this.setState({ [name]: value, identificacion: '' });
        } else {
            this.setState({ [name]: value });
        }
    };

    onChangeTxtDate = (e) => {
        this.setState({ fechaNacimiento: e.toDate() });
    }

    errorEnCalendario = (error, valor) => {
        if (error) {
            this.props.notifications.error(`Error: ${error} - ${valor}`);
        }
    }

    clickCancelarActualizar = () => {
        this.cambiarModoForm(TiposForm.VISUALIZAR);
    }

    clickActualizarCliente = () => {
        const { modoForm, grabando, listaTiposIdentificacion, ...datosEditadosCliente } = this.state;
        this.setState({ grabando: true });
        clientesService.actualizarCliente(datosEditadosCliente)
            .then(
                datosActualizados => {
                    this.props.notifications.success("Actualizado correctamente");
                    this.dispararClienteEditado(datosActualizados);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ grabando: false });
            });
    }

    clickRegistrarCliente = () => {
        this.setState({ grabando: true });
        const { modoForm, grabando, fechaNacimiento, ...datosClienteRegistrar } = this.state;
        clientesService.grabarCliente(datosClienteRegistrar)
            .then(
                datosClienteGrabado => {
                    this.props.notifications.success("Registrado correctamente");
                    this.dispararClienteRegistrado(datosClienteGrabado);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ grabando: false });
            });
    }

    onEditarClick = () => {
        this.cambiarModoForm(TiposForm.EDICION);
    }

    onCambiarClick = () => {
        this.cambiarModoForm(TiposForm.BUSCADOR);
    }

    cambiarModoForm = (modoForm) => {
        this.setState({ modoForm }, this.dispararCambioModoForm(modoForm));
    }

    dispararCambioModoForm = (modoForm) => {
        const { onCambioModoForm } = this.props;
        if (onCambioModoForm) {
            onCambioModoForm(modoForm);
        }
    }

    dispararClienteSeleccionado = (datosClienteSeleccionado) => {
        this.cargarCliente(datosClienteSeleccionado);
        const { onClienteSeleccionado } = this.props;
        if (onClienteSeleccionado) {
            onClienteSeleccionado(datosClienteSeleccionado);
        }
    }

    dispararClienteEditado = (datosActualizados) => {
        this.cargarCliente(datosActualizados);
        const { onClienteEditado } = this.props;
        if (onClienteEditado) {
            onClienteEditado(datosActualizados);
        }
    }

    dispararClienteRegistrado = (datosClienteGrabado) => {
        this.cargarCliente(datosClienteGrabado);
        const { onClienteRegistrado } = this.props;
        if (onClienteRegistrado) {
            onClienteRegistrado(datosClienteGrabado);
        }
    }

    localizacionSeleccionada = (tipoLocalizacion, valorSeleccionado) => {
        let { localizacion } = this.state;
        switch (tipoLocalizacion) {
            case 'pais':
                localizacion.pais = valorSeleccionado;
                localizacion.provincia = '';
                localizacion.canton = '';
                localizacion.parroquia = '';
                break;

            case 'provincia':
                localizacion.provincia = valorSeleccionado;
                localizacion.canton = '';
                localizacion.parroquia = '';
                break;

            case 'canton':
                localizacion.canton = valorSeleccionado;
                localizacion.parroquia = '';
                break;

            case 'parroquia':
                localizacion.parroquia = valorSeleccionado;
                break;

            default:
        }
        this.setState({ localizacion });
    }

    cargarCliente = (datosClienteSeleccionado) => {
        const { tipoIdentificacion, localizacion, ...datosCliente } = datosClienteSeleccionado;
        this.setState({
            tipoIdentificacion: tipoIdentificacion.codigo,
            localizacion: {
                pais: localizacion?.pais.codigo,
                provincia: localizacion?.provincia.codigo,
                canton: localizacion?.canton.codigo,
                parroquia: localizacion?.parroquia.codigo,
            },
            ...datosCliente,
        });
        this.cambiarModoForm(TiposForm.VISUALIZAR);
    }

    abrirRegistrarCliente = () => {
        let { datosPrecargadosRegistrar } = this.props;
        if (!datosPrecargadosRegistrar) {
            datosPrecargadosRegistrar = {
                nombre: '',
                apellido: '',
                telefono: '',
                email: '',
                genero: '',
                fechaNacimiento: new Date(),
                localizacion: {
                    pais: '',
                    provincia: '',
                    canton: '',
                    parroquia: ''
                },
            };
        }

        const { localizacion, ...restDatosPrecargadosRegistrar } = datosPrecargadosRegistrar;

        this.setState({
            modoForm: TiposForm.NUEVO,
            codigo: '',
            tipoIdentificacion: '',
            identificacion: '',
            nombreComercial: '',
            prioridadNombreComercial: false,
            direccion: '',
            localizacion: {
                pais: localizacion?.pais?.codigo,
                provincia: localizacion?.provincia?.codigo,
                canton: localizacion?.canton?.codigo,
                parroquia: localizacion?.parroquia?.codigo,
            },
            ...restDatosPrecargadosRegistrar,
        });
    }

    render() {
        const { classes, buscarAlMontar } = this.props;
        const {
            modoForm,
            grabando,
            cargandoTiposIdentificacion,
            listaTiposIdentificacion,
            codigo,
            tipoIdentificacion,
            identificacion,
            nombreComercial,
            prioridadNombreComercial,
            nombre,
            apellido,
            direccion,
            telefono,
            email,
            genero,
            fechaNacimiento,
            localizacion
        } = this.state;
        return (
            <Grid container spacing={2}>
                {
                    codigo !== '' || modoForm !== TiposForm.BUSCADOR
                        ?
                        <Grid container item xs={12} justify="space-between">
                            <Button
                                size="small"
                                endIcon={codigo !== '' ? <Cached /> : <Search />}
                                onClick={this.onCambiarClick}>
                                {
                                    codigo !== ''
                                        ? 'Cambiar'
                                        : 'Buscar'
                                }
                            </Button>
                            {
                                codigo !== ''
                                    ?
                                    <Button
                                        size="small"
                                        endIcon={modoForm === TiposForm.VISUALIZAR ? <Edit /> : <Close />}
                                        onClick={modoForm === TiposForm.VISUALIZAR ? this.onEditarClick : this.clickCancelarActualizar}>
                                        {modoForm === TiposForm.VISUALIZAR
                                            ?
                                            'Editar'
                                            :
                                            'Cancelar'
                                        }
                                    </Button>
                                    :
                                    null
                            }
                        </Grid>
                        :
                        null
                }
                {
                    modoForm === TiposForm.BUSCADOR
                        ?
                        <Grid item xs={12}>
                            <BuscadorCliente
                                buscarAlMontar={buscarAlMontar}
                                onClienteSeleccionado={this.dispararClienteSeleccionado}
                                onRegistrarClick={this.abrirRegistrarCliente}
                            />
                        </Grid>
                        :
                        <Fragment>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    variant="outlined"
                                    id="txtNombre"
                                    name="nombre"
                                    label="Nombre"
                                    value={nombre}
                                    onChange={this.handleChangetxt}
                                    autoFocus
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    variant="outlined"
                                    id="txtApellido"
                                    name="apellido"
                                    label="Apellido"
                                    value={apellido}
                                    onChange={this.handleChangetxt}
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                />
                            </Grid>
                            <Grid item xs={10}>
                                <TextField
                                    variant="outlined"
                                    id="txNombreComercial"
                                    name="nombreComercial"
                                    label="NombreComercial"
                                    value={nombreComercial}
                                    onChange={this.handleChangetxt}
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                />
                            </Grid>
                            <Grid item xs={2}>
                                <FormControlLabel
                                    control={
                                        <Checkbox
                                            checked={prioridadNombreComercial}
                                            onChange={this.handleChangeChk}
                                            value={prioridadNombreComercial}
                                            id="chkPrioridadNombreComercial"
                                            name="prioridadNombreComercial"
                                            color="primary"
                                            disabled={modoForm === TiposForm.VISUALIZAR}
                                        />
                                    }
                                    label="Prioridad"
                                    labelPlacement="bottom"
                                />
                            </Grid>
                            <Grid item xs={3}>
                                <FormControl variant="outlined" fullWidth required disabled={cargandoTiposIdentificacion || modoForm === TiposForm.VISUALIZAR}>
                                    <InputLabel id="lblTipoIdentificacion">
                                        {
                                            cargandoTiposIdentificacion ?
                                                <CircularProgress size={14} color="primary" className={classes.innerCircularProgress} />
                                                :
                                                null
                                        }
                                Tipo de identificación
                            </InputLabel>
                                    <Select
                                        labelId="lblTipoIdentificacion"
                                        id="cmbTipoIdentificacion"
                                        name="tipoIdentificacion"
                                        value={tipoIdentificacion}
                                        onChange={this.handleChangeCmbTipoIdentificacion}
                                    >
                                        {listaTiposIdentificacion.map((tipoID, i) =>
                                            <MenuItem key={i} value={tipoID.codigo}>{tipoID.descripcion}</MenuItem>
                                        )}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={9}>
                                <TextField
                                    variant="outlined"
                                    id="txtIdentificacion"
                                    name="identificacion"
                                    label="Identificacion"
                                    value={identificacion}
                                    onChange={this.handleChangetxt}
                                    autoFocus
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR || tipoIdentificacion == TipoIdentificacion.POTENCIAL}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    variant="outlined"
                                    id="txtTelefono"
                                    name="telefono"
                                    label="Teléfono o Celular"
                                    value={telefono}
                                    onChange={this.handleChangetxtTelefono}
                                    InputProps={{
                                        startAdornment: <InputAdornment position="start">+593</InputAdornment>
                                    }}
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    variant="outlined"
                                    id="txtEmail"
                                    name="email"
                                    label="Email"
                                    value={email}
                                    onChange={this.handleChangetxt}
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                    helperText="Separar con ; si tiene dos o más correos."
                                />
                            </Grid>
                            <Cascadalocalizacion
                                paisSeleccionado={localizacion.pais}
                                provinciaSeleccionado={localizacion.provincia}
                                cantonSeleccionado={localizacion.canton}
                                parroquiaSeleccionado={localizacion.parroquia}
                                disabled={modoForm === TiposForm.VISUALIZAR}
                                onLocalizacionSeleccionada={this.localizacionSeleccionada}
                            />
                            <Grid item xs={12}>
                                <TextField
                                    variant="outlined"
                                    id="txtDireccion"
                                    name="direccion"
                                    label="Direccion"
                                    value={direccion}
                                    onChange={this.handleChangetxt}
                                    fullWidth
                                    required
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl variant="outlined" fullWidth required>
                                    <InputLabel id="lblGenero">Genero</InputLabel>
                                    <Select
                                        labelId="lblGenero"
                                        id="cmbGenero"
                                        name="genero"
                                        value={genero}
                                        onChange={this.handleChangeCmb}
                                        disabled={modoForm === TiposForm.VISUALIZAR}
                                    >
                                        <MenuItem value="M">Masculino</MenuItem>
                                        <MenuItem value="F">Femenino</MenuItem>
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <KeyboardDatePicker
                                    inputVariant="outlined"
                                    label="Fecha de nacimiento"
                                    value={dayjs(fechaNacimiento)}
                                    views={["year", "month", "date"]}
                                    onChange={this.onChangeTxtDate}
                                    onError={this.errorEnCalendario}
                                    format="DD/MM/YYYY"
                                    fullWidth
                                    disabled={modoForm === TiposForm.VISUALIZAR}
                                    disableFuture
                                />
                            </Grid>
                            {
                                modoForm === TiposForm.NUEVO || modoForm === TiposForm.EDICION
                                    ?
                                    <Grid container item justify="center">
                                        {
                                            modoForm === TiposForm.EDICION ?
                                                <Button
                                                    variant="contained"
                                                    color="primary"
                                                    type="submit"
                                                    className={classes.submit}
                                                    disabled={grabando}
                                                    onClick={this.clickActualizarCliente}
                                                >
                                                    {grabando ?
                                                        <Fragment>
                                                            <CircularProgress size={14} color="primary" className={classes.innerCircularProgress} />
                                                            <Typography>
                                                                Actualizando...
                                                                </Typography>
                                                        </Fragment>
                                                        :
                                                        <Typography>
                                                            Actualizar
                                                        </Typography>
                                                    }
                                                </Button>
                                                :
                                                <Button
                                                    variant="contained"
                                                    color="primary"
                                                    type="submit"
                                                    className={classes.submit}
                                                    disabled={grabando}
                                                    onClick={this.clickRegistrarCliente}
                                                >
                                                    {grabando ?
                                                        <Fragment>
                                                            <CircularProgress size={14} color="primary" className={classes.innerCircularProgress} />
                                                            <Typography>
                                                                Registrando...
                                                                </Typography>
                                                        </Fragment>
                                                        :
                                                        <Typography>
                                                            Registrar
                                                        </Typography>
                                                    }
                                                </Button>
                                        }
                                    </Grid>
                                    :
                                    null
                            }
                        </Fragment>
                }

            </Grid>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(FormCliente));
