namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Polynomial
open Complex
open Solver
open FsUnit
open System.Linq
open System

module SolverTests =
  open FsUnitTyped

  [<Test>]
  let ``Test Divide By Derivative``() =
    // -1 + x^2
    let p = [| -Complex.one; Complex.zero; Complex.one |] |> Polynomial
    // 2x
    let d = p.derivative

    // 0.5*x - 0.5 / x
    let expected_quotient = [| Complex.zero; complex 0.5 |] |> Polynomial
    let expected_remainder = [| complex -0.5 |] |> Polynomial

    let (quotient, remainder) = divideByDerivative p

    quotient |> should equal expected_quotient
    remainder |> should equal expected_remainder

  [<Test>]
  let ``Cyclomatic Polynomial Should Have Primitive Roots Of Unity``() =

    // x^2 + x + 1.
    // Roots are primitive 3rd roots of -1.
    let coefficients = Array.create 3 <| complex 1.0
    let polynomial = Polynomial coefficients
    let roots = solve polynomial

    let angle = 2.0 * Math.PI / 3.0;
    let expected_roots = [| polar 1 angle ; polar 1 (-angle) |]

    roots |> should equal expected_roots
