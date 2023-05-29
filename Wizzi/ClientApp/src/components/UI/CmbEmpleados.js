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
import { sucursalesService, empleadosService } from "../../services";
import { checkEmptyObject } from "../../helpers/utils";

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

class CmbEmpleados extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        const { valorInicialSeleccionado, ciudadFiltrar, sucursalFiltrar, soloPuedeAgendar } = this.props;
        const filtro = {
            ciudadFiltrar,
            sucursalFiltrar,
            soloPuedeAgendar,
        }
        this.cargarEmpleados(valorInicialSeleccionado, filtro)
    }

    componentDidUpdate(prevProps) {
        let actualizarEstado = false;
        if (this.props.valorSeleccionado !== prevProps.valorSeleccionado ||
            this.props.ciudadFiltrar !== prevProps.ciudadFiltrar ||
            this.props.sucursalFiltrar !== prevProps.sucursalFiltrar ||
            this.props.soloPuedeAgendar !== prevProps.soloPuedeAgendar) {
            actualizarEstado = true;
        }
        if (actualizarEstado) {
            const filtro = {
                ciudad: this.props.ciudadFiltrar,
                sucursal: this.props.sucursalFiltrar,
                soloPuedeAgendar: this.props.soloPuedeAgendar,
            }
            this.cargarEmpleados(this.props.valorSeleccionado, filtro);
        }
    }

    cargarEmpleados = (valorSeleccionado, filtro) => {
        this.setState({ isLoading: true })
        empleadosService.getEmpleados({ soloRol: true, paginado: false, ...filtro })
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
                <InputLabel id="lblEmpleado">
                    {
                        isLoading ?
                            <CircularProgress size={14} color="primary" className={classes.circularProgress} />
                            :
                            null
                    }
                    Empleado
                </InputLabel>
                <Select
                    labelId="lblEmpleado"
                    id="cmbEmpleado"
                    name="empleado"
                    value={valorSeleccionado}
                    onChange={this.seleccionCambiada}
                >
                    {
                        incluirItemTodas
                            ? <MenuItem value={''}>TODAS</MenuItem>
                            : null
                    }
                    {
                        data.map((empleado, i) =>
                            <MenuItem key={i} value={empleado.codigo}>{`${empleado.apellido} ${empleado.nombre}`}</MenuItem>
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

export default connect(null, mapDispatchToProps)(withStyles(styles)(CmbEmpleados));
