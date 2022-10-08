open System
open Hw4.Parser
open Hw4.Calculator

let read = Console.ReadLine().Split(" ")

let calculator calcOptions =
    calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2
    
Console.WriteLine(read |> parseCalcArguments |> calculator)