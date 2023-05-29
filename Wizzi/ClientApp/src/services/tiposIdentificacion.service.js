import { authHeader, config, handleError, handleResponse } from '../helpers';

export const tiposIdentificacionService = {
    getTiposIdentificacion
};

function getTiposIdentificacion() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/tiposIdentificacion`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}