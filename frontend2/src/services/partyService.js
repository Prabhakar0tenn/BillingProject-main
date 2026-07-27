import api from "./api";

export async function getParties() {
    const response = await api.get("/Parties");
    return response.data;
}
export async function getPartyById(id) {
    const response = await api.get(`/Parties/${id}`);
    return response.data;
}

export async function addParty(party) {
    const response = await api.post("/Parties", party);
    return response.data;
}

export async function deleteParty(id) {
    await api.delete(`/Parties/${id}`);
}

