// REACT
import React, { Fragment, Component } from "react";
import queryString from 'query-string'

//ESTILOS
import { Paper, Grid, withStyles } from "@material-ui/core";

import { campaniasService } from "../../../services";
import FormSolicitudCita from "./formSolicitudCita";
import { CitaSolicitada } from "./";


const styles = theme => ({
    root: {
        height: '100vh',
    },
    image: {
        backgroundRepeat: 'no-repeat',
        backgroundColor: theme.palette.type === 'dark' ? theme.palette.grey[900] : theme.palette.grey[50],
        backgroundSize: '100%',
        backgroundPosition: 'center',
        [theme.breakpoints.down("xs")]: {
            height: '30vh',
            margin: 'auto'
        },
    },
    paper: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        margin: theme.spacing(8, 4),
        [theme.breakpoints.down("xs")]: {
            margin: theme.spacing(2, 4),
        },
    },
});

const initialState = {
    codigoSubCampania: '',
    img: '',
    citaSolicitada: false,
};

class SolicitarCita extends Component {
    constructor(props) {
        super(props);
        this.state = initialState;
    }

    componentDidMount() {
        const { cid } = queryString.parse(this.props.location.search);
        if (cid) {
            this.setState({ codigoSubCampania: cid });
            campaniasService.getSubcampaniaImg(cid)
                .then(
                    img => {
                        this.setState({ img });
                    },
                    error => {
                    });
        }
    }

    subCampaniaSolicitada = () => {
        this.setState({ citaSolicitada: true });
    }

    render() {
        const { classes } = this.props;
        return (
            <Fragment>
                <Grid container component="main" className={classes.root}>
                    <Grid item xs={12} sm={4} md={7} className={classes.image} style={{ backgroundImage: `url(/imagenes/${this.state.img})` }} />
                    <Grid item container xs={12} sm={8} md={5} component={Paper} elevation={6} square alignItems="center" justify="center">
                        <div className={classes.paper}>
                            {
                                this.state.citaSolicitada ?
                                    <CitaSolicitada />
                                    :
                                    <FormSolicitudCita subcampania={this.state.codigoSubCampania} onSubcampaniaGrabada={this.subCampaniaSolicitada}/>
                            }
                        </div>
                    </Grid>
                </Grid>
            </Fragment>
        );
    }
}

export default withStyles(styles)(SolicitarCita);