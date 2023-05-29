import { authHeader, config, handleError, handleResponse } from '../helpers';

export const gruposCitasService = {
    getAgendasDeCita,
    postAgendarCita,
    getMovimientosDeSolicitud,
};

function getAgendasDeCita(codigoGrupoCita) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/GruposCitasMedicas/${codigoGrupoCita}/agendas`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getMovimientosDeSolicitud(codigoSolicitud) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/GruposCitasMedicas/${codigoSolicitud}/movimientos`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function postAgendarCita(codigoGrupoCita, datosAgenda) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosAgenda)
    };

    return fetch(`${config.apiUrl}/GruposCitasMedicas/${codigoGrupoCita}/agendar`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
