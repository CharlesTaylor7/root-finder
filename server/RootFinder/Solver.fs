namespace RootFinder

open System

module Solver =

  let roots_of_unity n =
    let num = 2.0 * Math.PI
    let denom = double n
    seq { for i in 1 .. n -> num * double i / denom }

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

  let interpolate (p1: Polynomial) (p2: Polynomial) t = (1.0 - t) * p1 + t * p2

  let solve (p: Polynomial) =
    let n = p.coefficients.Length
    let roots = roots_of_unity n
    let p_start = polynomial_of_unity(n)
    let p_final = p
    let step_count = 10
    let delta = 0.01

    let step (i: int) = double (i + 1) / double step_count

    let reducer (current: Complex) (i: int) =
      let t = step i
      let p = interpolate p_start p_final t
      newton_solve p current delta

    let steps = seq { 1 .. step_count }

    Array.map <| Seq.fold reducer
