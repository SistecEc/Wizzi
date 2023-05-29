import React from 'react';

import {
    Typography,
    CardActionArea,
    CardContent,
    makeStyles,
} from '@material-ui/core';

import { AddCircle } from '@material-ui/icons';

import { SubCampaniaContainer } from './SubCampaniaContainer';

const useStyles = makeStyles(theme => ({
    root: {
        height: '100%',
    },
    content: {
        textAlign: 'center',
    },
    icono: {
        marginBottom: theme.spacing(2),
    }
}));

export const AgregarSubCampania = (props) => {
    const classes = useStyles();
    return (
        <SubCampaniaContainer>
            <CardActionArea className={classes.root} onClick={props.onClick}>
                <CardContent className={classes.content}>
                    <AddCircle fontSize="large" color="primary" className={classes.icono}/>
                    <Typography variant="subtitle2" color="primary">
                        Agregar subcampaña
                    </Typography>
                </CardContent>
            </CardActionArea>
        </SubCampaniaContainer>
    );
}
