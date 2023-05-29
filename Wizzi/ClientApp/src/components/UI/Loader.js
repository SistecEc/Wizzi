import React from 'react';
import { CircularProgress, Grid } from '@material-ui/core';

export const Loader = (props) => {
    return (
        <Grid container justify="center">
            <CircularProgress color="primary" {...props}/>
        </Grid>
    );
}