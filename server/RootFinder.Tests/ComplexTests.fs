namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Complex
open System
open FsUnit
open FsUnitTyped

module ComplexTests =

  [<Test>]
  let ``Complex Addition`` () =

    let sum = Complex (2.0, 2.0) + Complex (3.2, -1.1)
    let expected_sum = Complex (5.2, 0.9)

    sum |> shouldEqual expected_sum

  [<Test>]
  let ``Polar Coordinates`` () =
    let r = 7.0
    let theta = 6.6
    let z = polar r theta

    let twoPi = 2.0 * Math.PI
    z.norm |> should (equalWithin 1e-7) r
    z.phase |> should (equalWithin 1e-7) (theta % twoPi)

  [<Test>]
  let ``Complex Multiplication`` () =
    let r1 = 5
    let theta1 = 4.3
    let z1 = polar r1 theta1

    let r2 = 3
    let theta2 = -2.3
    let z2 = polar r2 theta2

    let polar_product = polar (r1 * r2) (theta1 + theta2)
    let regular_product = z1 * z2

    regular_product |> shouldEqual polar_product
