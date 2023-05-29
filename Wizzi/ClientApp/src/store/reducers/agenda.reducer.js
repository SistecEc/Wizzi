import {
    REQUEST_CITAS_MEDICAS,
    RECEIVE_CITAS_MEDICAS
} from "../constants";
const initialState = { citasMedicas: [], isLoading: false };

export const agenda = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case REQUEST_CITAS_MEDICAS:
            return {
                ...state,
                startDateIndex: action.startDateIndex,
                isLoading: true
            };

        case RECEIVE_CITAS_MEDICAS:
            return {
                ...state,
                startDateIndex: action.startDateIndex,
                citasMedicas: action.citasMedicas,
                isLoading: false
            };

        default:
            return state;
    }
};