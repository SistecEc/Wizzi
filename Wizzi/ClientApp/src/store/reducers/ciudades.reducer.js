import {
    REQUEST_CIUDADES,
    RECEIVE_CIUDADES
} from "../constants";
const initialState = { ciudades: [], isLoading: false };

export const ciudades = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case REQUEST_CIUDADES:
            return {
                ...state,
                ciudades: [],
                isLoading: true
            };

        case RECEIVE_CIUDADES:
            return {
                ...state,
                ciudades: action.ciudades,
                isLoading: false
            };

        default:
            return state;
    }
};