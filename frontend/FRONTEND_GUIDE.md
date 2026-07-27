# 📱 Simple B2B Billing System — Frontend2 (Khatabook/Tally Style)

## 🌟 Implemented Features & Improvements

All requested frontend enhancements have been implemented in clean, beginner-friendly React code:

### 1. `container` Layout Wrapper
- Wrapped `Parties.jsx`, `Products.jsx`, `CreateBill.jsx`, and `Invoices.jsx` in `<div className="container">`.
- Added clean CSS styling for `.container` with shadow, padding, and centered responsiveness.

### 2. Professional Blue Top Navbar
- **Header Title**: `Billing System` (Left)
- **Nav Buttons**: `Products` | `Parties` | `Create Bill` | `Invoices`
- **Active State**: Highlighting current active page button.

### 3. Invoice Details View
- Clicking `INV-2026-xxx` or "View Details" opens the full Tax Invoice modal.
- Includes Tax Invoice header, Customer Billed-to details, product breakdown table, subtotal, GST (5%), and grand total.

### 4. PDF Download & Print
- Added **"🖨️ Print / Download PDF"** button inside the Invoice Details Modal.
- Uses clean `@media print` rules for A4 printable output.

### 5. Input Validations
- `CreateBill.jsx`:
  - Quantity <= 0 alert: `"Quantity must be greater than zero."`
  - Stock availability check.
  - Mandatory Party & Product selection checks.

### 6. Single Row Product Combination
- In `CreateBill.jsx`, adding the same product multiple times automatically updates and combines the quantity on a single row (e.g. `Pen Qty 8`) instead of creating duplicate rows.
