namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Polynomial
open Complex
open Solver

module SolverTests =

  [<Test>]
  let ``Test Divide By Derivative``() =
    // -1 + x^2
    let p = [| -Complex.one; Complex.zero; Complex.one |] |> Polynomial
    // 2x
    let d = p.derivative

    // 0.5*x - 0.5 / x
    let expected_quotient = complex 0.5
    let expected_remainder = complex -0.5

    let (quotient, remainder) = divideByDerivative p
    let equal = equalWithin 0.01
    Assert.AreEqual(quotient.coefficients.Length, 2)
    Assert.True(equal quotient.[0] Complex.zero)
    Assert.True(equal quotient.[1] expected_quotient)

    Assert.AreEqual(remainder.coefficients.Length, 1)
    Assert.True(equal remainder.[0] expected_remainder)

  [<Test>]
  let ``Roots``() =
    let coefficients = Array.create 3 <| complex 1.0
    let polynomial = Polynomial coefficients
    let roots = solve polynomial

    Assert.True(roots.Length = 2)
