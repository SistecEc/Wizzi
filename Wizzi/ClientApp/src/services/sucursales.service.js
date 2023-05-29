import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const sucursalesService = {
    getSucursales,
    getCantonesSucursales,
};

function getSucursales({ ciudad, paginado, soloParaAgendar }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader(),
    };

    let queryparams = {
        ciudad,
        paginado,
        soloParaAgendar,
    };

    return fetch(`${config.apiUrl}/Sucursales?${queryString.stringify(queryparams)}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        }); 
}

function getCantonesSucursales({ soloParaAgendar }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader(),
    };

    let queryparams = {
        soloParaAgendar,
    };

    return fetch(`${config.apiUrl}/Sucursales/cantones?${queryString.stringify(queryparams)}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
