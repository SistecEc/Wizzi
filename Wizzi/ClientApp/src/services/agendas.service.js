import queryString from 'query-string';
import { authHeader, config, handleError, handleResponse } from '../helpers';

export const agendasService = {
    getAgendas,
    getBuscarAgendas,
    getAgendaPorConfirmar,
    getAgendasPorConfirmar,
    getAgendaPorCompletar,
    getAgendasPorCompletar,
    patchReagendar,
    putConfirmarAsistencia,
    putRegistrarInasistencia,
    postAgendarCita,
    PutCancelarAgenda,
    PostGenerarDocumentoEmpleado,
};

function getAgendas({ inicio, fin, empleado, p, estados, ciudad, sucursal }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {
        inicio,
        fin,
        empleado,
        p,
        estados,
        ciudad,
        sucursal,
    };

    return fetch(`${config.apiUrl}/Agendas?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getBuscarAgendas({ q, ciudad, sucursal }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {
        q,
        ciudad,
        sucursal,
    };

    return fetch(`${config.apiUrl}/Agendas/buscar?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getAgendaPorConfirmar(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };


    return fetch(`${config.apiUrl}/Agendas/porConfirmar/${id}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getAgendasPorConfirmar({ p, ciudad, sucursal,fechaRegistroInicio=null, fechaRegistroFinal=null,aplicarfiltrofecha  }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let queryparams = {
        p,
        ciudad,
        sucursal,
        fechaRegistroInicio,
        fechaRegistroFinal,
        aplicarfiltrofecha
    };

    return fetch(`${config.apiUrl}/Agendas/porConfirmar?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getAgendaPorCompletar(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/Agendas/porCompletar/${id}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getAgendasPorCompletar({ p, ciudad, sucursal,fechaRegistroInicio=null, fechaRegistroFinal=null, aplicarfiltrofecha  }) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    console.log("se aplica aqui: " + aplicarfiltrofecha)
    let queryparams = {
        p,
        ciudad,
        sucursal,
        fechaRegistroInicio,
        fechaRegistroFinal,
        aplicarfiltrofecha
    };

    return fetch(`${config.apiUrl}/Agendas/porCompletar?` + queryString.stringify(queryparams), requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function patchReagendar(codigoAgenda, datosReagenda) {
    const requestOptions = {
        method: 'PATCH',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosReagenda)
    };

    return fetch(`${config.apiUrl}/Agendas/${codigoAgenda}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function putConfirmarAsistencia(codigoAgenda, confirmarAsistenciaDto) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(confirmarAsistenciaDto)
    };

    return fetch(`${config.apiUrl}/Agendas/${codigoAgenda}/confirmarAsistencia`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function putRegistrarInasistencia(codigoAgenda, registrarInasistenciaDto) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(registrarInasistenciaDto)
    };

    return fetch(`${config.apiUrl}/Agendas/${codigoAgenda}/registrarInasistencia`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function postAgendarCita(datosAgenda) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(datosAgenda)
    };

    return fetch(`${config.apiUrl}/Agendas`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function PutCancelarAgenda(codigoAgenda, cancelarAgendaDto) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(cancelarAgendaDto)
    };

    return fetch(`${config.apiUrl}/Agendas/${codigoAgenda}/cancelar`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function PostGenerarDocumentoEmpleado(codigoAgenda, generarDocumentoEmpleadoDto) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(generarDocumentoEmpleadoDto)
    };

    return fetch(`${config.apiUrl}/Agendas/${codigoAgenda}/generarDocumentoEmpleado`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
