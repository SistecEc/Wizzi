import {
    REQUEST_WEATHER_FORECASTS,
    RECEIVE_WEATHER_FORECASTS
} from "../constants";

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const weatherForecastsActionCreators = {
    requestWeatherForecasts: startDateIndex => async (dispatch, getState) => {
        if (startDateIndex === getState().weatherForecasts.startDateIndex) {
            // Don't issue a duplicate request (we already have or are loading the requested data)
            return;
        }

        dispatch({ type: REQUEST_WEATHER_FORECASTS, startDateIndex });

        const url = `api/WeatherForecast`;
        const response = await fetch(url);
        const forecasts = await response.json();

        dispatch({ type: RECEIVE_WEATHER_FORECASTS, startDateIndex, forecasts });
    }
};