namespace RootFinder

open System
open System.Linq
open System.Collections.Generic
open Utilities

[<NoComparison>]
[<CustomEquality>]
type MultiSet<'a when 'a :> IEquatable<'a> and 'a : equality> =
  struct
    val groups: ('a * int) collection
    new (items: 'a seq) = { groups = group_by id items |> Seq.map (fun struct (x, g) -> (x, g.Count)) |> Seq.toArray }
  end

  override s.ToString() =
    sprintf "%A" s.groups

  member inline s.NumberDistinct =
    s.groups.Count

  interface IEquatable<'a MultiSet> with
    member s.Equals t =
       s.NumberDistinct = t.NumberDistinct &&
       Seq.forall (fun (x, c1: int) -> Seq.exists (fun (y, c2: int) -> x = y && c1 = c2) t.groups) s.groups

[<AutoOpen>]
module MultiSet =

  let inline multiset items = MultiSet(items)
