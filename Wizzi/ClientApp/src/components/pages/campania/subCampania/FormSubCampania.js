import React, { Component, Fragment } from 'react';

import {
    withStyles,
    Grid,
    TextField,
    Button,
    CircularProgress,
    Fab,
    IconButton,
} from '@material-ui/core';

import * as dayjs from 'dayjs';
import { DateTimePicker } from '@material-ui/pickers';
import { notificationActions } from '../../../../store/actions';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Edit, AddAPhoto } from '@material-ui/icons/';
import { campaniasService } from '../../../../services';
import { TiposForm } from '../../../../enums';

const styles = theme => ({
    form: {
        margin: 'auto',
    },
    contenedorImgPrev: {
        height: 150,
        maxWidth: '100%',
        position: 'relative'
    },
    imgPrev: {
        maxHeight: '100%',
        maxWidth: '100%',
    },
    circularProgress: {
        marginRight: theme.spacing(),
    },
    btnCambiarImg: {
        bottom: theme.spacing(),
        right: theme.spacing(),
        position: 'absolute'
    },
    iconoCambiarImg: {
        marginRight: theme.spacing(1),
    },
    inputUpload: {
        display: 'none',
    },
});

const initialState = {
    codigoCampania: '',
    descripcion: '',
    imagen: '',
    URLimagen: '',
    fechaInicio: dayjs(new Date()),
    fechaFin: dayjs(new Date()),
    isLoading: false,
}

class FormSubCampania extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        const { codigoCampania, datosPrecargados } = this.props;
        this.setState({ codigoCampania, ...datosPrecargados });
    }

    onChangeTxt = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    onChangeTxtDate = (txt) => (date) => {
        if (txt === 'FI') {
            this.setState({ fechaInicio: date });
        } else {
            this.setState({ fechaFin: date });
        }
    }

    onImgSubida = (e) => {
        let imagen = e.target.files[0];
        let URLimagen = URL.createObjectURL(imagen);
        this.setState({
            imagen,
            URLimagen
        });
    }

    errorEnCalendario = (error, valor) => {
        if (error) {
            this.props.notifications.error(`Error: ${error} - ${valor}`);
        }
    }

    handleSubCampaniaSubmit = (e) => {
        e.preventDefault();
        this.setState({ isLoading: true });
        const { _isLoading, ...infoSubCampania } = this.state;
        infoSubCampania.fechaInicio = infoSubCampania.fechaInicio.toISOString();
        infoSubCampania.fechaFin = infoSubCampania.fechaFin.toISOString();

        if (this.props.modo === TiposForm.NUEVO) {
            campaniasService.grabarSubCampania(infoSubCampania)
                .then(
                    infoSubCampaniaGrabada => {
                        this.props.notifications.success('Subcampaña grabada correctamente.');
                        this.props.onAgregado(infoSubCampaniaGrabada);
                        //URL.revokeObjectURL(this.state.URLimagen); //borra la imágen del cache
                    },
                    error => {
                        this.props.notifications.error(error);
                    })
                .finally(() => {
                    this.setState({ isLoading: false });
                });
        } else {
            campaniasService.actualizarSubCampania(infoSubCampania)
                .then(
                    infoSubCampaniaActualizada => {
                        this.props.notifications.success('Subcampaña actualizada correctamente.');
                        this.props.onActualizado(infoSubCampaniaActualizada);
                        //URL.revokeObjectURL(this.state.URLimagen); //borra la imágen del cache
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
        const { classes } = this.props;
        return (
            <form className={classes.form} onSubmit={this.handleSubCampaniaSubmit}>
                <Grid container spacing={2}>
                    <Grid container item xs={12} justify="center">
                        <input accept="image/*" className={classes.inputUpload} id="icon-button-file" type="file" onChange={this.onImgSubida} />
                        {
                            this.state.URLimagen === '' ?
                                <label htmlFor="icon-button-file">
                                    <IconButton fontSize="large" color="primary" aria-label="subir imágen" component="span">
                                        <AddAPhoto />
                                    </IconButton>
                                </label>
                                :
                                <div className={classes.contenedorImgPrev}>
                                    <img alt="Imágen subida" src={this.state.URLimagen} className={classes.imgPrev} />
                                    <Fab
                                        variant="extended"
                                        size="small"
                                        color="secondary"
                                        aria-label="editar"
                                        className={classes.btnCambiarImg}
                                        component="label"
                                        htmlFor="icon-button-file"
                                    >
                                        <Edit className={classes.iconoCambiarImg} />
                                        Cambiar
                                    </Fab>
                                </div>
                        }
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
                    <Button
                        variant="contained"
                        color="primary"
                        type="submit"
                        fullWidth
                        disabled={this.state.isLoading}
                    >
                        {this.state.isLoading ?
                            <Fragment>
                                <CircularProgress size={14} color="primary" className={classes.circularProgress} />
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
                </Grid>
            </form>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(FormSubCampania));
