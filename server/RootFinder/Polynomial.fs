namespace RootFinder

open System

type Polynomial(coefficients: array<Complex>) =
  member __.degree = coefficients.Length - 1
