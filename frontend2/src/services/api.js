import axios from "axios";

const api = axios.create({
    baseURL: "https://billing-backend2-api-dycbdqb7h7d6gvbw.southindia-01.azurewebsites.net/api"
});

export default api;