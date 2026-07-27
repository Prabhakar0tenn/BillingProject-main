import { useState } from "react";
import Navbar from "./components/Navbar";

import CreateBill from "./pages/CreateBill";
import Products from "./pages/Products";
import Parties from "./pages/Parties";
import Invoices from "./pages/Invoices";

import "./App.css";

function App() {
  const [currentPage, setCurrentPage] = useState("createBill");

  return (
    <>
      <Navbar currentPage={currentPage} setCurrentPage={setCurrentPage} />

      {currentPage === "products" && <Products />}

      {currentPage === "parties" && <Parties />}

      {currentPage === "createBill" && <CreateBill />}

      {currentPage === "invoices" && <Invoices />}
    </>
  );
}

export default App;
