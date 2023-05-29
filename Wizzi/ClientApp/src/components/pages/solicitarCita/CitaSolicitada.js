import React, { Fragment } from "react";
import { ReactComponent as CitaSolicitadaSVG } from './citaSolicitada2.svg'
import { Typography, makeStyles } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    root: {
        width: '60%',
    },
    texto: {
        marginTop: theme.spacing(5),
        width: '100%',
        textAlign: 'center',
    }
}));

export const CitaSolicitada = (props) => {
    const classes = useStyles();
    return (
        <Fragment>
            <CitaSolicitadaSVG className={classes.root} />
            <Typography variant="h4" className={classes.texto}>
                Cita solicitada correctamente
            </Typography>
        </Fragment>
    );
}