namespace RootFinder

open System

type Polynomial =
  struct
    val public coefficients: array<Complex>

    new (coefficients) = Polynomial(coefficients)

    member p.degree =
      p.coefficients.Length - 1

    member p.Item(index) =
      p.coefficients.[index]
  end

  static member zero = Polynomial <| Array.zeroCreate 1

   // Scalar operations
  static member (*) (s: double, p: Polynomial) =
    Polynomial <| Array.map (fun z -> s * z) p.coefficients

  static member (/) (p: Polynomial, s: double) =
    Polynomial <| Array.map (fun z -> z / s) p.coefficients

  // Ring operations
  static member (+) (p1: Polynomial, p2: Polynomial) =
    let max = Math.Max(p1.degree, p2.degree)
    let min = Math.Min(p1.degree, p2.degree)
    let array = Array.zeroCreate (max + 1)
    for i = 0 to min do
      array.[i] <- p1.[i] + p2.[i]
    Polynomial array

module Polynomial =

  type Remainder = Complex

  let (/) (p1: Polynomial, p2: Polynomial) = (Polynomial(), Complex.zero)


