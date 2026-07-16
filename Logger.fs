module GallerixWallPaper.Logger

open System
open System.Diagnostics

let init (fileName: string) =
    Trace.Listeners.Add(new ConsoleTraceListener())
    |> ignore

    Trace.Listeners.Add(new TextWriterTraceListener(fileName, "gallerix"))
    |> ignore

    Trace.AutoFlush <- true

let log msg =
    let info =
        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        + ": "
        + msg

    Trace.WriteLine info
