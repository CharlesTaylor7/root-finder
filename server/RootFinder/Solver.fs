namespace RootFinder

open System
open Complex
open Polynomial

module Solver =

  let roots_of_unity n =
    let num = double <| 2.0 * Math.PI
    let denom = double n
    seq {
      for i in 1 .. n do
      let angle = num * double i / denom
      yield polar 1 angle
    }

  let polynomial_of_unity n =
    let array = Array.zeroCreate (n + 1)
    array.[n] <- Complex.one
    array.[0] <- -Complex.one
    Polynomial array

  let initial_polynomial_and_roots n phase =
    let gamma = polar 1 phase
    let roots = roots_of_unity n
    let rotated_roots = Seq.map (fun r -> gamma * r) roots

    let coefficient = polar (-1) (float n * phase)
    let array = Array.zeroCreate (n + 1)
    array.[n] <- Complex.one
    array.[0] <- coefficient
    (Polynomial array, Seq.toArray rotated_roots)

  let newton_solve (p: Polynomial) guess =
    let d = p.derivative
    let (q, r) = p / p.derivative
    let newton z =
      let lastTerm =
        if r.degree = 0 && r.[0] = Complex.zero
        then Complex.zero
        else r.eval(z) / d.eval(z)
      z - (q.eval(z) + lastTerm)

    let tolerance = 0.1m * Complex.default_tolerance

    let rec iterate z =
      let next = newton z
      if equal_within tolerance z next
      then iterate next
      else next
    iterate guess

  let inline interpolate t (p1: Polynomial) (p2: Polynomial) =
    (1.0m - t) * p1 + t * p2

  let step_count = 10

  let path (root: Complex) (p_i: Polynomial) (p_f: Polynomial) : Complex list =
    let p_start = p_i
    let p_final = p_f
    let step_count = 10
    let delta = 1e-3

    let inline step i =
      decimal i / decimal step_count

    let reducer (previous: Complex list) (i: int) =
      let current = List.head previous
      let t = step i
      let p = interpolate t p_start p_final
      let next = newton_solve p current
      next :: previous

    let steps = seq { 1 .. step_count }

    let path = Seq.fold reducer [root] steps
    path

  open System
  let random = new Random()

  let solve (p: Polynomial) =
    let n = p.degree
    let phase = random.NextDouble()
    let (p_start, roots) = initial_polynomial_and_roots n phase
    let p_final = p

    let inline step i =
      decimal i / decimal step_count

    let reducer (current: Complex) (i: int) =
      let t = step i

      let p = interpolate t p_start p_final
      newton_solve p current

    let steps = seq { 1 .. step_count }

    Array.map (fun root -> Seq.fold reducer root steps) roots

  let monomial (c: Complex) (n: int) =
    if n < 0
    then failwith "Degree of a monomial should not be negative."
    else
      let array = Array.zeroCreate (n+1)
      array.[n] <- c
      Polynomial array
