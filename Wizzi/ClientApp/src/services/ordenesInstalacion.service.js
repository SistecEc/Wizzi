import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const ordenesInstalacionService = {
    getPorReagendar,
};

function getPorReagendar({ p, ciudad, sucursal,fechaRegistroInicio=null, fechaRegistroFinal=null  }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {
        p,
        ciudad,
        sucursal,
        fechaRegistroInicio,
        fechaRegistroFinal
    };

    return fetch(`${config.apiUrl}/OrdenesInstalacion/porReagendar?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
