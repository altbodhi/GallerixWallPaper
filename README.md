# GallerixWallPaper
Wallpaper changer for windows from Gallerix last pic of day

## setup steps:
1. dotnet build -c release (in work dir)
2. copy bin\release\net10.0\ to app dir (for example to %localappdata%\Programs\gallerix)
3. press ctrl+r and type shell:startup. in opened folder create shortcut for GallerixWallPaper.exe
4. restart-computer (in pwsh)

## changes:
    **2026-07-16:**
        1. add rollback option - attempt load last of not loaded images (only news by default)
           using: `dotnet run -- rollback` or `GallerixWallPaper.exe rollback`