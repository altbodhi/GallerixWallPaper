open GallerixWallPaper

Logger.init "gallerix.log"
Logger.log $"Load image"
let out = Utils.makeDir "gallerix"
let days = Gallerix.getDays () |> Async.RunSynchronously |> List.sortByDescending _.day
let last = days |> List.head

if Utils.isNew(last.src, out) then
    Logger.log (Utils.toJson last)
    match Utils.loadImage (last.src, out) with
    | Ok f ->  
        Utils.setWallpaper f |> fun c -> Logger.log( sprintf "%s set with code %d" f c)
    | Error e ->
        Logger.log e
else
    Logger.log (last.author + " "  + last.title + " already proccessed.")
