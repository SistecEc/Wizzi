import React from 'react';

import {
    Grid,
    Card,
} from '@material-ui/core';

import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        maxWidth: 345,
        '&:hover': {
            boxShadow: theme.shadows[2],
        },
    },
}));

export const SubCampaniaContainer = (props) => {
    const classes = useStyles();
    return (
        <Grid container item xs={12} sm={6} md={4} lg={3} justify="center">
            <Card className={classes.root, props.cardClass} variant="outlined">
                {props.children}
            </Card>
        </Grid>
    )
}
