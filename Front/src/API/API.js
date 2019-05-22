import axios from "axios";

var config = {
    baseURL: 'https://localhost:5001/api/v1',
};



export class API {
    static get(resource) {
        return axios.get(resource, config)
            .then(response => {
                if (!response.data.success) {
                    throw response.data;
                }

                return response.data.data;
            })
    }
}