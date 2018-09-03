namespace RootFinder.Tests

open FsCheck
open RootFinder
open System.Linq

type Generators =
    static member Complex() = {
      new Arbitrary<Complex>() with
        override x.Generator =
          gen {
            let! x = Gen.choose(-50, 50)
            let! y = Gen.choose(-50, 50)
            return x +| y
          }
        override x.Shrinker t = Seq.empty
    }

    static member Polynomial() = {
      new Arbitrary<Polynomial>() with
        override x.Generator =
          gen {
            let! degree = Gen.choose(1, 10)
            let c = Generators.Complex().Generator
            let! sequence = Enumerable.Repeat(c, degree + 1) |> Gen.sequenceToSeq
            return sequence |> Seq.toArray |> Polynomial
          }
        override x.Shrinker t = Seq.empty
    }
