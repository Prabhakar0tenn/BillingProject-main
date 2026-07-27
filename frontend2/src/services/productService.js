import api from "./api";

export async function getProducts() {
    const response = await api.get("/Product");
    return response.data;
}

export async function addProduct(product) {
    const response = await api.post("/Product", product);
    return response.data;
}

export async function deleteProduct(id) {
    await api.delete(`/Product/${id}`);
}

export async function updateStock(id, stock) {
    await api.put(`/Product/${id}`, stock, {
        headers: { "Content-Type": "application/json" }
    });
}

export async function updatePrice(id, price) {
    await api.put(`/Product/${id}/price`, price, {
        headers: { "Content-Type": "application/json" }
    });
}