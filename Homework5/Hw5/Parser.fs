module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

let (|CalculatorOperation|_|) arg =
    match arg with
    | Plus -> Some CalculatorOperation.Plus
    | Minus -> Some CalculatorOperation.Minus
    | Multiply -> Some CalculatorOperation.Multiply
    | Divide -> Some CalculatorOperation.Divide
    | _ -> None
    
let (|Double|_|) arg =
    match Double.TryParse(arg: string) with
    | true, double -> Some double
    | _ -> None

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length with
    | 3 -> Ok args
    | _ -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation operation -> Ok (arg1, operation, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<('a * string * 'b), Message> =
    match args[0] with
    | Double val1 ->
        match args[2] with
        | Double val2 -> Ok (val1, args[1], val2)
        | _ -> Error Message.WrongArgFormat
    | _ -> Error Message.WrongArgFormat

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if arg2 = 0.0 && operation = CalculatorOperation.Divide then Error Message.DivideByZero
    else Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe {
        let! argLengthSupported = args |> isArgLengthSupported   
        let! argsParse = argLengthSupported |> parseArgs 
        let! operationParse = argsParse |> isOperationSupported 
        let! checkDividindByZero = operationParse |> isDividingByZero 
        return checkDividindByZero
    }