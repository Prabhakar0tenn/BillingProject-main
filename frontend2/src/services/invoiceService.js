import api from "./api";
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";

export async function getInvoices() {
    const response = await api.get("/Invoice");
    return response.data;
}

export async function getInvoiceById(id) {
    const response = await api.get(`/Invoice/${id}`);
    return response.data;
}

export async function createInvoice(invoice) {
    const response = await api.post("/Invoice", invoice);
    return response.data;
}

export function generateInvoicePDF(invoice) {
    if (!invoice) return;

    const doc = new jsPDF();

    doc.setFontSize(18);
    doc.text("Billing System", 14, 20);
    doc.setFontSize(10);
    doc.text("TAX INVOICE", 14, 26);

    doc.setFontSize(11);
    doc.text(`Invoice No: ${invoice.invoiceNumber || ""}`, 140, 20);
    doc.text(`Date: ${invoice.invoiceDate ? new Date(invoice.invoiceDate).toLocaleDateString() : ""}`, 140, 26);

    doc.line(14, 30, 196, 30);

    doc.setFontSize(11);
    doc.text("Customer Details:", 14, 38);
    doc.setFontSize(10);
    doc.text(`Party Name: ${invoice.partyName || ""}`, 14, 45);
    doc.text(`Phone: ${invoice.partyPhone || ""}`, 14, 51);
    doc.text(`Address: ${invoice.partyAddress || "N/A"}`, 14, 57);
    doc.text(`GSTIN: ${invoice.partyGstin || ""}`, 14, 63);

    const tableData = (invoice.items || []).map(item => {
        const itemPrice = item.rate ?? item.price ?? 0;
        const itemTotal = item.totalAmount ?? item.totalPrice ?? item.total ?? (itemPrice * item.quantity) ?? 0;
        return [
            item.productName || "Product",
            `Rs. ${itemPrice}`,
            item.quantity,
            `Rs. ${itemTotal}`
        ];
    });

    autoTable(doc, {
        startY: 69,
        head: [["Product Name", "Price", "Quantity", "Total"]],
        body: tableData,
        theme: "grid",
        headStyles: { fillColor: [30, 41, 59] }
    });

    const finalY = (doc.lastAutoTable && doc.lastAutoTable.finalY) ? doc.lastAutoTable.finalY + 10 : 130;

    doc.setFontSize(10);
    doc.text(`Subtotal: Rs. ${invoice.subTotal}`, 140, finalY);
    doc.text(`GST (5%): Rs. ${invoice.gst}`, 140, finalY + 6);
    doc.setFontSize(11);
    doc.text(`Grand Total: Rs. ${invoice.grandTotal}`, 140, finalY + 12);

    doc.save(`${invoice.invoiceNumber || "Invoice"}.pdf`);
}