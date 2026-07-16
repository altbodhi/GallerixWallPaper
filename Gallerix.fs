module GallerixWallPaper.Gallerix
//#r "nuget: FSharp.Data"
open FSharp.Data

type PicOfDay =
    { day: string
      author: string
      src: string
      title: string }

let fmtDay (s: string) =
    s.Trim().Split('.')
    |> Array.rev
    |> String.concat "-"

let parseNode (n: HtmlNode) =
    let day: string =
        n.CssSelect(".card-block.sui")
        |> List.map (fun x -> x.InnerText() |> fmtDay)
        |> List.head
    let author = n.CssSelect(".card-block > p.card-text") |> List.head |> _.InnerText().Trim()
    let img =
        n.CssSelect(".card-block > a > img")
        |> List.map (fun (x: HtmlNode) -> (x.AttributeValue("src"), x.AttributeValue("alt")))
        |> List.head

    { day = day
      author = author
      src = fst img |> sprintf "https:%s"
      title = snd img }

let getDays () =
    async {

        let! page = HtmlDocument.AsyncLoad "https://in.gallerix.ru/days/"

        let cards = page.CssSelect("div.row > div.card")

        return cards |> List.map parseNode
    }

let getDay (d:System.DateTime) =
    let cards = getDays () |> Async.RunSynchronously
    let dt = d.ToString("yyyy-MM-dd")
    cards |> List.tryFind (fun x -> x.day = dt)

    