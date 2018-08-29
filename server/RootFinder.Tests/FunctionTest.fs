namespace RootFinder.Tests

open Xunit
open Amazon.Lambda.TestUtilities
open RootFinder
open Polynomial
open Complex
open Solver

module FunctionTest =
    [<Fact>]
    let ``Roots``() =
        let coefficients = Array.create 3 <| complex 1.0
        let polynomial = Polynomial coefficients
        let roots = solve polynomial

        Assert.True(roots.Length = 2)
