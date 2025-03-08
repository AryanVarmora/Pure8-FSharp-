# Pure8-FSharp-

This emulator simulates basic arithmetic and logical operations on 8-bit numbers using F#. It performs binary addition, subtraction, and logical operations including NOT, AND, OR, and XOR. Arithmetic operations accept signed decimal values (-128 to 127), while logical operations use hexadecimal inputs (0x00 to 0xFF).

## 🚀 Features

- **Arithmetic Operations:** Addition (`ADD`), Subtraction (`SUB`)
- **Logical Operations:** Bitwise NOT, AND, OR, XOR
- **Number Representation:** 
  - Decimal numbers (-128 to 127) for arithmetic
  - Hexadecimal numbers (0x00 to 0xFF) for logical operations
- **Two's Complement Handling:** Correctly handles negative numbers and binary overflow
- **Functional Implementation:** Immutable variables and tail recursion

## 🛠️ Built With
- **F#** – A functional-first programming language
- **.NET** – Cross-platform development framework

## 📌 Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine
- F# tools installed (`dotnet fsi`)

## ⚙️ How to Run

1. Clone the repository:
```bash
git clone https://github.com/yourusername/8bit-emulator.git
