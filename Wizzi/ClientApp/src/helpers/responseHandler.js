export const handleResponse = (response) => {
    return new Promise((resolve, reject) => {
        if (response.ok) {
            // return json if it was returned in the response
            var contentType = response.headers.get("content-type");
            if (contentType) {
                if (contentType.includes("application/json")) {
                    response.json().then(json => resolve(json));
                } else if (contentType.includes("text/html")) {
                    reject("No se ha encontrado la ruta indicada");
                } else {
                    response.text().then(text => resolve(text));
                }
            } else {
                resolve();
                // reject("No se ha encontrado contenido");
            }
        } else {
            // return error message from response body
            response.text().then(text => reject(text));
        }
    });
}

export const handleError = (error) => {
    return Promise.reject(error && error.message);
}