open System
//open System.IO
open Deedle


let (+/) path1 path2 = IO.Path.Combine(path1, path2)

[<EntryPoint>]
let main argv = 
    //ただのテキストファイル読み込みサンプル（sjis無理だった）
    let csvText = 
        let hasHeader = true
        let filepath = "F:\\dev" +/ "fsharp" +/ "fsharp_snippets" +/ "fsharp_snippets" +/ "data" +/ "c01.csv"
        //let iso2022jp01 = 50220
        //let iso2022jp02 = 50222
        //let encoding = Text.Encoding.GetEncoding iso2022jp01
        IO.File.ReadAllText (filepath)
        //IO.File.ReadAllText (filepath, encoding)

    printfn "%A" csvText

    //DeedleによるCSV読み込みサンプル
    let csvDf = 
        let hasHeader = false// default true
        let filepath = "F:\\dev" +/ "fsharp" +/ "fsharp_snippets" +/ "fsharp_snippets" +/ "data" +/ "c01_modified.csv"
        Frame.ReadCsv (filepath)
        //Frame.ReadCsv (filepath, hasHeader)

    //csvDf.Print()
    //csvDf.GetRow(0).Print ()
    csvDf.FilterRowsBy("都道府県コード", 23).Print () //https://fslab.org/Deedle/reference/deedle-framemodule.html
    let mens = csvDf.FilterRowsBy("都道府県コード", 23).GetColumn("人口（男）") //https://fslab.org/Deedle/reference/deedle-framemodule.html
    mens.Print ()
    mens.Sum() |> printfn "男 Sum: %A"
    mens.Min() |> printfn "男 Min: %A"
    mens.Max() |> printfn "男 Max: %A"


    "処理終了"|>printfn "%A"
    0