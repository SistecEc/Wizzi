import { solicitudCitaConstants } from '../constants';
const initialState = { isLoading: false };

export const solicitudesCitas = (state, action) => {
    state = state || initialState;
    switch (action.type) {
        case solicitudCitaConstants.GRABAR_REQUEST:
        case solicitudCitaConstants.OBTENER_TODAS_REQUEST:
            return {
                isLoading: true
            };

        case solicitudCitaConstants.OBTENER_REQUEST:
            return {
                id: action.id,
                isLoading: true
            };

        case solicitudCitaConstants.GRABAR_CORRECTO:
        case solicitudCitaConstants.OBTENER_CORRECTO:
            return {
                solicitud: action.infoSolicitud,
                isLoading: false
            };

        case solicitudCitaConstants.GRABAR_FALLIDO:
        case solicitudCitaConstants.OBTENER_FALLIDO:
        case solicitudCitaConstants.OBTENER_TODAS_FALLIDO:
            return {
                error: action.error,
                isLoading: false
            };

        case solicitudCitaConstants.OBTENER_TODAS_CORRECTO:
            return {
                solicitudes: action.solicitudes,
                isLoading: false
            };


        default:
            return state
    }
}