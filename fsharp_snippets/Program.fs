//Install-Package Giraffe
open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

IO.Directory.GetCurrentDirectory () |> printfn "%A"

__SOURCE_DIRECTORY__ |> printfn "%A"
__SOURCE_FILE__ |> printfn "%A"
__LINE__ |> printfn "%A"


let documentRoot = [|__SOURCE_DIRECTORY__; "web"; "public"|] |> IO.Path.Combine 
documentRoot |> printfn "documentRoot: %s"
IO.Path.Combine (documentRoot, "index.html") |> printfn "index -> %A"


let webApp =
    choose [
        route "/ping"   >=> text "pong" //http://localhost:5000/ping
        route "/"       >=> htmlFile (IO.Path.Combine (documentRoot, "index.html")) ] //http://localhost:5000

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    |> ignore)
        .Build()
        .Run()
    0