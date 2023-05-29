import React, { Component } from 'react';

import { notificationActions } from '../../../store/actions';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { campaniasService } from '../../../services';

import {
    Grid,
    TextField,
    DialogTitle,
    DialogContent,
    DialogActions,
    Button,
    CircularProgress,
} from '@material-ui/core';

import { ResponsiveDialog } from '../../UI'

import { DateTimePicker } from '@material-ui/pickers';
import * as dayjs from 'dayjs';
import { Fragment } from 'react';
import { TiposForm } from '../../../enums';


const initialState = {
    titulo: '',
    descripcion: '',
    presupuesto: 0.00,
    fechaInicio: dayjs(new Date()),
    fechaFin: dayjs(new Date()),
    isLoading: false,
}

class DialogFormCampania extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentWillReceiveProps(props) {
        if (props.datosPrecargados.codigo) {
            this.setState(props.datosPrecargados);
        } else {
            this.setState(initialState);
        }
    }

    onChangeTxt = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    onChangeTxtNumero = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: parseFloat(value) || 0 });
    }

    onChangeTxtDate = (txt) => (date) => {
        if (txt === 'FI') {
            this.setState({ fechaInicio: date });
        } else {
            this.setState({ fechaFin: date });
        }
    }

    errorEnCalendario = (error, valor) => {
        if (error) {
            this.props.notifications.error(`Error: ${error} - ${valor}`);
        }
    }

    grabarCampania = () => {
        this.setState({ isLoading: true });
        const { isLoading, ...infoCampania } = this.state;
        infoCampania.fechaInicio = infoCampania.fechaInicio.toISOString();
        infoCampania.fechaFin = infoCampania.fechaFin.toISOString();

        if (this.props.modo === TiposForm.NUEVO) {
            campaniasService.grabarCampania(infoCampania)
                .then(
                    infoCampaniaGrabada => {
                        this.props.notifications.success('Campaña grabada correctamente.');
                        this.props.onCampaniaAgregada(infoCampaniaGrabada);
                    },
                    error => {
                        this.props.notifications.error(error);
                    })
                .finally(() => {
                    this.setState({ isLoading: false });
                });
        } else {
            campaniasService.actualizarCampania(infoCampania)
                .then(
                    infoCampaniaActualizada => {
                        this.props.notifications.success('Campaña actualizada correctamente.');
                        this.props.onCampaniaActualizada(infoCampaniaActualizada);
                    },
                    error => {
                        this.props.notifications.error(error);
                    })
                .finally(() => {
                    this.setState({ isLoading: false });
                });
        }

    }

    render() {
        return (
            <ResponsiveDialog
                aria-labelledby="tituloDialogCampania"
                open={this.props.open}
                onClose={this.props.onClose}
            >
                <DialogTitle id="tituloDialogCampania">
                    {this.props.modo === TiposForm.NUEVO ?
                        `Ingresar nueva campaña`
                        :
                        `Editar campaña`
                    }
                </DialogTitle>
                <DialogContent>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                id="txtTitulo"
                                name="titulo"
                                label="Título"
                                value={this.state.titulo}
                                onChange={this.onChangeTxt}
                                autoFocus
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                id="txtDescripcion"
                                name="descripcion"
                                label="Descripción"
                                value={this.state.descripcion}
                                onChange={this.onChangeTxt}
                                multiline
                                rows="4"
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <DateTimePicker
                                inputVariant="outlined"
                                ampm={false}
                                label="Fecha de inicio"
                                minDateMessage="La fecha de inicio no puede ser menor a la actual"
                                value={this.state.fechaInicio}
                                onChange={this.onChangeTxtDate('FI')}
                                onError={this.errorEnCalendario}
                                disablePast
                                format="DD/MM/YYYY HH:mm"
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <DateTimePicker
                                inputVariant="outlined"
                                ampm={false}
                                label="Fecha de finalización"
                                minDate={this.state.fechaInicio}
                                minDateMessage="La fecha de finalización no debe ser menor a la de inicio"
                                value={this.state.fechaFin}
                                onChange={this.onChangeTxtDate('FF')}
                                onError={this.errorEnCalendario}
                                disablePast
                                format="DD/MM/YYYY HH:mm"
                                fullWidth
                            />
                        </Grid>
                        <Grid item xs={12} sm={6}>
                            <TextField
                                variant="outlined"
                                id="txtPresupuesto"
                                name="presupuesto"
                                label="Presupuesto"
                                type="number"
                                value={this.state.presupuesto}
                                onChange={this.onChangeTxtNumero}
                                fullWidth
                                required
                            />
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button autoFocus onClick={this.props.onCancelClick} color="primary">
                        Cancelar
                        </Button>
                    <Button onClick={this.grabarCampania} color="primary" autoFocus disabled={this.state.isLoading}>
                        {this.state.isLoading ?
                            <Fragment>
                                <CircularProgress size={14} color="primary" />
                                {this.props.modo === TiposForm.NUEVO ?
                                    `Grabando...`
                                    :
                                    `Actualizando...`
                                }
                            </Fragment>
                            :
                            this.props.modo === TiposForm.NUEVO ?
                                `Grabar`
                                :
                                `Actualizar`
                        }
                    </Button>
                </DialogActions>
            </ResponsiveDialog>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(DialogFormCampania);
