import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const reportesService = {
    getSolicitudes,
};

function getSolicitudes({ sucursal, empleado, fechaInicio, fechaFinal, estadoagenda}) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/reporte/CitasAtendidas/${sucursal}/${empleado}/${fechaInicio}/${fechaFinal}/${estadoagenda}`)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
