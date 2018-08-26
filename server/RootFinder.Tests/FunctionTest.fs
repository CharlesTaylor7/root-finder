namespace RootFinder.Tests


open Xunit
open Amazon.Lambda.TestUtilities
open RootFinder


module FunctionTest =
    [<Fact>]
    let ``Invoke ToUpper Lambda Function``() =
        let lambda = Lambda()
        let context = TestLambdaContext()
        let polynomial = Polynomial [| Complex.one |]
        let roots = lambda.Handler polynomial context

        Assert.True(false)

    [<EntryPoint>]
    let main _ = 0
