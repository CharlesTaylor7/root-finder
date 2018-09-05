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
  let ``Initial roots of initial polynomial`` () =
    let phase = 2.1
    let degree = 3
    let (p, roots) = initial_polynomial_and_roots degree phase
    Array.forall (fun r -> p.eval(r) = Complex.zero) roots |> shouldEqual true

  [<Test>]
  let ``Divide By Derivative: x^2 - 1``() =
    // -1 + x^2
    let p = [| -Complex.one; Complex.zero; Complex.one |] |> Polynomial
    // derivative = 2x

    // 0.5*x - 0.5 / x
    let expected_quotient = [| Complex.zero; complex 0.5 |] |> Polynomial
    let expected_remainder = [| complex -0.5 |] |> Polynomial

    let (quotient, remainder) = p / p.derivative

    quotient * p.derivative + remainder |> shouldEqual p

  [<Test>]
  let ``Divide By Derivative: x ^ 2``() =

    let p = monomial Complex.one 2

    // 0.5*x
    let expected_quotient = [| Complex.zero; complex 0.5 |] |> Polynomial
    let expected_remainder = [| Complex.zero |] |> Polynomial

    let (quotient, remainder) = p / p.derivative

    quotient |> shouldEqual expected_quotient
    remainder |> shouldEqual expected_remainder

  [<Test>]
  let ``Solve: x^2 + x + 1``() =

    // Roots are primitive 3rd roots of -1.
    let coefficients = Array.create 3 <| complex 1.0
    let polynomial = Polynomial coefficients
    let roots = solve polynomial

    let angle = 2.0 * Math.PI / 3.0;
    let expected_roots = [| polar 1 angle ; polar 1 (-angle) |]

    multiset roots |> shouldEqual (multiset expected_roots)

  [<Test>]
  let ``Solve: x^2 + 1``() =

    // x^2 + 1
    let polynomial = [| Complex.one; Complex.zero; Complex.one |] |> Polynomial
    let roots = solve polynomial

    let expected_roots = [| 0 +| 1; 0 +| -1 |]

    multiset roots |> shouldEqual (multiset expected_roots)

  [<Test>]
  let ``Solve: x^3``() =

    // x^3.
    let polynomial = monomial Complex.one 3
    let roots = solve polynomial

    let expected_roots = Array.zeroCreate 3

    roots |> shouldEqual expected_roots

// Tests fail because polynomials are very ill conditioned
// Need to read up on pre conditioning
  [<Test>]
  let ``Solve: x^10``() =

    // x^10.
    let polynomial = monomial Complex.one 10
    let roots = solve polynomial

    let expected_roots = Array.zeroCreate 10

    roots |> shouldEqual expected_roots

  [<Test>]
  let ``Solve: Wilkinson's Polynomial``() =

    let terms = seq {
      for i in 1..20 do
      yield Polynomial [| -i +| 0; 1 +| 0 |]
    }

    let product = Seq.fold (fun p q -> p * q) Polynomial.one terms

    printfn "%A" product
    let roots = solve product

    roots |> shouldEqual (seq {1..20} |> Seq.map complex |> Seq.toArray)
