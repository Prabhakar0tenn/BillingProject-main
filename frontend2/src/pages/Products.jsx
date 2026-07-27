import { useEffect, useState } from "react";

import {
    getProducts,
    addProduct,
    deleteProduct,
    updateStock,
    updatePrice
} from "../services/productService";

function Products() {

    const [products, setProducts] = useState([]);

    const [name, setName] = useState("");

    const [price, setPrice] = useState("");

    const [stock, setStock] = useState("");

    const [stockInputs, setStockInputs] = useState({});

    const [priceInputs, setPriceInputs] = useState({});

    async function loadProducts() {

        const data = await getProducts();

        setProducts(data);

    }

    async function handleAddProduct() {

        if (!name) {

            alert("Enter Product Name");

            return;

        }

        if (!price || Number(price) <= 0) {

            alert("Enter Valid Price");

            return;

        }

        if (!stock || Number(stock) < 0) {

            alert("Enter Valid Stock");

            return;

        }

        const product = {

            name: name,

            price: Number(price),

            stock: Number(stock)

        };

        await addProduct(product);

        await loadProducts();

        setName("");

        setPrice("");

        setStock("");

    }

    async function handleDelete(id) {

        await deleteProduct(id);

        await loadProducts();

    }

    async function handleUpdateStock(id) {

        const newStock = Number(stockInputs[id]);

        if (!newStock || newStock <= 0) {

            alert("Enter Valid Stock");

            return;

        }

        await updateStock(id, newStock);

        await loadProducts();

        setStockInputs({

            ...stockInputs,

            [id]: ""

        });

    }

    async function handleUpdatePrice(id) {

        const newPrice = Number(priceInputs[id]);

        if (!newPrice || newPrice <= 0) {

            alert("Enter Valid Price");

            return;

        }

        await updatePrice(id, newPrice);

        await loadProducts();

        setPriceInputs({

            ...priceInputs,

            [id]: ""

        });

    }

    useEffect(function () {

        loadProducts();

    }, []);

    return (

        <div className="container">

            <h2>Products</h2>

            <hr />

            <h3>Add Product</h3>

            <input
                type="text"
                placeholder="Product Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
            />

            <br /><br />

            <input
                type="number"
                placeholder="Price"
                value={price}
                onChange={(e) => setPrice(e.target.value)}
            />

            <br /><br />

            <input
                type="number"
                placeholder="Stock"
                value={stock}
                onChange={(e) => setStock(e.target.value)}
            />

            <br /><br />

            <button onClick={handleAddProduct}>

                Add Product

            </button>

            <hr />

            <h3>Product List</h3>

            <table>

                <thead>

                    <tr>

                        <th>Name</th>

                        <th>Price</th>

                        <th>Stock</th>

                        <th>Add Stock</th>

                        <th>New Price</th>

                        <th>Action</th>

                    </tr>

                </thead>

                <tbody>

                    {products.map(function (product) {

                        return (

                            <tr key={product.id}>

                                <td>{product.name}</td>

                                <td>{product.price}</td>

                                <td>{product.stock}</td>

                                <td>

                                    <input
                                        type="number"
                                        placeholder="Stock"
                                        value={stockInputs[product.id] || ""}
                                        onChange={function (e) {

                                            setStockInputs({

                                                ...stockInputs,

                                                [product.id]: e.target.value

                                            });

                                        }}
                                    />

                                    <button
                                        onClick={function () {

                                            handleUpdateStock(product.id);

                                        }}
                                    >

                                        Update

                                    </button>

                                </td>

                                <td>

                                    <input
                                        type="number"
                                        placeholder="Price"
                                        value={priceInputs[product.id] || ""}
                                        onChange={function (e) {

                                            setPriceInputs({

                                                ...priceInputs,

                                                [product.id]: e.target.value

                                            });

                                        }}
                                    />

                                    <button
                                        onClick={function () {

                                            handleUpdatePrice(product.id);

                                        }}
                                    >

                                        Update

                                    </button>

                                </td>

                                <td>

                                    <button
                                        onClick={function () {

                                            handleDelete(product.id);

                                        }}
                                    >

                                        Delete

                                    </button>

                                </td>

                            </tr>

                        );

                    })}

                </tbody>

            </table>

        </div>

    );

}

export default Products;