namespace RootFinder

open System

[<AutoOpen>]
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

  let newton_solve (p: Polynomial) guess delta =
    let d = p.derivative
    let newton z = z - p.eval(z) / d.eval(z)
    let rec iterate z =
      let next = newton z
      if (z - next).normSquared > delta
      then iterate next
      else next
    iterate guess

  let inline interpolate (p1: Polynomial) (p2: Polynomial) t =
    (1.0 - t) * p1 + t * p2

  let solve (p: Polynomial) =
    let n = p.coefficients.Length
    let roots = roots_of_unity n |> Seq.toArray
    let p_start = polynomial_of_unity n
    let p_final = p
    let step_count = 10
    let delta = 1e-7

    let inline step i =
      double (i + 1) / double step_count

    let reducer (current: Complex) (i: int) =
      let t = step i
      let p = interpolate p_start p_final t
      newton_solve p current delta

    let steps = seq { 1 .. step_count }

    Array.map (fun root -> Seq.fold reducer root steps) roots
