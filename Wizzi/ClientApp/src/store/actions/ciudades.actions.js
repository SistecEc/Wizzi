import {
    REQUEST_CIUDADES,
    RECEIVE_CIUDADES
} from "../constants";

export const ciudadesActionCreators = {
    requestCiudades: () => dispatch => {
        dispatch({ type: REQUEST_CIUDADES });
        return fetch(`api/ciudades`)
            .then(response => response.json())
            .then(ciudades => {
                dispatch({ type: RECEIVE_CIUDADES, ciudades });
            })
            .catch(err=> console.log('Error: ', err));
  }
};