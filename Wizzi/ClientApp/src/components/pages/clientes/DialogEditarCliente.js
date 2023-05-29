import { DialogContent, DialogTitle, withStyles } from '@material-ui/core';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { TiposForm } from '../../../enums';
import { clientesService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { Loader, ResponsiveDialog } from '../../UI';
import FormCliente from './FormCliente';

const initialState = {
    cargando: false,
    cargandoClienteAligar: false,
    buscarPaciente: true,
    registrarPaciente: false,
    datosCliente: {},
    codigoSolicitud: '',
};

const styles = theme => ({
    root: {
        width: '100%',
    },
    circularInsideButton: {
        marginRight: theme.spacing(),
    },
});

class DialogEditarCliente extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
    }

    componentDidUpdate(prevProps) {
        const { open: openAntiguo } = prevProps;
        const { open, codigoClienteEditar } = this.props;
        if (open && open !== openAntiguo) {
            this.cargarCliente(codigoClienteEditar);
        }
    }

    cargarCliente = (codigoCliente) => {
        this.setState({ cargando: true });
        clientesService.getCliente(codigoCliente)
            .then(
                datosCliente => {
                    this.setState({ datosCliente });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ cargando: false });
            });
    }

    cambioModoFormRegistroPaciente = (modo) => {
        switch (modo) {
            case TiposForm.EDICION:
                break;

            case TiposForm.VISUALIZAR:
                break;

            default:
        }
    }

    pacienteEditado = (datosCliente) => {
        this.setState({
            registrarPaciente: false,
            buscarPaciente: false,
            datosCliente,
        });
        this.props.onClienteActualizado(datosCliente)
    }

    pacienteRegistrado = (datosCliente) => {
        this.setState({
            registrarPaciente: false,
            buscarPaciente: false,
            datosCliente,
        });
    }

    render() {
        const { cargando, datosCliente } = this.state;

        return (
            <ResponsiveDialog
                aria-labelledby="tituloDialogEditarCliente"
                open={this.props.open}
                onClose={this.props.onClose}
                fullWidth={true}
                maxWidth="lg"
            >
                <DialogTitle id="tituloDialogEditarCliente">
                    Editar cliente
                </DialogTitle>
                <DialogContent>
                    {
                        cargando ?
                            <Loader />
                            :
                            <FormCliente
                                modoForm={this.state.registrarPaciente ? TiposForm.NUEVO : TiposForm.VISUALIZAR}
                                datosPrecargados={datosCliente}
                                onCambioModoForm={this.cambioModoFormRegistroPaciente}
                                onClienteEditado={this.pacienteEditado}
                                onClienteRegistrado={this.pacienteRegistrado} />
                    }

                </DialogContent>
            </ResponsiveDialog>
        );
    }

}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(DialogEditarCliente));
