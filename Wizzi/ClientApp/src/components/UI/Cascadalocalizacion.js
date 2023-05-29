// REACT
import React, { Component, Fragment } from "react";

// REDUX
import { connect } from "react-redux";

//ESTILOS
import {
    Select,
    MenuItem,
    InputLabel,
    FormControl,
    CircularProgress,
    withStyles,
    Grid,
} from "@material-ui/core";

import { bindActionCreators } from "redux";
import { notificationActions } from "../../store/actions";
import { localizacionesService } from "../../services";

const styles = theme => ({
    circularProgress: {
        marginRight: theme.spacing(),
    },
});

const initialState = {
    paisesCargados: false,
    cargandoPaises: false,
    cargandoProvincias: false,
    cargandoCantones: false,
    cargandoParroquias: false,
    paises: [],
    provincias: [],
    cantones: [],
    parroquias: [],
    paisSeleccionado: '',
    provinciaSeleccionado: '',
    cantonSeleccionado: '',
    parroquiaSeleccionado: '',
}

class Cascadalocalizacion extends Component {
    //_isMounted = false;

    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        this.cargarPaises();
        //this._isMounted = true;
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.paisesCargados !== this.state.paisesCargados) {
            this.cargarLocalizacion();
        }

    }

    componentWillUnmount() {
        //this._isMounted = false;
    }

    cargarLocalizacion = () => {
        const { paisSeleccionado, provinciaSeleccionado, cantonSeleccionado, parroquiaSeleccionado } = this.props;
        if (paisSeleccionado) {
            if (this.state.paises.filter(c => c.codigo === paisSeleccionado).length > 0) {
                this.setState({ paisSeleccionado });
                this.cargarProvincias(paisSeleccionado, provinciaSeleccionado, cantonSeleccionado, parroquiaSeleccionado);
            }
        }
    }

    cargarPaises = () => {
        this.setState({ cargandoPaises: true, paises: [], provincias: [], cantones: [], parroquias: [] });
        localizacionesService.getPaises(false)
            .then(
                paises => {
                    paises.push({ codigo: '', descripcion: 'Selecionar...' });
                    this.setState({ paises, paisesCargados: true });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargandoPaises: false });
            });
    }

    cargarProvincias = (paisSeleccionado, provinciaSeleccionado, cantonSeleccionado, parroquiaSeleccionado) => {
        this.setState({ cargandoProvincias: true, provincias: [], cantones: [], parroquias: [] });
        localizacionesService.getProvincias(paisSeleccionado, false)
            .then(
                provincias => {
                    provincias.push({ codigo: '', descripcion: 'Selecionar...' });
                    let nuevoEstado = {};
                    nuevoEstado.provincias = provincias;
                    if (provincias.filter(c => c.codigo === provinciaSeleccionado).length > 0) {
                        nuevoEstado.provinciaSeleccionado = provinciaSeleccionado;
                    }
                    this.setState(nuevoEstado, nuevoEstado.provinciaSeleccionado ? this.cargarCantones(provinciaSeleccionado, cantonSeleccionado, parroquiaSeleccionado) : null);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargandoProvincias: false });
            });
    }

    cargarCantones = (provinciaSeleccionado, cantonSeleccionado, parroquiaSeleccionado) => {
        this.setState({ cargandoCantones: true, cantones: [], parroquias: [] });
        localizacionesService.getCantones(provinciaSeleccionado, false)
            .then(
                cantones => {
                    cantones.push({ codigo: '', descripcion: 'Selecionar...' });
                    let nuevoEstado = {};
                    nuevoEstado.cantones = cantones;
                    if (cantones.filter(c => c.codigo === cantonSeleccionado).length > 0) {
                        nuevoEstado.cantonSeleccionado = cantonSeleccionado;
                    }
                    this.setState(nuevoEstado, nuevoEstado.cantonSeleccionado ? this.cargarParroquias(cantonSeleccionado, parroquiaSeleccionado) : null);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargandoCantones: false });
            });
    }

    cargarParroquias = (cantonSeleccionado, parroquiaSeleccionado) => {
        this.setState({ cargandoParroquias: true, parroquias: [] });
        localizacionesService.getParroquias(cantonSeleccionado, false)
            .then(
                parroquias => {
                    parroquias.push({ codigo: '', descripcion: 'Selecionar...' });
                    let nuevoEstado = {};
                    nuevoEstado.parroquias = parroquias;
                    if (parroquias.filter(c => c.codigo === parroquiaSeleccionado).length > 0) {
                        nuevoEstado.parroquiaSeleccionado = parroquiaSeleccionado;
                    }
                    this.setState(nuevoEstado);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargandoParroquias: false });
            });
    }

    handleChangeCmb = e => {
        const { name, value: valorSeleccionado } = e.target;
        switch (name) {
            case 'pais':
                this.setState({ paisSeleccionado: valorSeleccionado, provinciaSeleccionado: '', cantonSeleccionado: '', parroquiaSeleccionado: '' });
                this.cargarProvincias(valorSeleccionado);
                break;

            case 'provincia':
                this.setState({ provinciaSeleccionado: valorSeleccionado, cantonSeleccionado: '', parroquiaSeleccionado: '' });
                this.cargarCantones(valorSeleccionado);
                break;

            case 'canton':
                this.setState({ cantonSeleccionado: valorSeleccionado, parroquiaSeleccionado: '' });
                this.cargarParroquias(valorSeleccionado);
                break;

            case 'parroquia':
                this.setState({ parroquiaSeleccionado: valorSeleccionado });
                break;

            default:
                break;

        }
        this.props.onLocalizacionSeleccionada(name, valorSeleccionado);
    };

    render() {
        const { classes } = this.props;
        const { cargandoPaises, paises, paisSeleccionado,
            cargandoProvincias, provincias, provinciaSeleccionado,
            cargandoCantones, cantones, cantonSeleccionado,
            cargandoParroquias, parroquias, parroquiaSeleccionado } = this.state;

        return (
            <Fragment>
                <Grid item xs={12}>
                    <FormControl variant="outlined" fullWidth required disabled={cargandoPaises || this.props.disabled}>
                        <InputLabel id="lblPais">
                            {
                                cargandoPaises ?
                                    <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                    :
                                    null
                            }
                            País
                        </InputLabel>
                        <Select
                            labelId="lblPais"
                            id="cmbPais"
                            name="pais"
                            value={paisSeleccionado}
                            onChange={this.handleChangeCmb}
                        >
                            {paises.map((pais, i) =>
                                <MenuItem key={i} value={pais.codigo}>{pais.descripcion}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    <FormControl variant="outlined" fullWidth required disabled={cargandoProvincias || this.props.disabled}>
                        <InputLabel id="lblProvincia">
                            {
                                cargandoProvincias ?
                                    <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                    :
                                    null
                            }
                            Provincia
                        </InputLabel>
                        <Select
                            labelId="lblProvincia"
                            id="cmbProvincia"
                            name="provincia"
                            value={provinciaSeleccionado}
                            onChange={this.handleChangeCmb}
                        >
                            {provincias.map((provincia, i) =>
                                <MenuItem key={i} value={provincia.codigo}>{provincia.descripcion}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    <FormControl variant="outlined" fullWidth required disabled={cargandoCantones || this.props.disabled}>
                        <InputLabel id="lblCanton">
                            {
                                cargandoCantones ?
                                    <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                    :
                                    null
                            }
                            Cantón
                        </InputLabel>
                        <Select
                            labelId="lblCanton"
                            id="cmbCanton"
                            name="canton"
                            value={cantonSeleccionado}
                            onChange={this.handleChangeCmb}
                        >
                            {cantones.map((canton, i) =>
                                <MenuItem key={i} value={canton.codigo}>{canton.descripcion}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    <FormControl variant="outlined" fullWidth required disabled={cargandoParroquias || this.props.disabled}>
                        <InputLabel id="lblParroquia">
                            {
                                cargandoParroquias ?
                                    <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                                    :
                                    null
                            }
                            Parroquia
                        </InputLabel>
                        <Select
                            labelId="lblParroquia"
                            id="cmbParroquia"
                            name="parroquia"
                            value={parroquiaSeleccionado}
                            onChange={this.handleChangeCmb}
                        >
                            {parroquias.map((parroquia, i) =>
                                <MenuItem key={i} value={parroquia.codigo}>{parroquia.descripcion}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                </Grid>

            </Fragment>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(Cascadalocalizacion));
