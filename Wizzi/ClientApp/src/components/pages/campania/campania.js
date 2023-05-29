import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import {
    Typography,
    withStyles,
    Accordion,
    AccordionSummary,
    AccordionDetails,
    Grid,
    Avatar,
    Button,
    Tooltip,
} from '@material-ui/core';

import ExpandMoreIcon from '@material-ui/icons/ExpandMore';


import { notificationActions } from '../../../store/actions';

import { green, red } from '@material-ui/core/colors';
import { campaniasService } from '../../../services';
import { Loader } from '../../UI';
import { AvatarGroup } from '@material-ui/lab';
import SubCampanias from './subCampania/SubCampanias';
import FormSubCampania from './subCampania/FormSubCampania';
import * as dayjs from 'dayjs';
import { Fragment } from 'react';
import { TiposForm } from '../../../enums';

const styles = theme => ({
    presupuesto: {
        color: green[400],
    },
    btnEliminar: {
        marginLeft: theme.spacing(),
        borderColor: red[400],
        backgroundColor: red[400],
        color: 'white',
        '&:hover': {
            backgroundColor: 'white',
            color: red[500],
        },
    }
});

const initialState = {
    isExpanded: false,
    isLoading: false,
    subcampanias: [],
    mostrarAgregarSubcampania: false,
    modoFormSubCampania: TiposForm.NUEVO,
    datosPrecargadosFormSubCampania: {}
};

