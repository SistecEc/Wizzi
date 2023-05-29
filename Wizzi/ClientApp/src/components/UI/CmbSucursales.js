// REACT
import React, { Component } from "react";

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
} from "@material-ui/core";

import { bindActionCreators } from "redux";
import { notificationActions } from "../../store/actions";
import { sucursalesService } from "../../services";

const styles = theme => ({
    circularProgress: {
        marginRight: theme.spacing(),
    },
});

const initialState = {
    valorSeleccionado: '',
    data: [],
    isLoading: false,
}

class CmbSucursales extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        const { valorInicialSeleccionado, ciudadFiltrar } = this.props;
        this.cargarSucursales(ciudadFiltrar, valorInicialSeleccionado)
    }

    componentDidUpdate(prevProps) {
        const { ciudadFiltrar: ciudadFiltrarAntiguo } = prevProps;
        const { ciudadFiltrar } = this.props;
        if (ciudadFiltrar !== ciudadFiltrarAntiguo) {
            this.cargarSucursales(ciudadFiltrar)
        }
    }

    cargarSucursales(ciudad, valorSeleccionado) {
        this.setState({ isLoading: true });
        let filtro = {
            ciudad,
            paginado: false
        };
        if (this.props.soloParaAgendar) {
            filtro.soloParaAgendar = true;
        };
        sucursalesService.getSucursales(filtro)
            .then(
                data => {
                    let nuevoEstado = {
                        data
                    };
                    if (valorSeleccionado) {
                        nuevoEstado.valorSeleccionado = valorSeleccionado;
                    }
                    this.setState(nuevoEstado);
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    }

    seleccionCambiada = e => {
        const { value: valorSeleccionado } = e.target;
        const { onValorSeleccionado } = this.props;
        if (onValorSeleccionado) {
            onValorSeleccionado(e);
        }
        this.setState({ valorSeleccionado });
    }

    render() {
        const { classes, incluirItemTodas, formControlProps } = this.props;
        const { valorSeleccionado, isLoading, data } = this.state;
        return (
            <FormControl disabled={isLoading} {...formControlProps}>
                <InputLabel id="lblSucursal">
                    {
                        isLoading ?
                            <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                            :
                            null
                    }
                    Sucursal
                </InputLabel>
                <Select
                    labelId="lblSucursal"
                    id="cmbSucursal"
                    name="sucursal"
                    value={valorSeleccionado}
                    onChange={this.seleccionCambiada}
                >
                    {
                        incluirItemTodas
                            ? <MenuItem value={''}>TODAS</MenuItem>
                            : null
                    }
                    {
                        data.map((sucursal, i) =>
                            <MenuItem key={i} value={sucursal.codigo}>{sucursal.nombre}</MenuItem>
                        )
                    }
                </Select>
            </FormControl>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(CmbSucursales));
