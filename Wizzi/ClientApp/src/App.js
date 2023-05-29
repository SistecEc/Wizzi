import React, { Component } from "react";

import { history } from './helpers';
import { notificationActions } from './store/actions';
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

//MATERIAL
import CssBaseline from "@material-ui/core/CssBaseline";

//UI
import ThemeProvider from './components/UI/ThemeProvider';
import ReduxSnackbar from './components/UI/ReduxSnackBar';

//router
import { Router } from "./components/router"

import * as dayjs from 'dayjs';
import 'dayjs/locale/es';

dayjs.locale('es');

class App extends Component {
    constructor(props) {
        super(props);

        const { notificationActions } = this.props;
        history.listen((location, action) => {
            // clear alert on location change
            notificationActions.clear();
        });
    }

    render() {
        return (
            <ThemeProvider>
                <CssBaseline />
                <ReduxSnackbar />
                <Router />
            </ThemeProvider>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        notificationActions: bindActionCreators(notificationActions, dispatch)
    };
};

export default connect(
    null,
    mapDispatchToProps
)(App);