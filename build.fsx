#I @"packages/FAKE/tools"
#I @"packages/FAKE.BuildLib/lib/net451"
#r "FakeLib.dll"
#r "BuildLib.dll"

open Fake
open BuildLib

let solution = initSolution "" "" []

Target "Clean" <| fun _ -> cleanBin

Target "PackUnity" <| fun _ ->
    packUnityPackage "./src/UnityPackage/ThaiFontAdjuster.unitypackage.json"

Target "Pack" <| fun _ -> ()

Target "Help" <| fun _ -> 
    showUsage solution (fun _ -> None)

"Clean"
  ==> "PackUnity"
  ==> "Pack"

RunTargetOrDefault "Help"
