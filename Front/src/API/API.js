import axios from "axios";

var config = {
    baseURL: process.env.REACT_APP_API_URL,
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