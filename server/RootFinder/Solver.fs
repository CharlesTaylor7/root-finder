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

  type IterationCount = int

  let newton_solve (p: Polynomial) guess : Complex * IterationCount =
    let d = p.derivative
    let f = safe_eval(p / p.derivative)
    let newton z =
      z - (f z)

    let tolerance = 1e-15

    let rec iterate z i =
      let next = newton z
      if equal_within tolerance z next
      then (next, i)
      else iterate next (i + 1)

    iterate guess 1

  let inline interpolate t (p1: Polynomial) (p2: Polynomial) =
    (1.0 - t) * p1 + t * p2

  let step_count = 10

  open System
  let random = new Random()

  let solve_with_counts (p: Polynomial) : (Complex * IterationCount) array  =
    let n = p.degree
    let phase = random.NextDouble()
    let (p_i, roots) = initial_polynomial_and_roots n phase
    let p_f = p

    let inline step i =
      float i / float step_count

    let reducer (z: Complex, n: IterationCount) (i: int) =
      let t = step i
      let p = interpolate t p_i p_f
      let dt = step 1

      let dz_dt =
        let numerator = p_i - p_f
        let denominator = (1.0 - t) * p_i.derivative + t * p_f.derivative
        safe_eval (numerator / denominator) z

      let z_prime = z + dt * dz_dt
      let (z_next, count) = newton_solve p z_prime
      (z_next, count + n)

    let steps = seq { 1 .. step_count }

    let trace root = Seq.fold reducer root steps
    Array.map ((fun r -> (r, 0)) >> trace) roots

  let solve (p: Polynomial) : Complex array =
    solve_with_counts p |> Array.map fst
