namespace RootFinder.Tests

open NUnit.Framework
open RootFinder
open Complex
open System

module ComplexTests =

  let precision = 3
  let tolerance = 1e-3
  let equal = equalWithin tolerance

  [<Test>]
  let ``Complex Addition Test`` () =

    let sum = Complex (2.0, 2.0) + Complex (3.2, -1.1)
    let expectedSum = Complex (5.2, 0.9)

    equal sum expectedSum |>  Assert.True

  [<Test>]
  let ``Polar Coordinates Test`` () =
    let r = 7.0
    let theta = 6.6
    let z = polar r theta

    let twoPi = 2.0 * Math.PI
    Assert.AreEqual (r, z.norm)
    Assert.AreEqual (theta % twoPi, z.phase)

  [<Test>]
  let ``Complex Multiplication Test`` () =
    let r1 = 5
    let theta1 = 4.3
    let z1 = polar r1 theta1

    let r2 = 3
    let theta2 = -2.3
    let z2 = polar r2 theta2

    let polar_product = polar (r1 * r2) (theta1 + theta2)
    let regular_product = z1 * z2

    equal polar_product regular_product |> Assert.True
