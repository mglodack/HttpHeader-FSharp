#r @"._fake\packages\FAKE\tools\FakeLib.dll"

open Fake
open Fake.Testing.XUnit2

Target "Clean" (fun _ -> printfn "Clean output here.")

Target "Test" (fun _ ->
  !! (@"**\*\bin\**\*Tests.dll")
  |> xUnit2 id
)

Target "MSBuild" (fun _ ->
  !! ("*.sln")
  |> MSBuildRelease "bin" "Build"
  |> ignore
)

Target "CI" id

"CI" <== [ "Test"; "MSBuild" ]

RunTargetOrDefault "Hello"
