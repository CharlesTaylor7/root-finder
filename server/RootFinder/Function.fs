namespace Lambda.RootFinder

open System
open Amazon.Lambda.Core
open Amazon.Lambda.Serialization.Json

[<assembly: LambdaSerializer(typeof<JsonSerializer>)>]()

type Lambda() =

    member __.Handler (input: string) (_: ILambdaContext) =
        match input with
        | null -> String.Empty
        | _ -> input.ToUpper()
