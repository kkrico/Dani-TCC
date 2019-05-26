import axios from "axios";

var config = {
    baseURL: 'https://pesquisapsicologia.azurewebsites.net/api/v1',
};

const parseResponse = response => {
    if (!response.data.success) {
        throw response.data;
    }

    return response.data.data;
}

export class API {
    static get(resource) {
        return axios.get(resource, config)
            .then(parseResponse);
    }

    static post(resource, body) {
        return axios.post(resource, body, config)
            .then(parseResponse);
    }
}