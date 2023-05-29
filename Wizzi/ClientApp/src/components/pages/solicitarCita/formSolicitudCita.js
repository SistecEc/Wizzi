// REACT
import React, { Fragment, Component } from "react";

// REDUX
import { connect } from "react-redux";

//ESTILOS
import {
    Typography,
    FormControlLabel,
    Checkbox,
    TextField,
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    Grid,
    CircularProgress,
    Button,
    withStyles,
    Avatar,
    Link,
    Box,
    InputAdornment
} from "@material-ui/core";

import HearingOutlinedIcon from '@material-ui/icons/HearingOutlined';

import { notificationActions } from "../../../store/actions";
import { bindActionCreators } from "redux";
import { citasService, relacionesRepPacService } from "../../../services";
import CmbCiudadSucursales from "../../UI/CmbCiudadSucursales";
import { KeyboardDatePicker, KeyboardDateTimePicker } from "@material-ui/pickers";
import CmbSucursales from "../../UI/CmbSucursales";

const styles = theme => ({
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
        color: 'white'
    },
    circularProgress: {
        marginRight: theme.spacing(),
    },
});

function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Powered by '}
            <Link color="inherit" href="http://sistec.com.ec/">
                Sistec
            </Link>
            {' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const initialState = {
    cargandoRelacionesPaciente: true,
    relacionesPaciente: [],
    isLoading: false,
    esPaciente: true,
    nombrePaciente: '',
    apellidoPaciente: '',
    telefonoPaciente: '',
    emailPaciente: '',
    generoPaciente: '',
    fechaNacimiento: new Date(),
    nombreRepresentante: '',
    apellidoRepresentante: '',
    telefonoRepresentante: '',
    emailRepresentante: '',
    fechaTentativa: new Date(),
    observacion: '',
    relacionRepresentantePaciente: 1,
    ciudad: '',
    sucursal: '',
    subCampaniaOrigen: '',
}

