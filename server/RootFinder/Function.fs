namespace RootFinder

open System
open Amazon.Lambda.Core
open Amazon.Lambda.Serialization.Json

[<assembly: LambdaSerializer(typeof<JsonSerializer>)>]()

type Lambda() =

    member __.Handler (input: Polynomial) (_: ILambdaContext) : array<Complex> = [||]
