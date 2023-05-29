import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import { Paper, makeStyles, Grid, TextField, Button, FormControlLabel, Checkbox, Typography, CircularProgress } from '@material-ui/core';

import { userActions } from '../../../store/actions';
import { Fragment } from 'react';

const useStyles = makeStyles(theme => ({
    circularInsideButton: {
        marginRight: theme.spacing(),
    },
    margin: {
        margin: theme.spacing(2),
    },
    loginForm: {
        padding: theme.spacing(),
        margin: '0 auto',
        width: 400,
        height: 500,
        '&:before': {
            content: ' ',
            display: 'block',
            backgroundColor: '#99f',
            height: 24,
            flexGrow: 1,
        }
    }
}));

function LoginPage(props) {
    const classes = useStyles();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();

        const { dispatch } = props;
        if (username && password) {
            dispatch(userActions.login(username, password));
        }
    }

    return (
        <Fragment>
            <Paper className={classes.loginForm}>
                <form name="form" className={classes.margin} onSubmit={handleSubmit}>
                    <Grid container spacing={3} alignItems="flex-end">
                        <Grid item xs={12} style={{ textAlign: "center" }}>
                            <Typography variant="h5">Iniciar sesión</Typography>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                name="username"
                                id="username"
                                label="Username"
                                value={username}
                                onChange={(e) => { setUsername(e.target.value)}}
                                fullWidth
                                autoFocus
                                required
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                name="password"
                                id="password"
                                label="Password"
                                value={password}
                                onChange={(e) => { setPassword(e.target.value)}}
                                type="password"
                                fullWidth
                                required
                            />
                        </Grid>
                        <Grid container alignItems="center" justify="space-between">
                            <Grid item>
                                <FormControlLabel control={
                                    <Checkbox
                                        color="primary"
                                    />
                                } label="Remember me" />
                            </Grid>
                            <Grid item>
                                <Button disableFocusRipple disableRipple style={{ textTransform: "none" }} variant="text" color="primary">Forgot password ?</Button>
                            </Grid>
                        </Grid>
                        <Grid container justify="center">
                            <Button variant="contained" color="primary" type="submit" disabled={props.loggingIn}>
                                {props.loggingIn ?
                                    <Fragment>
                                        <CircularProgress size={14} color="primary" className={classes.circularInsideButton} />
                                        <Typography>Ingresando...</Typography>
                                    </Fragment>
                                    :
                                    <Typography>Ingresar</Typography>
                                }
                            </Button>
                        </Grid>
                    </Grid>
                    <Button color="secondary" component={Link} to="/register">
                        ¿No tienes una cuenta? Registrate
                        </Button>
                </form>
            </Paper>
        </Fragment>
        );
}

function mapStateToProps(state) {
    const { loggingIn } = state.authentication;
    return {
        loggingIn
    };
}

export default connect(mapStateToProps, null)(LoginPage);