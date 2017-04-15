#r @"._fake\packages\FAKE\tools\FakeLib.dll"

open Fake

Target "Hello" (fun _ -> printfn "Hello World")

RunTargetOrDefault "Hello"
