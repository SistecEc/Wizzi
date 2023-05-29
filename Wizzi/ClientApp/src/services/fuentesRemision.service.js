import { authHeader, config, handleError, handleResponse } from '../helpers';

export const fuentesRemisionService = {
    getFuentesRemision
};

function getFuentesRemision() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/FuentesRemision`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}