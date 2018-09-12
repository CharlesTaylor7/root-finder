namespace RootFinder.Tests

open NUnit.Framework
open Amazon.Lambda.Serialization.Json
open System.IO
open RootFinder
open FsUnitTyped

module SerializerTests =

  [<Test>]
  let ``Serialize & Deserialize Complex Number`` () =
    use stream = new MemoryStream()
    let serializer = new JsonSerializer()

    let z = 3.0 +| -2
    serializer.Serialize(z, stream)

    let deserialized_z = serializer.Deserialize(stream)

    z |> shouldEqual deserialized_z
