import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';

import { notificationActions } from '../../store/actions';
import { Grow } from '@material-ui/core';

function Alert(props) {
    return <MuiAlert elevation={6} variant="filled" {...props} />;
}

function ReduxSnackbar(props) {
    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        props.clear();
    };

    return (
        <Snackbar
            open={props.visible}
            onClose={handleClose}
            autoHideDuration={6000}
            TransitionComponent={Grow}
        >
            <Alert onClose={handleClose} severity={props.severity}>
                {props.message}
            </Alert>
        </Snackbar>
    );
}

//ReduxSnackbar.defaultProps = {
//    snackbar: {}
//};

//ReduxSnackbar.propTypes = {
//    snackbar: PropTypes.object,
//    visible: PropTypes.bool.isRequired,
//    severity: PropTypes.string.isRequired,
//    message: PropTypes.string.isRequired,
//    handleSnackbarClose: PropTypes.func.isRequired
//};

const mapStateToProps = state => {
    return {
        visible: state.notification.visible,
        severity: state.notification.severity,
        message: state.notification.message
    };
};

const mapDispatchToProps = dispatch => {
    //Destructuramos clear del array de notificationActions
    const { clear } = notificationActions;
    //Hacemos el bind de clear como objeto {clave: valor}, al no pasarle el nombre de la clave asume que tiene el mismo nombre que la variable
    return bindActionCreators({ clear }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(ReduxSnackbar);