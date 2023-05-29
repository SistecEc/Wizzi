import React, { Component } from 'react';

import {
    Typography,
    Container,
    withStyles,
    Grid,
    Button,
} from '@material-ui/core';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';

import { ReactComponent as Banner } from './community.svg'
import Campania from './campania'

import { campaniasService } from '../../../services';
import { notificationActions } from '../../../store/actions';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { FabButton, Loader } from '../../UI'
import { Fragment } from 'react';
import DialogFormCampania from './DialogFormCampania';

import * as dayjs from 'dayjs';
import { TiposForm } from '../../../enums';

const styles = theme => ({
    banner: {
        maxHeight: '25vh',
    },
    titulo: {
        textAlign: 'center',
        marginBottom: theme.spacing(3),
    },
    fab: {
        position: 'fixed',
        bottom: theme.spacing(2),
        right: theme.spacing(2),
    },
    btnCargarMas: {
        marginTop: theme.spacing(),
    }
});

class Campanias extends Component {
    constructor(props) {
        super(props);
        this.state = {
            infoPaginado: {},
            campanias: [],
            isLoading: false,
            popUpCampaniaAbierto: false,
            modoPopupCampania: TiposForm.NUEVO,
            datosPrecargadosPopupCampania: {}
        }
    }

    componentDidMount() {
        this.cargarCampanias(1);
    }

    cargarCampanias = (pagina) => {
        this.setState({ isLoading: true });
        campaniasService.getCampanias(pagina)
            .then(
                resultados => {
                    const { resultados: SiguientesCampanias, ...infoPaginado } = resultados;
                    this.setState({
                        infoPaginado,
                        campanias: [...this.state.campanias, ...SiguientesCampanias]
                    });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    }

    CargarSiguientePagina = () => {
        this.cargarCampanias(this.state.infoPaginado.paginaActual + 1);
    }

    abrirPopupNuevaCampania = () => {
        this.setState({
            popUpCampaniaAbierto: true,
            modoPopupCampania: TiposForm.NUEVO,
            datosPrecargadosPopupCampania: {}
        });
    }

    popupCampaniaCerrado = () => {
        this.setState({ popUpCampaniaAbierto: false });
    }

    popupCampaniaCancelado = () => {
        this.setState({ popUpCampaniaAbierto: false });
    };

    campaniaAgregada = (infoCampania) => {
        let { campanias } = this.state;
        campanias.splice(0, 0, infoCampania);
        this.setState({ campanias, popUpCampaniaAbierto: false });
    };

    campaniaActualizada = (infoCampania) => {
        let { campanias } = this.state;
        let campania = campanias.find(campania => campania.codigo === infoCampania.codigo);
        const index = campanias.indexOf(campania);
        campanias[index] = { ...infoCampania };
        this.setState({ popUpCampaniaAbierto: false, campanias });
    };


    renderBotonCargarMas = (infoPaginado, classes) => {
        if (infoPaginado) {
            if (infoPaginado.paginaActual !== infoPaginado.cantidadPaginas) {
                return (
                    <Grid container justify="center" className={classes.btnCargarMas}>
                        <Button
                            variant="text"
                            color="primary"
                            endIcon={<ExpandMoreIcon />}
                            onClick={this.CargarSiguientePagina}
                        >
                            Cargar más
                        </Button>
                    </Grid>
                );
            }
        }
    }

    abrirPopupEditarCampania = (info) => {
        info.fechaInicio = dayjs(info.fechaInicio);
        info.fechaFin = dayjs(info.fechaFin);
        this.setState({
            popUpCampaniaAbierto: true,
            datosPrecargadosPopupCampania: info,
            modoPopupCampania: TiposForm.EDICION
        });
    }

    deleteCampania = (codigoCampania) => {
        this.setState({ isLoading: true });
        campaniasService.deleteCampania(codigoCampania)
            .then(
                resultados => {
                    this.props.notifications.success("Se ha eliminado la campaña correctamente");
                    const campaniasCargadas = this.state.campanias.filter(campania => campania.codigo !== codigoCampania);
                    this.setState({ campanias: campaniasCargadas });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    }

    render() {
        const { classes } = this.props;
        const { isLoading, infoPaginado, campanias } = this.state;
        return (
            <Fragment>
                <Container maxWidth="lg">
                    <Banner className={classes.banner} />
                    <Typography variant="h2" className={classes.titulo}>Campañas</Typography>
                    {
                        campanias?.map((camp, i) => (
                            <Campania key={camp.codigo} info={camp} editClick={this.abrirPopupEditarCampania} deleteClick={this.deleteCampania} />
                        ))
                    }
                    {isLoading ? <Loader /> : null}

                    {
                        this.renderBotonCargarMas(infoPaginado, classes)
                    }
                </Container>
                <DialogFormCampania
                    open={this.state.popUpCampaniaAbierto}
                    onClose={this.popupCampaniaCerrado}
                    onCancelClick={this.popupCampaniaCancelado}
                    onCampaniaAgregada={this.campaniaAgregada}
                    onCampaniaActualizada={this.campaniaActualizada}
                    modo={this.state.modoPopupCampania}
                    datosPrecargados={this.state.datosPrecargadosPopupCampania}
                />
                <FabButton className={classes.fab} onClick={this.abrirPopupNuevaCampania} />
            </Fragment>

        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(Campanias));
