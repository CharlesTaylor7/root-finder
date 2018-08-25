namespace Lambda.RootFinder.Tests


open Xunit
open Amazon.Lambda.TestUtilities

open Lambda.RootFinder


module FunctionTest =
    [<Fact>]
    let ``Invoke ToUpper Lambda Function``() =
        // Invoke the lambda function and confirm the string was upper cased.
        let lambdaFunction = Function()
        let context = TestLambdaContext()
        let upperCase = lambdaFunction.FunctionHandler "hello world" context

        Assert.Equal("HELLO WORLD", upperCase)

    [<EntryPoint>]
    let main _ = 0
