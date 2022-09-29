//https://www.nuget.org/packages/Newtonsoft.Json/13.0.2-beta2#versions-body-tab
//
//PM > NuGet\Install-Package Newtonsoft.Json -Version 13.0.1
// or
//dotnet add package Newtonsoft.Json --version 13.0.1

open Newtonsoft.Json
open System

//Serialize JSON
(*
Product product = new Product();
product.Name = "Apple";
product.Expiry = new DateTime(2008, 12, 28);
product.Sizes = new string[] { "Small" };

string json = JsonConvert.SerializeObject(product);
// {
//   "Name": "Apple",
//   "Expiry": "2008-12-28T00:00:00",
//   "Sizes": [
//     "Small"
//   ]
// }
*)

let data = Map [
    ("Name", "Apple"); ("Expiry", DateTime(2008, 12, 28).ToString()); ("Size", "Small")
]
JsonConvert.SerializeObject(data)|>printfn "%A"//mapだから順不同で表示される


//let typeArray = [|1;2;3;4|]
//let typeList = [1;2;3;4]
type Product =
    {
        Name: string
        Expiry: DateTime
        Sizes: string [] //一応arrayで
    }
let product = {Name= "Apple"; Expiry= DateTime(2008, 12, 28); Sizes= [|"Small";"Large"|] }
product|>JsonConvert.SerializeObject|>printfn "%A"


//Deserialize JSON
(*
string json = @"{
  'Name': 'Bad Boys',
  'ReleaseDate': '1995-4-7T00:00:00',
  'Genres': [
    'Action',
    'Comedy'
  ]
}";

Movie m = JsonConvert.DeserializeObject<Movie>(json);

string name = m.Name;
// Bad Boys
*)

let json = 
    @"{
      'Name': 'Bad Boys',
      'ReleaseDate': '1995-4-7T00:00:00',
      'Genres': [
        'Action',
        'Comedy'
      ]
    }"


//判別共用体パターン(Discriminated Union Patterns)
type Genres = 
    Action
    | Comedy

//レコード    
type Movie = {
    Name: string
    ReleaseDate: string
    //Genres: Genres list
    Genres: string list
}


let m: Movie = JsonConvert.DeserializeObject<Movie> json
printfn "%A" m

//LINQ to JSON
(*
JArray array = new JArray();
array.Add("Manual text");
array.Add(new DateTime(2000, 5, 23));

JObject o = new JObject();
o["MyArray"] = array;

string json = o.ToString();
// {
//   "MyArray": [
//     "Manual text",
//     "2000-05-23T00:00:00"
//   ]
// }
*)
