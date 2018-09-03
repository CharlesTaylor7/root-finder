#load "RebindEquals.fs"
#load "Utilities.fs"
#load "Complex.fs"
#load "Polynomial.fs"
#load "Solver.fs"

open RootFinder
open RebindEquals
open Complex
open Polynomial
open Solver
open System

let roots = roots_of_unity 10
let p = polynomial_of_unity 10

let one = Polynomial [| Complex.one |]
let product_of_linear_terms = Seq.fold (fun product root -> product * (Polynomial [| -root; Complex.one |])) one roots
