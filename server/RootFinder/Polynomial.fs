namespace RootFinder

open System

type Polynomial =
  struct
    val coefficients: array<Complex>
    new (coefficients) = { coefficients = coefficients}
  end

  member inline p.degree =
    p.coefficients.Length - 1

  member inline p.Item(index) =
    p.coefficients.[index]

  member p.derivative =
    let degree = p.degree
    let array = Array.zeroCreate degree
    for i = 0 to degree - 1 do
      array.[i] <- (i + 1) * p.[i + 1]
    Polynomial array

  // Horner's Rule
  member p.eval(z: Complex) =
    let mutable total = Complex.zero
    for i = p.degree downto 0 do
      total <- p.[i] + z * total
    total

  static member (*) (s: double, p: Polynomial) =
    Polynomial <| Array.map (fun z -> s * z) p.coefficients

  static member (/) (p: Polynomial, s: double) =
    Polynomial <| Array.map (fun z -> z / s) p.coefficients

  static member (+) (p1: Polynomial, p2: Polynomial) =
    let max = Math.Max(p1.degree, p2.degree)
    let min = Math.Min(p1.degree, p2.degree)
    let array = Array.zeroCreate (max + 1)
    for i = 0 to min do
      array.[i] <- p1.[i] + p2.[i]
    Polynomial array

[<AutoOpen>]
module Polynomial =
  let inline poly seq =
    Seq.map complex seq |> Seq.toArray |> Polynomial
