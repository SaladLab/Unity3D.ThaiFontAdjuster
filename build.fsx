#I @"packages/FAKE/tools"
#I @"packages/FAKE.BuildLib/lib/net451"
#r "FakeLib.dll"
#r "BuildLib.dll"

open Fake
open BuildLib

let solution = initSolution "" "" []

Target "Clean" <| fun _ -> cleanBin

Target "Package" <| fun _ -> buildUnityPackage "./src/UnityPackage"

Target "Help" <| fun _ -> 
    showUsage solution (fun name -> 
        if name = "package" then Some("Build package", "")
        else None)

"Clean"
  ==> "Package"

RunTargetOrDefault "Help"
