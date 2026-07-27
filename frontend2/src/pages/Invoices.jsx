import { useEffect, useState } from "react";
import { getInvoices, generateInvoicePDF } from "../services/invoiceService";

function Invoices() {
    const [invoices, setInvoices] = useState([]);
    const [selectedInvoice, setSelectedInvoice] = useState(null);

    async function loadInvoices() {
        const data = await getInvoices();
        setInvoices(data);
    }

    function handlePrint(invoice) {
        if (invoice) {
            generateInvoicePDF(invoice);
        }
    }

    useEffect(() => {
        loadInvoices();
    }, []);

    return (
        <div className="container">
            <h2>Previous Invoices</h2>
            <hr />

            <table>
                <thead>
                    <tr>
                        <th>Invoice No.</th>
                        <th>Party</th>
                        <th>Phone</th>
                        <th>GSTIN</th>
                        <th>Subtotal</th>
                        <th>GST (5%)</th>
                        <th>Grand Total</th>
                        <th>Date</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {invoices.length === 0 ? (
                        <tr>
                            <td colSpan="9">No invoices found.</td>
                        </tr>
                    ) : (
                        invoices.map(invoice => (
                            <tr key={invoice.id}>
                                <td>
                                    <button
                                        className="link-btn"
                                        onClick={() => setSelectedInvoice(invoice)}
                                    >
                                        {invoice.invoiceNumber}
                                    </button>
                                </td>
                                <td>{invoice.partyName}</td>
                                <td>{invoice.partyPhone}</td>
                                <td>{invoice.partyGstin}</td>
                                <td>₹{invoice.subTotal}</td>
                                <td>₹{invoice.gst}</td>
                                <td><strong>₹{invoice.grandTotal}</strong></td>
                                <td>{new Date(invoice.invoiceDate).toLocaleDateString()}</td>
                                <td>
                                    <button
                                        className="btn-secondary"
                                        onClick={() => setSelectedInvoice(invoice)}
                                    >
                                        View Details
                                    </button>
                                    {" "}
                                    <button
                                        className="btn-success"
                                        onClick={() => handlePrint(invoice)}
                                    >
                                        Download PDF
                                    </button>
                                </td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>

            {}
            {selectedInvoice && (
                <div className="modal-overlay">
                    <div className="modal-content printable-invoice">
                        <div className="no-print" style={{ textAlign: "right", marginBottom: "10px" }}>
                            <button className="btn-success" onClick={() => handlePrint(selectedInvoice)}>
                                Download PDF
                            </button>
                            {" "}
                            <button className="btn-secondary" onClick={() => setSelectedInvoice(null)}>
                                Close
                            </button>
                        </div>

                        <div style={{ border: "1px solid #ccc", padding: "16px", background: "#fff" }}>
                            <div style={{ display: "flex", justifyContent: "space-between", borderBottom: "1px solid #ccc", paddingBottom: "10px", marginBottom: "15px" }}>
                                <div>
                                    <h2 style={{ margin: 0, color: "#0f172a" }}>Billing System</h2>
                                    <p style={{ fontSize: "14px", color: "#555" }}>TAX INVOICE</p>
                                </div>
                                <div style={{ textAlign: "right" }}>
                                    <h3 style={{ margin: 0 }}>{selectedInvoice.invoiceNumber}</h3>
                                    <p style={{ fontSize: "14px" }}>Date: {new Date(selectedInvoice.invoiceDate).toLocaleDateString()}</p>
                                </div>
                            </div>

                            <div style={{ marginBottom: "15px" }}>
                                <h4>Customer Details:</h4>
                                <p><strong>Party Name:</strong> {selectedInvoice.partyName}</p>
                                <p><strong>Phone:</strong> {selectedInvoice.partyPhone}</p>
                                <p><strong>Address:</strong> {selectedInvoice.partyAddress || "N/A"}</p>
                                <p><strong>GSTIN:</strong> {selectedInvoice.partyGstin}</p>
                            </div>

                            <table>
                                <thead>
                                    <tr>
                                        <th>Product Name</th>
                                        <th>Price</th>
                                        <th>Qty</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {selectedInvoice.items && selectedInvoice.items.map((item, index) => {
                                        const itemPrice = item.rate ?? item.price ?? 0;
                                        const itemTotal = item.totalAmount ?? item.totalPrice ?? item.total ?? (itemPrice * item.quantity) ?? 0;
                                        return (
                                            <tr key={index}>
                                                <td>{item.productName}</td>
                                                <td>₹{itemPrice}</td>
                                                <td>{item.quantity}</td>
                                                <td>₹{itemTotal}</td>
                                            </tr>
                                        );
                                    })}
                                </tbody>
                            </table>

                            <div className="bill-summary-box" style={{ marginLeft: "auto", marginTop: "15px" }}>
                                <p><strong>Subtotal:</strong> <span>₹{selectedInvoice.subTotal}</span></p>
                                <p><strong>GST (5%):</strong> <span>₹{selectedInvoice.gst}</span></p>
                                <p><strong>Grand Total:</strong> <span>₹{selectedInvoice.grandTotal}</span></p>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default Invoices;