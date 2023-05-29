import {
    campaniasConstants,
    campaniaConstants
} from "../constants";
const initialState = { isLoading: false };

export const campanias = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case campaniasConstants.GET:
            return {
                ...state,
                isLoading: true
            };

        case campaniasConstants.GET_RECIBIR:
            return {
                ...state,
                data: action.campanias,
                isLoading: false
            };

        case campaniasConstants.GET_FALLIDO:
            return {
                ...state,
                isLoading: false
            };

        case campaniaConstants.GET_SUBCAMPANIAS:
            return {
                ...state,
                isLoading: true
            };

        case campaniaConstants.GET_SUBCAMPANIAS_RECIBIR:
            let { data } = state;
            let campania = data.find(c => c.codigo === action.codigoCampania);
            const index = data.indexOf(campania);
            data[index].subcampanias = action.subcampanias;

            return {
                ...state,
                data
            };

        default:
            return state;
    }
};