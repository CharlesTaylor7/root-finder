#load "Complex.fs"
#load "Polynomial.fs"
#load "Solver.fs"

open RootFinder
open Complex
open Polynomial
open Solver

//// 1 + x^2
//// expected root is +/- i
//let p_final = Polynomial [| Complex.one; Complex.zero; Complex.one |]
//
//// -1
//let p_start = Polynomial [| -Complex.one; Complex.one |]
//let path = path Complex.one p_start p_final

// -1 + x^2
//let p = [| -Complex.one; Complex.zero; Complex.one |] |> Polynomial
//// 2x
//let d = p.derivative
//
//// 0.5*x - 0.5 / x
//let expected_quotient = (0.5, -0.5)
//
////let newton z = z - p.eval(z) / d.eval(z)
//
////let p = Polynomial [| Complex.one; Complex.zero; 2 * Complex.one |]
////let d = p.derivative
//
//let results = divideByDerivative p


