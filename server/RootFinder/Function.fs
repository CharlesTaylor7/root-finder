namespace RootFinder

open System
open Amazon.Lambda.Core
open Amazon.Lambda.Serialization.Json
open Solver

[<assembly: LambdaSerializer(typeof<JsonSerializer>)>]()

type Lambda() =

  member __.Handler (input: string) (_: ILambdaContext) =
    "Hello World!"

//  member __.Handler (input: Polynomial) (_: ILambdaContext) : Complex array =
//    solve input
