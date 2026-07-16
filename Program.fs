open GallerixWallPaper

Logger.init "gallerix.log"
"start with [" + System.String.Join(",", Utils.args()) + "]" |> Logger.log
let out = Utils.makeDir "gallerix"
let days = Gallerix.getDays () |> Async.RunSynchronously |> List.sortByDescending _.day
let last = days |> List.head
let apply (last: Gallerix.PicOfDay,out) =
    Logger.log (Utils.toJson last)
    match Utils.loadImage (last.src, out) with
    | Ok f ->  
        Utils.setWallpaper f |> fun c -> Logger.log( sprintf "%s set with code %d" f c)
    | Error e ->
        Logger.log e
let rec rollback (xs: Gallerix.PicOfDay list) = 
    match xs with
    | h :: t ->
        let src = h.src
        if Utils.isNew(src, out) then apply (h,out) else rollback t
    | [] -> Logger.log ("no image for rollback")

if Utils.isRollback() 
then rollback days 
elif Utils.isNew(last.src, out) then
        apply(last,out)
    else
        Logger.log (last.author + " "  + last.title + " already proccessed.")
