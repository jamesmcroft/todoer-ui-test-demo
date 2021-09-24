import axios from "axios";
import environment from "../utils/environment";

const axiosClient = axios.create({
  baseURL: environment.BaseUrl,
  headers: {
    'Content-Type': 'application/json'
  }
});

axiosClient.defaults.withCredentials = true;

axiosClient.interceptors.response.use(
  function (response) {
    return response;
  },
  function (error) {
    const code = parseInt(error.response && error.response.status);
    if (code === 401) {
      if (location.pathname !== '/login') {
        window.location.replace('/login');
      }
    }
    return Promise.reject(error);
  });

export default axiosClient;
