import {
    REQUEST_WEATHER_FORECASTS,
    RECEIVE_WEATHER_FORECASTS
} from "../constants";
const initialState = { forecasts: [], isLoading: false };

export const weatherForecasts = (state, action) => {
    state = state || initialState;

    if (action.type === REQUEST_WEATHER_FORECASTS) {
        return {
            ...state,
            startDateIndex: action.startDateIndex,
            isLoading: true
        };
    }

    if (action.type === RECEIVE_WEATHER_FORECASTS) {
        return {
            ...state,
            startDateIndex: action.startDateIndex,
            forecasts: action.forecasts,
            isLoading: false
        };
    }

    return state;
};