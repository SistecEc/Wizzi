import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const localizacionesService = {
    getPaises,
    getProvincias,
    getCantones,
    getParroquias
};

function getPaises(paginado = true) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {};
    if (paginado === false) {
        queryparams.paginado = paginado;
    }

    return fetch(`${config.apiUrl}/localizaciones/paises?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getProvincias(pais, paginado = true) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {};
    queryparams.pais = pais;
    if (paginado === false) {
        queryparams.paginado = paginado;
    }

    return fetch(`${config.apiUrl}/localizaciones/provincias?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getCantones(provincia, paginado = true) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {};
    queryparams.provincia = provincia;
    if (paginado === false) {
        queryparams.paginado = paginado;
    }

    return fetch(`${config.apiUrl}/localizaciones/cantones?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getParroquias(canton, paginado = true) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {};
    queryparams.canton = canton;
    if (paginado === false) {
        queryparams.paginado = paginado;
    }

    return fetch(`${config.apiUrl}/localizaciones/parroquias?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}