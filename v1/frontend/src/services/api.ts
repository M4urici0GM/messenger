import axios from 'axios';

const api = axios.create({
    baseURL: '',
    timeout: 15000,
    timeoutErrorMessage: 'Timeout limit reached.',
});

export default api;
