namespace RootFinder

#nowarn "86"
open System
open Microsoft.FSharp.Core

// Copied from: https://zeckul.wordpress.com/2015/07/09/how-to-avoid-boxing-value-types-in-f-equality-comparisons/
[<AutoOpen>]
module RebindEquals =
    let inline eq<'a when 'a :> IEquatable<'a>> (x: 'a) (y: 'a) = x.Equals y
    let inline (=) x y = eq x y
    let inline (<>) x y = not (eq x y)

    let inline (=@) x y = Operators.(=) x y
    let inline (<>@) x y = Operators.(<>) x y
