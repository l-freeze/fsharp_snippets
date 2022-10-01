//https://fslab.org/Deedle/index.html
//https://fslab.org/Deedle/tutorial.html

//Deedle install
//PM > NuGet\Install-Package Deedle -Version 2.5.0
// or
//dotnet add package Deedle --version 2.5.0

//Charting install（oxyplotと言うのが似てるっぽいがまずはサンプル通りcharting）
//PM > NuGet\Install-Package FSharp.Charting -Version 2.1.0
// or
//dotnet add package FSharp.Charting --version 2.1.0

//PM > NuGet\Install-Package OxyPlot.Core -Version 2.1.0 （pdf使ってみる：NuGet\Install-Package OxyPlot.Pdf -Version 2.1.0）
// or
//dotnet add package OxyPlot.Core --version 2.1.0

// CSV source: e-Stat （2022/9）

open System
open Deedle
//open FSharp.Charting (「System.IO.FileNotFoundException: 'Could not load file or assembly 'System.Windows.Forms.DataVisualization, 」で使えない)

(*
let c01FilePath = "c01.csv"
let c01 = 
    c01FilePath 
    |> Frame.ReadCsv
*)

// Create from sequence of keys and sequence of values
let dates  = 
  [ DateTime(2013,1,1); 
    DateTime(2013,1,4); 
    DateTime(2013,1,8) ]
let values = 
  [ 10.0; 20.0; 30.0 ]
let first = Series(dates, values)

// Create from a single list of observations
Series.ofObservations
  [ DateTime(2013,1,1) => 10.0
    DateTime(2013,1,4) => 20.0
    DateTime(2013,1,8) => 30.0 ] |> printfn "%A"

// Shorter alternative to 'Series.ofObservations'
series [ 1 => 1.0; 2 => 2.0 ] |> printfn "%A"

// Create series with implicit (ordinal) keys
Series.ofValues [ 10.0; 20.0; 30.0 ] |> printfn "%A"

/// Generate date range from 'first' with 'count' days
let dateRange (first:System.DateTime) count = 
    seq {
        //for i in 1..(count-1) do
        //    first.AddDays (float i)
        for i in 1..(count-1) -> first.AddDays (i|>float)
    }

/// Generate 'count' number of random doubles
let rand count = 
    let rnd = Random()
    seq{
        for i in 1..(count-1) -> rnd.NextDouble()
    }


// A series with values for 10 days 
let second = Series(dateRange (DateTime(2013,1,1)) 10, rand 10)
printfn "%A" second
printfn "second.Print"
()|>second.Print


let df1 = Frame(["first"; "second"], [first; second])
df1.Print ()


let df2 = 
    [
    "first" => first
    "second" => second
    ]|> Frame.ofColumns
printfn "df2.Print"
df2.Print ()




"処理終了" |> printfn "%s"
