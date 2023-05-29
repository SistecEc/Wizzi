import { authHeader, config, handleError, handleResponse } from '../helpers';

export const campaniasService = {
    getCampanias,
    grabarCampania,
    actualizarCampania,
    deleteCampania,

    getSubcampanias,
    getSubcampaniaImg,
    grabarSubCampania,
    actualizarSubCampania,
    deleteSubCampania,
};

function getCampanias(pagina) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/Campanias?p=${pagina}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function grabarCampania(infoCampania) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(infoCampania)
    };

    return fetch(`${config.apiUrl}/Campanias/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function actualizarCampania(infoCampania) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(infoCampania)
    };

    return fetch(`${config.apiUrl}/Campanias/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function deleteCampania(codigoCampania) {
    const requestOptions = {
        method: 'DELETE',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
    };

    return fetch(`${config.apiUrl}/Campanias/${codigoCampania}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getSubcampanias(codigoCampania) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    return fetch(`${config.apiUrl}/Campanias/${codigoCampania}/subCampanias`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function getSubcampaniaImg(codigoSubCampania) {
    const requestOptions = {
        method: 'GET',
    };

    return fetch(`${config.apiUrl}/SubCampanias/${codigoSubCampania}/imagen`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function grabarSubCampania(infoSubCampania) {
    var formData = new FormData();
    formData.append("codigoCampania", infoSubCampania.codigoCampania);
    formData.append("descripcion", infoSubCampania.descripcion);
    formData.append("imagen", infoSubCampania.imagen);
    formData.append("fechaInicio", infoSubCampania.fechaInicio);
    formData.append("fechaFin", infoSubCampania.fechaFin);
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader() },
        body: formData
    };

    return fetch(`${config.apiUrl}/Subcampanias/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function actualizarSubCampania(infoSubCampania) {
    var formData = new FormData();
    formData.append("codigo", infoSubCampania.codigo);
    formData.append("descripcion", infoSubCampania.descripcion);
    formData.append("imagen", infoSubCampania.imagen);
    formData.append("fechaInicio", infoSubCampania.fechaInicio);
    formData.append("fechaFin", infoSubCampania.fechaFin);
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader() },
        body: formData
    };

    return fetch(`${config.apiUrl}/Subcampanias/`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}

function deleteSubCampania(codigoSubCampania) {
    const requestOptions = {
        method: 'DELETE',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
    };

    return fetch(`${config.apiUrl}/Subcampanias/${codigoSubCampania}`, requestOptions)
        .then(handleResponse, handleError)
        .then(resp => {
            return resp;
        });
}
