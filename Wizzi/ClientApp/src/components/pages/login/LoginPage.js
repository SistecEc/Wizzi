import React from 'react';
import { connect } from 'react-redux';

import { Paper, withStyles, Grid, TextField, Button, Typography, CircularProgress, Grow } from '@material-ui/core';

import { userActions } from '../../../store/actions';
import { Fragment } from 'react';

const styles = theme => ({
    circularInsideButton: {
        marginRight: theme.spacing(),
    },
    margin: {
        margin: theme.spacing(2),
    },
    //container: {
    //    '&:before, &:after': {
    //        content: '""',
    //        display: 'block',
    //        'box-flex': 1,
    //        'flex-grow': 1,
    //        height: '24px'
    //    }
    //},
    loginForm: {
        padding: theme.spacing(),
        margin: '0 auto',
        width: 400
    }
});

class LoginPage extends React.Component {
    constructor(props) {
        super(props);

        // reset login status
        this.props.dispatch(userActions.logout());

        this.state = {
            montado: false,
            username: '',
            password: ''
        };
    }

    componentDidMount() {
        this.setState({ montado: true });
    }

    handleChange = (e) => {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    handleSubmit = (e) => {
        e.preventDefault();

        const { username, password } = this.state;
        const { dispatch } = this.props;
        if (username && password) {
            dispatch(userActions.login(username, password));
        }
    }

    render() {
        const { classes, loggingIn } = this.props;
        const { username, password, montado } = this.state;
        return (
            <div className={classes.container}>
                <Grow in={montado}>
                    <Paper className={classes.loginForm}>
                        <form name="form" className={classes.margin} onSubmit={this.handleSubmit}>
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
                                        onChange={this.handleChange}
                                        className={"noUpperCase"}
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
                                        onChange={this.handleChange}
                                        className={"noUpperCase"}
                                        type="password"
                                        fullWidth
                                        required
                                    />
                                </Grid>
                                <Grid container justify="center">
                                    <Button variant="contained" color="primary" type="submit" disabled={loggingIn}>
                                        {loggingIn ?
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
                        </form>
                    </Paper>
                </Grow>
            </div>
        );
    }
}

function mapStateToProps(state) {
    const { loggingIn } = state.authentication;
    return {
        loggingIn
    };
}

const connectedLoginPage = connect(mapStateToProps)(withStyles(styles)(LoginPage));
export { connectedLoginPage as LoginPage };