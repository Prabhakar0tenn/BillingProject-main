# Project Context & Rules (Antigravity Memory File)

## 📌 Project Overview
- **Goal**: Build a simple, easy-to-use B2B Billing System (Khatabook / Tally style simplicity).
- **Target Audience**: Non-technical shop owners / manufacturers.
- **Deadline**: Friday.
- **Mode**: Step-by-step teaching & mentorship mode (User writes code by hand to learn C# / .NET syntax).

---

## ⚙️ Architecture & Design Guidelines
1. **No Login / No JWT**: Direct access to billing, parties, products, and invoices.
2. **Clean 4-Layer Architecture**:
   - `Models/` (Domain Entities)
   - `DTOs/` (API Payload contracts)
   - `Services/` (Business Logic Interfaces & Implementations)
   - `Data/` (MongoDbContext for DB queries)
   - `Controllers/` (REST API Endpoints)
3. **Database**: MongoDB (`BillingDb2`).

---

## 📋 Data Schema Preferences
- **Party**: `Id`, `PartyName`, `Phone`, `Gstin`, `Address`, `CreatedAt`.
- **Product**: `Id`, `Name`, `Price` (decimal), `Stock` (int), `CreatedAt`. (HSN Code removed as per user preference).
- **Invoice**: `Id`, `InvoiceNumber`, `PartyId`, `PartyName`, `PartyPhone`, `PartyGstin`, `Items` (`List<InvoiceItem>`), `SubTotal`, `Gst`, `GrandTotal`, `InvoiceDate`.

---

## 💡 Key Teaching Notes
- Explain every C# keyword, attribute, and framework concept line-by-line in plain Hinglish.
- Point out syntax errors gently before compiling so the user learns C# compiler rules.
