import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";

import { agenda } from './agenda.reducer';
import { authentication } from './authentication.reducer';
import { campanias } from "./campanias.reducer";
import { ciudades } from "./ciudades.reducer";
import { notification } from './notification.reducer';
import { registration } from './registration.reducer';
import { solicitudesCitas } from "./solicitudCita.reducer";
import { subCampania } from "./subCampania.reducer";
import { theme } from "./theme.reducer";
import { users } from "./users.reducer";
import { weatherForecasts } from "./weatherForecasts.reducer";

import { history } from "../../helpers";

const reducers = {
    agenda,
    authentication,
    campanias,
    ciudades,
    notification,
    registration,
    solicitudesCitas,
    subCampania,
    theme,
    users,
    weatherForecasts
};

const rootReducer = combineReducers({
    ...reducers,
    router: connectRouter(history)
});

export default rootReducer;