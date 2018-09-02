namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Polynomial
open Complex
open Solver
open System.Linq
open System
open FsUnitTyped

module SolverTests =

  [<Test>]
  let ``Divide By Derivative``() =
    // -1 + x^2
    let p = [| -Complex.one; Complex.zero; Complex.one |] |> Polynomial
    // 2x
    let d = p.derivative

    // 0.5*x - 0.5 / x
    let expected_quotient = [| Complex.zero; complex 0.5 |] |> Polynomial
    let expected_remainder = [| complex -0.5 |] |> Polynomial

    let (quotient, remainder) = divideByDerivative p

    quotient |> shouldEqual expected_quotient
    remainder |> shouldEqual expected_remainder

  [<Test>]
  let ``Cyclomatic polynomial should have primitive roots of unity``() =

    // x^2 + x + 1.
    // Roots are primitive 3rd roots of -1.
    let coefficients = Array.create 3 <| complex 1.0
    let polynomial = Polynomial coefficients
    let roots = solve polynomial

    let angle = 2.0 * Math.PI / 3.0;
    let expected_roots = [| polar 1 angle ; polar 1 (-angle) |]

    roots |> shouldEqual expected_roots

  [<Test>]
  let ``Solve polynomial where it shares a root with its derivative``() =

    // x^3.
    let polynomial = monomial Complex.one 3
    let roots = solve polynomial

    let expected_roots = Array.zeroCreate 3

    roots |> shouldEqual expected_roots
