namespace RootFinder

open System

type Complex = unit

type Polynomial () =

  static member (*) (s: decimal, p: Polynomial) : Polynomial = failwith ""
  static member (*) (s: Complex, p: Polynomial) : Polynomial = failwith ""
  static member (+) (p: Polynomial, q: Polynomial) : Polynomial = failwith ""

module Solver =

  let interpolate t (p1: Polynomial) (p2: Polynomial) =
    (1.0 - t) * p1 + t * p2

  let p_start: Polynomial = failwith ""
  let p_final: Polynomial = failwith ""
  let t = 0.5
  let p = interpolate t p_start p_final
