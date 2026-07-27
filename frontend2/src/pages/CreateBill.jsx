import { useEffect, useState } from "react";

import { getProducts } from "../services/productService";
import { getParties } from "../services/partyService";
import { createInvoice } from "../services/invoiceService";

function CreateBill() {

    const [products, setProducts] = useState([]);
    const [parties, setParties] = useState([]);

    const [selectedParty, setSelectedParty] = useState("");
    const [selectedProduct, setSelectedProduct] = useState("");
    const [quantity, setQuantity] = useState("");

    const [items, setItems] = useState([]);

    async function loadProducts() {

        const data = await getProducts();

        setProducts(data);

    }

    async function loadParties() {

        const data = await getParties();

        setParties(data);

    }

    function handleAddItem() {

        if (!selectedProduct) {

            alert("Select Product");

            return;

        }

        if (!quantity || Number(quantity) <= 0) {

            alert("Enter Valid Quantity");

            return;

        }

        const product = products.find(function (p) {

            return p.id === selectedProduct;

        });

        if (Number(quantity) > product.stock) {

            alert("Not Enough Stock");

            return;

        }

        let found = false;

        const updatedItems = items.map(function (item) {

            if (item.productId === product.id) {

                found = true;

                const newQuantity = item.quantity + Number(quantity);

                return {

                    productId: item.productId,

                    productName: item.productName,

                    price: item.price,

                    quantity: newQuantity,

                    total: item.price * newQuantity

                };

            }

            return item;

        });

        if (found) {

            setItems(updatedItems);

        }
        else {

            const newItem = {

                productId: product.id,

                productName: product.name,

                price: product.price,

                quantity: Number(quantity),

                total: product.price * Number(quantity)

            };

            setItems([...items, newItem]);

        }

        setSelectedProduct("");

        setQuantity("");

    }

    function handleRemoveItem(index) {

        const updatedItems = items.filter(function (item, i) {

            return i !== index;

        });

        setItems(updatedItems);

    }

    const subTotal = items.reduce(function (sum, item) {

        return sum + item.total;

    }, 0);

    const gst = subTotal * 0.05;

    const grandTotal = subTotal + gst;

    async function handleSaveInvoice() {

        if (!selectedParty) {

            alert("Select Party");

            return;

        }

        if (items.length === 0) {

            alert("Add Products");

            return;

        }

        const invoiceItems = items.map(function (item) {

            return {

                productId: item.productId,

                quantity: item.quantity

            };

        });

        const invoice = {

            partyId: selectedParty,

            items: invoiceItems

        };

        await createInvoice(invoice);

        alert("Invoice Created Successfully");

        setSelectedParty("");

        setSelectedProduct("");

        setQuantity("");

        setItems([]);

        loadProducts();

    }

    useEffect(function () {

        loadProducts();

        loadParties();

    }, []);

    return (

        <div className="container">

            <h2>Create Bill</h2>

            <hr />

            <h3>Select Party</h3>

            <select
                value={selectedParty}
                onChange={(e) => setSelectedParty(e.target.value)}
            >

                <option value="">Select Party</option>

                {parties.map(function (party) {

                    return (

                        <option
                            key={party.id}
                            value={party.id}
                        >

                            {party.partyName}

                        </option>

                    );

                })}

            </select>

            <hr />

            <h3>Add Product</h3>

            <select
                value={selectedProduct}
                onChange={(e) => setSelectedProduct(e.target.value)}
            >

                <option value="">Select Product</option>

                {products.map(function (product) {

                    return (

                        <option
                            key={product.id}
                            value={product.id}
                        >

                            {product.name}

                        </option>

                    );

                })}

            </select>

            <br /><br />

            <input
                type="number"
                placeholder="Quantity"
                value={quantity}
                onChange={(e) => setQuantity(e.target.value)}
            />

            <br /><br />

            <button onClick={handleAddItem}>

                Add Item

            </button>

            <hr />

            <h3>Bill Items</h3>

            <table>

                <thead>

                    <tr>

                        <th>Product</th>

                        <th>Price</th>

                        <th>Quantity</th>

                        <th>Total</th>

                        <th>Action</th>

                    </tr>

                </thead>

                <tbody>

                    {items.map(function (item, index) {

                        return (

                            <tr key={index}>

                                <td>{item.productName}</td>

                                <td>{item.price}</td>

                                <td>{item.quantity}</td>

                                <td>{item.total}</td>

                                <td>

                                    <button
                                        onClick={() => handleRemoveItem(index)}
                                    >

                                        Remove

                                    </button>

                                </td>

                            </tr>

                        );

                    })}

                </tbody>

            </table>

            <br />

            <h3>Bill Summary</h3>

            <p>

                <strong>Subtotal :</strong> {subTotal}

            </p>

            <p>

                <strong>GST :</strong> {gst}

            </p>

            <p>

                <strong>Grand Total :</strong> {grandTotal}

            </p>

            <br />

            <button onClick={handleSaveInvoice}>

                Save Invoice

            </button>

        </div>

    );

}

export default CreateBill;