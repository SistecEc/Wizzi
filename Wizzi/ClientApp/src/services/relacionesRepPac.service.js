import { config, handleError, handleResponse } from '../helpers';

export const relacionesRepPacService = {
    getRelaciones,
};

function getRelaciones() {
    const requestOptions = {
        method: 'GET',
    };

    return fetch(`${config.apiUrl}/RelacionesRepPac`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}