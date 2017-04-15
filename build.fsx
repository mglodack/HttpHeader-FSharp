#r @"._fake\packages\FAKE\tools\FakeLib.dll"

open Fake
open Fake.Testing.XUnit2

let private msbuildArtifacts = !! "src/**/bin/**/*.*" ++ "src/**/obj/**/*.*"
let private testAssemblies = !! "tests/**/*.Tests.dll" -- "**/obj/**/*.Tests.dll"
let private solutionFile = "HttpHeader.sln"

Target "RestorePackages" (fun _ ->
  solutionFile
  |> RestoreMSSolutionPackages (fun p ->
      { p with
          OutputPath = "packages"
      })
)

Target "Clean" (fun _ ->
  DeleteFiles msbuildArtifacts

  [ "bin"; "packages"; ]
  |> Seq.iter CleanDir
)

Target "Test" (fun _ ->
  testAssemblies
  |> xUnit2 id
)

Target "MSBuild" (fun _ ->
  [ solutionFile ]
  |> MSBuildRelease "bin" "Build"
  |> ignore
)

Target "CI" id

"CI" <== [ "Clean"; "RestorePackages"; "MSBuild"; "Test"]

RunTargetOrDefault "CI"
