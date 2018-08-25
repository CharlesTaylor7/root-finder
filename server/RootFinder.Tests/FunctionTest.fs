namespace RootFinder.Tests


open Xunit
open Amazon.Lambda.TestUtilities
open RootFinder


module FunctionTest =
    [<Fact>]
    let ``Invoke ToUpper Lambda Function``() =
        let lambda = Lambda()
        let context = TestLambdaContext()
        let upperCase = lambda.Handler "hello world" context

        Assert.Equal("HELLO WORLD", upperCase)

    [<EntryPoint>]
    let main _ = 0
