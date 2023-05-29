import { authHeader, config, handleError, handleResponse } from '../helpers';

export const clientesService = {
    buscarClientes,
    getCliente,
    getClientes,
    grabarCliente,
    actualizarCliente,
};

function buscarClientes(datoBuscar, pagina, tamanioPagina) {
    pagina = pagina > 0 ? pagina : 1;
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/clientes/find?query=${encodeURI(datoBuscar)}&p=${pagina}&tp=${tamanioPagina}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getCliente(codigoCliente) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/clientes/${codigoCliente}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getClientes(pagina) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/clientes?p=${pagina}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function grabarCliente(infoCliente) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(infoCliente)
    };

    return fetch(`${config.apiUrl}/clientes/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function actualizarCliente(infoCliente) {
    const requestOptions = {
        method: 'PATCH',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(infoCliente)
    };

    return fetch(`${config.apiUrl}/clientes/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
