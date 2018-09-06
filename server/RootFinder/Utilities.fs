namespace RootFinder

open System
open System.Linq
open System.Collections.Generic

module Utilities =

  type 'a collection = 'a IReadOnlyCollection

  let inline group_by<'a, 'b when 'b :> IEquatable<'b>> (f: 'a -> 'b) (xs: 'a seq) : struct ('b * List<'a>) collection =
    let groups = List<struct ('b * List<'a>)>();
    for x in xs do
      let x_key = f x
      match Seq.tryFind (fun struct (key, _) -> x_key = key) groups with
      | Some struct (_, g) -> g.Add(x)
      | None ->
        let list = List()
        list.Add(x)
        groups.Add(struct (x_key, list))

    groups :> struct ('b * List<'a>) collection

  // Enumerable.SequenceEqual doesn't enforce any equality constraint.
  let inline sequence_equal<'a when 'a :> IEquatable<'a>> (x: 'a seq) (y: 'a seq) =
    Enumerable.SequenceEqual (x, y)
