open System

// Convert Hexadecimal Input to Binary List
let hexToBinary (hex: string) : int list =
    try
        let sanitizedHex = hex.Trim().ToUpper().Replace("0x", "")
        let number = Convert.ToInt32(sanitizedHex, 16)
        List.init 8 (fun i -> (number >>> (7 - i)) &&& 1)
    with
    | _ -> failwith "Invalid hex value. Please enter a valid 8-bit hex number."

// Convert Binary to Hexadecimal
let binaryToHex (bits: int list) : string =
    let decimalValue = bits |> List.fold (fun acc bit -> (acc <<< 1) ||| bit) 0
    sprintf "%X" decimalValue

// Convert Signed Decimal to Binary (Handles Two’s Complement)
let signedDecimalToBinary (decimal: int) : int list =
    if decimal < -128 || decimal > 127 then failwith "Value out of range for 8-bit signed integer."
    let value = if decimal < 0 then 256 + decimal else decimal
    List.init 8 (fun i -> (value >>> (7 - i)) &&& 1)

// Convert Binary to String for Display
let binaryToString (bits: int list) : string =
    bits |> List.map string |> String.concat "; "

// Logical Operations
let bitwiseNot bits = List.map (fun bit -> 1 - bit) bits
let bitwiseAnd = List.map2 (&&&)
let bitwiseOr = List.map2 (|||)
let bitwiseXor = List.map2 (^^^)

// Process Logical Operations (Returns a Formatted String)
let processLogical op a b =
    let aBin = hexToBinary a
    let bBin = hexToBinary b
    let result =
        match op with
        | "AND" -> bitwiseAnd aBin bBin
        | "OR" -> bitwiseOr aBin bBin
        | "XOR" -> bitwiseXor aBin bBin
        | _ -> failwith "Invalid logical operation"
    let hexResult = binaryToHex result
    sprintf "\n1. %s = [%s] = %s\n2. %s\n3. %s = [%s] = %s\n4. ---------------------------\n5. Result = [%s] = %s" 
        a (binaryToString aBin) a op b (binaryToString bBin) b (binaryToString result) hexResult

// Process NOT Operation (Returns a Formatted String)
let processNot a =
    let aBin = hexToBinary a
    let result = bitwiseNot aBin
    let hexResult = binaryToHex result
    sprintf "\n1. %s = [%s] = %s\n2. NOT\n3. Result = [%s] = %s" 
        a (binaryToString aBin) a (binaryToString result) hexResult

// Process Arithmetic Operations (Returns a Formatted String)
let processArithmetic op a b =
    let result =
        match op with
        | "ADD" -> a + b
        | "SUB" -> a - b
        | _ -> failwith "Invalid arithmetic operation"

    if result < -128 || result > 127 then failwith "Result out of range for 8-bit signed integer."
    
    let resultBinary = signedDecimalToBinary result
    sprintf "\n1. %d = [%s] = %d\n2. %s\n3. %d = [%s] = %d\n4. ---------------------------\n5. Result = [%s] = %d" 
        a (binaryToString (signedDecimalToBinary a)) a op 
        b (binaryToString (signedDecimalToBinary b)) b 
        (binaryToString resultBinary) result

// Parses and Executes User Input (Purely Handles I/O)
let rec emulator () =
    printf "\nEnter the operation you want to perform (NOT, OR, AND, XOR, ADD, SUB or QUIT):\n"
    printf "Available operations: NOT, OR, AND, XOR, ADD, SUB\n"
    let input = Console.ReadLine().Trim().ToUpper()

    match input with
    | "ADD" | "SUB" ->  
        printf "\nEnter first decimal value (-128 to 127): "
        let num1 = Console.ReadLine().Trim()
        printf "\nEnter second decimal value (-128 to 127): "
        let num2 = Console.ReadLine().Trim()
        try
            let output = processArithmetic input (int num1) (int num2)
            printfn "%s" output
        with 
        | :? FormatException -> printfn "\nInvalid input. Please enter valid decimal numbers."
        | _ -> printfn "\nInvalid decimal values. Please enter valid 8-bit signed integers."
        emulator()

    | "AND" | "OR" | "XOR" ->  
        printf "\nEnter first Hex value: "
        let hexA = Console.ReadLine().Trim().ToUpper()
        printf "\nEnter second Hex value: "
        let hexB = Console.ReadLine().Trim().ToUpper()
        try
            let output = processLogical input hexA hexB
            printfn "%s" output
        with
        | Failure msg -> printfn "\n%s" msg
        | _ -> printfn "\nInvalid hex values. Please enter valid 8-bit hex numbers."
        emulator()

    | "NOT" ->
        printf "\nEnter Hex value: "
        let hex = Console.ReadLine().Trim().ToUpper()
        try
            let output = processNot hex
            printfn "%s" output
        with
        | Failure msg -> printfn "\n%s" msg
        | _ -> printfn "\nInvalid hex value. Please enter a valid 8-bit hex number."
        emulator()

    | "QUIT" -> printfn "\nExiting..."
    | _ -> 
        printfn "\nInvalid operation. Please try again."
        emulator()

[<EntryPoint>]
let main _ =
    emulator()
    0
