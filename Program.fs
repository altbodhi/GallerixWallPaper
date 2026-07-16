open GallerixWallPaper

Logger.init "gallerix.log"

Logger.log (
    "start with ["
    + System.String.Join(", ", Utils.args ())
    + "]"
)

MainCore.execute ()
