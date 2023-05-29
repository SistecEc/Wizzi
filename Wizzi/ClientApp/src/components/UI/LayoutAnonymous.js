import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import ThemeToggler from "./ThemeToggler";

const useStyles = makeStyles({
    root: {
        minHeight: '100vh',
        display: "flex",
        flexDirection: "column",
        justifyContent: "center"
    }
});

export const LayoutAnonymous = (props) => {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <ThemeToggler/>
            {props.children}
        </div>
    );
}