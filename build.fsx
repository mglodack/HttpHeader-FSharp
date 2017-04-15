#r @"._fake\packages\FAKE\tools\FakeLib.dll"

open Fake
open Fake.Testing.XUnit2

Target "Hello" (fun _ -> printfn "Hello World")

Target "Test" (fun _ ->
  !! (@"**\*\bin\**\*Tests.dll")
  |> xUnit2 id
)

RunTargetOrDefault "Hello"
