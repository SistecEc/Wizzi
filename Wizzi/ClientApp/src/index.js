import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { history } from "./helpers";
import configureStore from "./store";
import App from "./App";
import registerServiceWorker from './registerServiceWorker';
import './index.scss';

import DayjsUtils from '@date-io/dayjs';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';

// Get the application-wide store instance, prepopulating with state from the server where available.
const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <MuiPickersUtilsProvider utils={DayjsUtils}>
                <App />
            </MuiPickersUtilsProvider>
        </ConnectedRouter>
    </Provider>,
    document.getElementById('root')
);

registerServiceWorker();