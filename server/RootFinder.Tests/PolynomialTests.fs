namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Complex
open Polynomial
open Solver
open FsUnitTyped

module PolynomialTests =

  [<Test>]
  let ``Polynomial Multiplication`` () =
    let roots = roots_of_unity 10
    let p = polynomial_of_unity 10

    let one = Polynomial [| Complex.one |]
    let product_of_linear_terms = Seq.fold (fun product root -> product * (Polynomial [| -root; Complex.one |])) one roots

    product_of_linear_terms |> shouldEqual p


