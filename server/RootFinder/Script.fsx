#load "Complex.fs"
#load "Polynomial.fs"
#load "Solver.fs"

open RootFinder
open Complex
open Polynomial
open Solver

let polynomial = Polynomial [| 3 +| 2; 2 +| 0; |]

//solve polynomial
