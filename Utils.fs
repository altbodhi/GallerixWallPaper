module GallerixWallPaper.Utils

open FSharp.Data
open System
open System.IO
open System.Runtime.InteropServices

[<DllImport("user32.dll", CharSet = CharSet.Auto)>]
extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni)

let setWallpaper file =
    SystemParametersInfo(20, 0, file, 1 ||| 2)

let makeDir path =
    let p =
        Path.Combine(Environment.CurrentDirectory, path)

    Directory.CreateDirectory(p).FullName

let loadImage (url, storeTo) =
    match (Http.Request url).Body with
    | Binary bytes ->
        let fileName =
            Path.Combine(storeTo, Path.GetFileName(url))

        File.WriteAllBytes(fileName, bytes)
        Ok fileName
    | Text text -> Error text

let toJson x =
    System.Text.Json.JsonSerializer.Serialize x

let isNew (src: string, dir) =
    Path.Combine(dir, Path.GetFileName(src))
    |> File.Exists
    |> not

let isRollback () =
    Environment.GetCommandLineArgs()
    |> Array.contains "rollback"

let args () =
    Environment.GetCommandLineArgs()