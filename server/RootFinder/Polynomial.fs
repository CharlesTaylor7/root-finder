namespace RootFinder

open System
open Complex
open Utilities
open System.Linq

[<NoComparison>]
[<CustomEquality>]
type Polynomial =
  struct
    val coefficients: Complex array
    new (coefficients) = { coefficients = coefficients}
  end

  interface IEquatable<Polynomial> with
    member p.Equals q =
      sequence_equal p.coefficients q.coefficients

  override p.ToString() =
    let format_monomial i =
      match i with
      | 0 -> ""
      | 1 -> " * x"
      | n -> String.Format(" * x^{0}", n)

    let formatted =
      String.Join(" + " , p.coefficients
        .Select(fun z n -> struct (z, n))
        .Where(fun struct (z, _) -> z <> Complex.zero)
        .Select(fun struct (z, n) -> "(" + z.ToString() + ")" + format_monomial n))

    if formatted = ""
    then "0"
    else formatted

  member inline p.degree =
    p.coefficients.Length - 1

  member inline p.Item(index) =
    if index > p.degree
    then Complex.zero
    else p.coefficients.[index]

  member p.derivative =
    let degree = p.degree
    let array = Array.zeroCreate degree
    for i = 0 to degree - 1 do
      array.[i] <- (i + 1) * p.[i + 1]
    Polynomial array

  member p.leadCoefficient =
    p.coefficients.[p.degree]

  // Horner's Rule
  member p.eval(z: Complex) =
    let mutable total = Complex.zero
    for i = p.degree downto 0 do
      total <- p.[i] + z * total
    total

  static member (*) (s: Complex, p: Polynomial) =
    Polynomial <| Array.map (fun z -> s * z) p.coefficients

  static member (*) (s: decimal, p: Polynomial) =
    Polynomial <| Array.map (fun z -> s * z) p.coefficients

  static member (/) (p: Polynomial, s: decimal) =
    Polynomial <| Array.map (fun z -> z / s) p.coefficients

  static member (+) (p1: Polynomial, p2: Polynomial) =
    let max = Math.Max(p1.degree, p2.degree)
    let array = Array.zeroCreate (max + 1)
    for i = 0 to max do
      array.[i] <- p1.[i] + p2.[i]
    Polynomial array

  static member (-) (p1: Polynomial, p2: Polynomial) =
      let max = Math.Max(p1.degree, p2.degree)
      let array = Array.zeroCreate (max + 1)
      for i = 0 to max do
        array.[i] <- p1.[i] - p2.[i]
      Polynomial array

  static member (*) (p: Polynomial, q: Polynomial) =
    let array = Array.zeroCreate (p.degree + q.degree + 1)
    for i = 0 to array.Length - 1 do
      for j = 0 to i do
        array.[i] <- array.[i] + p.[j] * q.[i - j]
    Polynomial array

  static member (/) (p: Polynomial, d: Polynomial): Polynomial * Polynomial =
    let monomial (c: Complex) (n: int) =
      if n < 0
      then failwith "Degree of a monomial should not be negative."
      else
        let array = Array.zeroCreate (n+1)
        array.[n] <- c
        Polynomial array

    let leadCoefficient (p: Polynomial) =
      Array.last p.coefficients

    let minusLeadTerm (p: Polynomial) =
      let array = Array.zeroCreate p.degree
      for i = 0 to p.degree - 1 do
        array.[i] <- p.[i]
      Polynomial array

    let b = p.derivative
    let c = leadCoefficient b

    let mutable q = Polynomial [| Complex.zero |]
    let mutable r = p

    while r.degree >= b.degree do
      let s = monomial (leadCoefficient r / c) (r.degree - b.degree)
      q <- q + s
      r <- (r - (s * b)) |> minusLeadTerm
    (q, r)

module Polynomial =
  let inline poly seq =
    Seq.map complex seq |> Seq.toArray |> Polynomial
