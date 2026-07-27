function Navbar(props) {
    return (
        <div className="navbar">
            <button
                onClick={() => props.setCurrentPage("products")}
            >
                Products
            </button>

            <button
                onClick={() => props.setCurrentPage("parties")}
            >
                Parties
            </button>

            <button
                onClick={() => props.setCurrentPage("createBill")}
            >
                Create Bill
            </button>

            <button
                onClick={() => props.setCurrentPage("invoices")}
            >
                Invoices
            </button>
        </div>
    );
}

export default Navbar;