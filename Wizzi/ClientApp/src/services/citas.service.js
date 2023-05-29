import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const citasService = {
    getCitaMedica,
    grabarSolicitudCita,
    getSolicitudesCitas,
    getSolicitudCita,
    postAgendarSolicitudCita,
    postRegistrarLlamadaSolicitudCita,
    deleteSolicitudCita,
    postRegistrarLlamadaCita,
};

function getCitaMedica(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/CitasMedicas/${id}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function grabarSolicitudCita(infoSolicitud) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(infoSolicitud)
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getSolicitudesCitas({ p, ciudad, sucursal, fechaRegistro = null,fechaRegistroInicio=null,fechaRegistroFinal=null,aplicarfiltrofecha }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    console.log("Solicitud "+ aplicarfiltrofecha)
    let queryparams = {
        p,
        ciudad,
        sucursal,
        fechaRegistro,
        fechaRegistroInicio,
        fechaRegistroFinal,
        aplicarfiltrofecha
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getSolicitudCita(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas/${id}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function postAgendarSolicitudCita(codigoSolicitud, datosAgenda) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosAgenda)
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas/${codigoSolicitud}/agendar`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function postRegistrarLlamadaSolicitudCita(codigoSolicitud, datosLlamada) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosLlamada)
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas/${codigoSolicitud}/registrarLlamada`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function deleteSolicitudCita(codigoSolicitud) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/SolicitudesCitas/${codigoSolicitud}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function postRegistrarLlamadaCita(codigoCita, datosLlamada) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosLlamada)
    };

    return fetch(`${config.apiUrl}/CitasMedicas/${codigoCita}/registrarLlamada`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
