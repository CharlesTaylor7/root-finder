namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Polynomial
open Complex
open Solver
open FsUnit
open System.Linq

module SolverTests =

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
  let ``Roots``() =
    let coefficients = Array.create 3 <| complex 1.0
    let polynomial = Polynomial coefficients
    let roots = solve polynomial

    roots |> should haveLength 2
