import React from "react";
import { Container, makeStyles } from "@material-ui/core";
import { ReactComponent as NotFoundBanner } from './404.svg'

const useStyles = makeStyles({
    container: {
        margin: 'auto',
        textAlign: 'center',
    },
    root: {
        maxHeight: '60vh',
        maxWidth: '70vw',
    },
});

export const NotFound = (props) => {
    const classes = useStyles();
    return (
        <Container maxWidth="md" className={classes.container}>
            <NotFoundBanner className={classes.root} />
        </Container>
    );
}