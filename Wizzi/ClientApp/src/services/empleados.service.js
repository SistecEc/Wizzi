import * as queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const empleadosService = {
    getEmpleados,
};

function getEmpleados({ paginado, p, soloRol, ciudad, sucursal, soloPuedeAgendar }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {
        paginado,
        p,
        soloRol,
        ciudad,
        sucursal,
        soloPuedeAgendar,
    };

    return fetch(`${config.apiUrl}/Empleados?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