class Campania extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
    }

    mostrarDetalleCampania = idCampania => (event, isExpanded) => {
        this.setState({ isExpanded });
        if (isExpanded) {
            this.setState({ isLoading: true });
            campaniasService.getSubcampanias(idCampania)
                .then(
                    subcampanias => {
                        this.setState({ subcampanias: subcampanias || [] });
                    },
                    error => {
                        this.props.notifications.error(error);
                    })
                .finally(() => {
                    this.setState({ isLoading: false });
                });
        }
    }

    renderMiniaturasSubcampanias = (info, limite) => {
        const { cantidadSubCampanias, primerasSubcampanias: subCampanias } = info;

        if (cantidadSubCampanias > 0) {
            limite = Math.min(subCampanias.length, limite);
            const sobrante = Math.max(cantidadSubCampanias - limite, 0);
            return (
                <AvatarGroup>
                    {
                        subCampanias.slice(0, limite).map((subCampania, i) => (
                            <Tooltip key={i} title={subCampania.descripcion}>
                                <Avatar alt={subCampania.descripcion} src={`Imagenes/${subCampania.imagen}`} />
                            </Tooltip>

                        ))
                    }
                    {
                        sobrante > 0 ?
                            <Tooltip key={limite + 1} title={`+ ${sobrante} campaña(s)`}>
                                <Avatar>{`+${sobrante}`}</Avatar>
                            </Tooltip>
                            :
                            null
                    }
                </AvatarGroup>
            )


        } else {
            return null
        }
    }

    onEditClick = (e) => {
        e.stopPropagation();
        const { codigo, titulo, descripcion, presupuesto, fechaInicio, fechaFin } = this.props.info;
        this.props.editClick({ codigo, titulo, descripcion, presupuesto, fechaInicio, fechaFin });
    }

    onDeleteClick = (e) => {
        e.stopPropagation();
        const { codigo } = this.props.info;
        this.props.deleteClick(codigo);
    }

    mostrarFormAgregarSubCampania = () => {
        this.setState({
            mostrarAgregarSubcampania: true,
            modoFormSubCampania: TiposForm.NUEVO,
            datosPrecargadosFormSubCampania: {},
        });
    }

    mostrarEditarSubCampania = (infoSubCampania) => {
        let datosPrecargadosFormSubCampania = {
            codigo: infoSubCampania.codigo,
            descripcion: infoSubCampania.descripcion,
            imagen: '',
            URLimagen: `imagenes/${infoSubCampania.imagen}`,
            fechaInicio: dayjs(infoSubCampania.fechaInicio),
            fechaFin: dayjs(infoSubCampania.fechaFin),
        };
        this.setState({
            mostrarAgregarSubcampania: true,
            modoFormSubCampania: TiposForm.EDICION,
            datosPrecargadosFormSubCampania,
        });
    }

    onSubCampaniaAgregada = (infoSubCampania) => {
        let { subcampanias } = this.state;
        subcampanias.splice(0, 0, infoSubCampania);
        this.setState({ mostrarAgregarSubcampania: false, subcampanias });
    }

    onSubCampaniaEditada = (infoSubCampania) => {
        let { subcampanias } = this.state;
        let subCampania = subcampanias.find(subCampania => subCampania.codigo === infoSubCampania.codigo);
        const index = subcampanias.indexOf(subCampania);
        subcampanias[index] = { ...infoSubCampania };
        this.setState({ mostrarAgregarSubcampania: false, subcampanias });
    }

    borrarSubCampania = (infoSubCampania) => {
        campaniasService.deleteSubCampania(infoSubCampania.codigo)
            .then(
                resultado => {
                    const subCampaniasCargadas = this.state.subcampanias.filter(subCampania => subCampania.codigo !== infoSubCampania.codigo);
                    this.setState({ subcampanias: subCampaniasCargadas });
                },
                error => {
                    this.props.notifications.error(error);
                })
            .finally(() => {
                this.setState({ isLoading: false });
            });
    }

    render() {
        const { classes, info } = this.props;
        return (
            <Accordion key={info.codigo} onChange={this.mostrarDetalleCampania(info.codigo)}>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls={`panel${info.codigo}-content`}
                    id={`panel${info.codigo}-header`}
                >
                    <Grid container spacing={1}>
                        <Grid container item xs>
                            <Grid item xs={12}>
                                <Typography variant="subtitle2">
                                    {info.titulo}
                                </Typography>
                            </Grid>
                            <Grid item xs={12}>
                                <Typography variant="caption">
                                    {info.descripcion}
                                </Typography>
                            </Grid>
                        </Grid>
                        <Grid container item xs justify="center" alignContent="center">
                            {
                                this.state.isExpanded ?
                                    <Fragment>
                                        <Button variant="outlined" color="secondary" onClick={this.onEditClick} aria-label="Editar campaña">
                                            Editar
                                        </Button>
                                        <Button variant="outlined" className={classes.btnEliminar} onClick={this.onDeleteClick} aria-label="Eliminar campaña">
                                            Eliminar
                                        </Button>
                                    </Fragment>
                                    :
                                    this.renderMiniaturasSubcampanias(info, 2)
                            }
                        </Grid>
                        <Grid container item xs={2} sm={1} alignItems="center" justify="flex-end">
                            <Typography variant="subtitle2" className={classes.presupuesto}>{`$${info.presupuesto}`}</Typography>
                        </Grid>
                    </Grid>
                </AccordionSummary>
                <AccordionDetails>
                    {this.state.isLoading ?
                        <Loader />
                        :
                        this.state.mostrarAgregarSubcampania ?
                            <FormSubCampania
                                datosPrecargados={this.state.datosPrecargadosFormSubCampania}
                                codigoCampania={info.codigo}
                                modo={this.state.modoFormSubCampania}
                                onAgregado={this.onSubCampaniaAgregada}
                                onActualizado={this.onSubCampaniaEditada}
                            />
                            :
                            <SubCampanias
                                infoSubCampanias={this.state.subcampanias}
                                onAgregarClick={this.mostrarFormAgregarSubCampania}
                                onEditarClick={this.mostrarEditarSubCampania}
                                onDeleteClick={this.borrarSubCampania}
                            />
                    }
                </AccordionDetails>
            </Accordion>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notifications: bindActionCreators({ ...notificationActions }, dispatch)
    };
};

export default connect(null, mapDispatchToProps)(withStyles(styles)(Campania));
