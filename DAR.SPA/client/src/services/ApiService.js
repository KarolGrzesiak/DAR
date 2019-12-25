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
    return axios.post(url, resource, {
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Expose-Headers": "Access-Control-*",
        "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS, HEAD",
        "Access-Control-Allow-Headers":
          "Access-Control-*, Origin, X-Requested-With, Content-Type, Accept"
      }
    });
  }
};

export default ApiService;