class FormSolicitudCita extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        this.cargarRelacionesPaciente();
    }

    componentDidUpdate(prevProps) {
        if (this.props.subcampania !== prevProps.subcampania) {
            this.setState({ subCampaniaOrigen: this.props.subcampania });
        }
    }

    cargarRelacionesPaciente = () => {
        relacionesRepPacService.getRelaciones()
            .then(
                relacionesPaciente => {
                    this.setState({ cargandoRelacionesPaciente: false, relacionesPaciente, });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    }

    handleChangeChk = (e) => {
        const { name, checked } = e.target;
        this.setState({ [name]: checked });
    }

    handleChangetxt = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value.toUpperCase() });
    }

    handleChangetxtTelefono = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value.slice(0, 9) });
    }

    handleChangetxtFechaNacimiento = (e) => {
        this.setState({ fechaNacimiento: e });
    }

    handleChangetxtFechaTentativa = (e) => {
        this.setState({ fechaTentativa: e });
    }

    handleChangeCmb = e => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    };

    handleChangeCmbCiudad = e => {
        const { value: ciudad } = e.target;
        this.setState({ ciudad });
    };

    handleChangeCmbSucursal = e => {
        const { value: sucursal } = e.target;
        this.setState({ sucursal });
    };

    errorEnCalendario = (error, valor) => {
        if (error) {
            this.props.notifications.error(`Error: ${error} - ${valor}`);
        }
    }

    handleSubmit = e => {
        e.preventDefault();
        this.setState({ isLoading: true });

        const { isLoading,
            cargandoRelacionesPaciente,
            relacionesPaciente,
            ...infoSolicitud } = this.state;
        infoSolicitud.fechaTentativa = infoSolicitud.fechaTentativa.toISOString();
        infoSolicitud.fechaNacimiento = infoSolicitud.fechaNacimiento.toISOString();

        citasService.grabarSolicitudCita(infoSolicitud)
            .then(
                solicitudGrabada => {
                    this.props.onSubcampaniaGrabada(solicitudGrabada);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    };

    render() {
        const { classes } = this.props;
        const {
            cargandoRelacionesPaciente,
            relacionesPaciente,
            isLoading,
            esPaciente,
            nombrePaciente,
            apellidoPaciente,
            telefonoPaciente,
            emailPaciente,
            generoPaciente,
            fechaNacimiento,
            nombreRepresentante,
            apellidoRepresentante,
            telefonoRepresentante,
            emailRepresentante,
            fechaTentativa,
            observacion,
            relacionRepresentantePaciente,
            ciudad,
            sucursal,
        } = this.state;

        return (
            <Fragment>
                <Avatar className={classes.avatar}>
                    <HearingOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Solicitar cita
                </Typography>
                <form className={classes.form} onSubmit={this.handleSubmit}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={esPaciente}
                                        onChange={this.handleChangeChk}
                                        value={esPaciente}
                                        id="chkEsPaciente"
                                        name="esPaciente"
                                        color="primary"
                                    />
                                }
                                label="Usted es el interesado"
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                variant="outlined"
                                id="txtNombrePaciente"
                                name="nombrePaciente"
                                label="Nombres"
                                value={nombrePaciente}
                                onChange={this.handleChangetxt}
                                autoFocus
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                variant="outlined"
                                id="txtApellidoPaciente"
                                name="apellidoPaciente"
                                label="Apellidos"
                                value={apellidoPaciente}
                                onChange={this.handleChangetxt}
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <FormControl variant="outlined" fullWidth required>
                                <InputLabel id="lblGeneroPaciente">Genero</InputLabel>
                                <Select
                                    labelId="lblGeneroPaciente"
                                    id="cmbGeneroPaciente"
                                    name="generoPaciente"
                                    value={generoPaciente}
                                    onChange={this.handleChangeCmb}
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
                                value={fechaNacimiento}
                                views={["year", "month", "date"]}
                                onChange={this.handleChangetxtFechaNacimiento}
                                onError={this.errorEnCalendario}
                                format="DD/MM/YYYY"
                                fullWidth
                                disableFuture
                            />
                        </Grid>
                        {
                            esPaciente ?
                                <Fragment>
                                    <Grid item xs={12}>
                                        <TextField
                                            variant="outlined"
                                            id="txtTelefonoPaciente"
                                            name="telefonoPaciente"
                                            label="Teléfono"
                                            value={telefonoPaciente}
                                            onChange={this.handleChangetxtTelefono}
                                            InputProps={
                                                {
                                                    startAdornment: <InputAdornment position="start">+593</InputAdornment>
                                                }
                                            }
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField
                                            variant="outlined"
                                            id="txtEmailPaciente"
                                            name="emailPaciente"
                                            label="Email"
                                            value={emailPaciente}
                                            onChange={this.handleChangetxt}
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                </Fragment>
                                :
                                <Fragment>
                                    <Grid item xs={12} sm={6}>
                                        <TextField
                                            variant="outlined"
                                            id="txtNombreRepresentante"
                                            name="nombreRepresentante"
                                            label="Nombres representante"
                                            value={nombreRepresentante}
                                            onChange={this.handleChangetxt}
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={6}>
                                        <TextField
                                            variant="outlined"
                                            id="txtApellidoRepresentante"
                                            name="apellidoRepresentante"
                                            label="Apellidos representante"
                                            value={apellidoRepresentante}
                                            onChange={this.handleChangetxt}
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <FormControl variant="outlined" fullWidth required disabled={cargandoRelacionesPaciente}>
                                            <InputLabel id="lblRelacionPaciente">
                                                {
                                                    cargandoRelacionesPaciente ?
                                                        <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                                        :
                                                        null
                                                }
                                                Relación
                                            </InputLabel>
                                            <Select
                                                labelId="lblRelacionPaciente"
                                                id="cmbRelacionPaciente"
                                                name="relacionRepresentantePaciente"
                                                value={relacionRepresentantePaciente}
                                                onChange={this.handleChangeCmb}
                                            >
                                                {
                                                    relacionesPaciente.map((relacion, i) =>
                                                        <MenuItem key={i} value={relacion.codigo}>{relacion.descripcion}</MenuItem>
                                                    )
                                                }
                                            </Select>
                                        </FormControl>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField
                                            variant="outlined"
                                            id="txtTelefonoRepresentante"
                                            name="telefonoRepresentante"
                                            label="Teléfono representante"
                                            value={telefonoRepresentante}
                                            onChange={this.handleChangetxt}
                                            InputProps={
                                                {
                                                    startAdornment: <InputAdornment position="start">+593</InputAdornment>
                                                }
                                            }
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <TextField
                                            variant="outlined"
                                            id="txtEmailRepresentante"
                                            name="emailRepresentante"
                                            label="Email representante"
                                            value={emailRepresentante}
                                            onChange={this.handleChangetxt}
                                            fullWidth
                                            required
                                        />
                                    </Grid>
                                </Fragment>
                        }
                        <Grid item xs={12}>
                            <CmbCiudadSucursales
                                valorInicialSeleccionado={ciudad}
                                onValorSeleccionado={this.handleChangeCmbCiudad}
                                formControlProps={{
                                    variant: "outlined",
                                    fullWidth: true,
                                    required: true
                                }} />
                        </Grid>
                        {
                            ciudad !== '' ?
                                <Grid item xs={12}>
                                    <CmbSucursales
                                        incluirItemTodas={true}
                                        valorInicialSeleccionado={sucursal}
                                        ciudadFiltrar={ciudad}
                                        onValorSeleccionado={this.handleChangeCmb}
                                        formControlProps={{
                                            variant: "outlined",
                                            fullWidth: true,
                                        }}
                                    />
                                </Grid>
                                :
                                null
                        }
                        <Grid item xs={12}>
                            <KeyboardDateTimePicker
                                inputVariant="outlined"
                                label="Fecha de cita tentativa"
                                value={fechaTentativa}
                                onChange={this.handleChangetxtFechaTentativa}
                                onError={this.errorEnCalendario}
                                format="DD/MM/YYYY HH:mm:ss"
                                ampm={false}
                                fullWidth
                                disablePast
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                id="txtObservacion"
                                name="observacion"
                                label="Consultas o Comentarios"
                                value={observacion}
                                onChange={this.handleChangetxt}
                                multiline
                                rows="4"
                                fullWidth
                                required
                            />
                        </Grid>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            fullWidth
                            className={classes.submit}
                            disabled={isLoading}
                        >
                            {isLoading ?
                                <Fragment>
                                    <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                    <Typography>Solicitando...</Typography>
                                </Fragment>
                                :
                                <Typography>Solicitar cita</Typography>
                            }
                        </Button>
                        <Box mt={5}>
                            <Copyright />
                        </Box>
                    </Grid>
                </form>
            </Fragment>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(FormSolicitudCita));
