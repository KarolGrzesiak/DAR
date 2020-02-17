import axios from "axios";

const ApiService = {
    init(baseURL) {
        axios.defaults.baseURL = baseURL;
    },

    get(url) {
        return axios.get(url);
    },
    delete(url) {
        return axios.delete(url);
    },
    post(url, resource) {
        return axios.post(url, resource);
    }
};

export default ApiService;
