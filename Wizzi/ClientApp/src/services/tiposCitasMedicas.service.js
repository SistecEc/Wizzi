import { authHeader, config, handleError, handleResponse } from '../helpers';

export const tiposCitasMedicasService = {
    getTiposCitasMedicas
};

function getTiposCitasMedicas() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/TiposCitasMedicas`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}