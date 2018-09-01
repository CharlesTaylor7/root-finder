namespace RootFinder

open System
open System.Linq

module Utilities =

  // Enumerable.SequenceEqual doesn't enforce any equality constraint.
  let inline sequence_equal<'a when 'a :> IEquatable<'a>> (x: 'a seq) (y: 'a seq) =
    Enumerable.SequenceEqual (x, y)

