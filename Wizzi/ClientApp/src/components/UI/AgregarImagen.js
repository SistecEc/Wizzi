import React from 'react';

import {
    CardContent,
    makeStyles,
    Card,
    IconButton,
} from '@material-ui/core';

import { AddAPhoto } from '@material-ui/icons';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
    },
    content: {
        textAlign: 'center',
    },
    input: {
        display: 'none',
    },
}));

export const AgregarImagen = (props) => {
    const classes = useStyles();
    return (
        <Card className={classes.root} variant="outlined">
            <CardContent className={classes.content}>
                <input accept="image/*" className={classes.input} id="icon-button-file" type="file" onChange={props.onFileUpload} />
                <label htmlFor="icon-button-file">
                    <IconButton fontSize="large" color="primary" aria-label="subir imágen" component="span">
                        <AddAPhoto />
                    </IconButton>
                </label>
            </CardContent>
        </Card>
    );
}
