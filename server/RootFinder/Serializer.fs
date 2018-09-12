namespace RootFinder

open Amazon.Lambda.Serialization.Json
open Amazon.Lambda.Core
open Froto.Serialization.Serialize

module Serializer =

  type ComplexSerializer () =
    interface ILambdaSerializer with
      member s.Serialize(response, stream) = ()
      member s.Deserialize(stream) = failwith ""
