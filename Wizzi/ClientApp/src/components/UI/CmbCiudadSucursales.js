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

class CmbCiudadSucursales extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        const { valorInicialSeleccionado: valorSeleccionado } = this.props;
        this.setState({ isLoading: true });
        let filtro = {};
        if (this.props.soloParaAgendar) {
            filtro.soloParaAgendar = true;
        };
        sucursalesService.getCantonesSucursales(filtro)
            .then(
                data => {
                    this.setState({ data, valorSeleccionado });
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
                <InputLabel id="lblCiudad">
                    {
                        isLoading ?
                            <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                            :
                            null
                    }
                    Ciudad
                </InputLabel>
                <Select
                    labelId="lblCiudad"
                    id="cmbCiudad"
                    name="ciudad"
                    value={valorSeleccionado}
                    onChange={this.seleccionCambiada}
                >
                    {
                        incluirItemTodas
                            ? <MenuItem value={''}>TODAS</MenuItem>
                            : null
                    }
                    {
                        data.map((ciudad, i) =>
                            <MenuItem key={i} value={ciudad.codigo}>{ciudad.descripcion}</MenuItem>
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

export default connect(null, mapDispatchToProps)(withStyles(styles)(CmbCiudadSucursales));
