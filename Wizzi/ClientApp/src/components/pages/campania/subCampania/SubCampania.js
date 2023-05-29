import React, { Component } from 'react';

import {
    Typography,
    withStyles,
    Grid,
    CardMedia,
    CardContent,
    Fab,
} from '@material-ui/core';

import { DateRange, Dialpad, Edit, Delete } from '@material-ui/icons/';
import { red } from '@material-ui/core/colors';

import * as dayjs from 'dayjs';
import { SubCampaniaContainer } from './SubCampaniaContainer';

const styles = theme => ({
    container: {
        position: 'relative',
        '&:hover': {
            '& $buttonContainer': {
                display: 'flex',
            },
        },
    },
    buttonContainer: {
        top: theme.spacing(),
        right: theme.spacing(),
        display: 'none',
        position: 'absolute',
        '& button': {
            marginRight: theme.spacing(),
        },
    },
    textoIcono: {
        margin: 'auto',
    },
    btnEliminar: {
        backgroundColor: red[500],
        color: 'white',
        '&:hover': {
            backgroundColor: 'white',
            color: red[500],
        },
    }
});

class SubCampania extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
    }

    convertirFecha = (fecha) => {
        return dayjs(fecha).format("DD MMM");
    }

    editarClick = () => {
        this.props.onEditar(this.props.infoSubcampania);
    }

    deleteClick = () => {
        this.props.onDelete(this.props.infoSubcampania);
    }

    render() {
        const { classes, infoSubcampania } = this.props;
        return (
            <SubCampaniaContainer cardClass={classes.container}>
                <CardMedia
                    component="img"
                    alt={`campaña ${infoSubcampania.codigo}`}
                    height="140"
                    image={`Imagenes/${infoSubcampania.imagen}`}
                    title={infoSubcampania.descripcion}
                />
                <CardContent>
                    <Grid container spacing={2}>
                        <Grid item container alignContent="center" xs={12}>
                            <Typography variant="body2" color="textSecondary">
                                <DateRange fontSize="small" />
                            </Typography>
                            <Typography variant="body2" color="textSecondary" className={classes.textoIcono}>
                                {`${this.convertirFecha(infoSubcampania.fechaInicio)} - ${this.convertirFecha(infoSubcampania.fechaFin)}`}
                            </Typography>
                        </Grid>
                        <Grid item container alignContent="center" xs={12}>
                            <Typography variant="body2" color="textSecondary">
                                <Dialpad fontSize="small" />
                            </Typography>
                            <Typography variant="body2" color="textSecondary" className={classes.textoIcono}>
                                {infoSubcampania.codigo}
                            </Typography>
                        </Grid>
                    </Grid>
                    <Typography gutterBottom variant="subtitle1">
                        {infoSubcampania.descripcion}
                    </Typography>
                    <div className={classes.buttonContainer}>
                        <Fab size="small" color="primary" aria-label="edit" onClick={this.editarClick}>
                            <Edit />
                        </Fab>
                        <Fab size="small" className={classes.btnEliminar} aria-label="delete" onClick={this.deleteClick}>
                            <Delete />
                        </Fab>
                    </div>
                </CardContent>
            </SubCampaniaContainer>
        );
    }
}

export default withStyles(styles)(SubCampania);
