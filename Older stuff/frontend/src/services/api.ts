import axios, { AxiosResponse, AxiosRequestConfig } from 'axios';

const api = axios.create({
    baseURL: '',
    timeout: 15000,
    timeoutErrorMessage: 'Timeout limit reached.',
});


api.interceptors.response.use(
    (response: AxiosResponse) => response,
    (error: AxiosResponse) => {

    },
);

api.interceptors.request.use((config: AxiosRequestConfig): AxiosRequestConfig => {
    const requestConfig = config;
    requestConfig.headers['Content-Type'] = 'application/json';
    return requestConfig;
});

export default api;
