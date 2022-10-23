open System

let cancelEventHandler (sender:Object) (args:ConsoleCancelEventArgs) = 
    ()|>Console.Clear
    $"key pressd: {args.SpecialKey}"|>printfn "%A"
    $"key cancel: {args.Cancel}"|>printfn "%A"
    args.Cancel <- true
    $"key cancel: {args.Cancel}"|>printfn "%A"
    ()
    

[<EntryPoint>]
let main args =

    AppDomain.CurrentDomain.ProcessExit.Add(fun _ -> printfn "exitする")//終了時の処理

    args|>printfn "args: %A"
    ()|>Console.Clear

    //cancel event handler
    Console.CancelKeyPress.AddHandler( new ConsoleCancelEventHandler(cancelEventHandler)) //ctrl+cのイベントハンドラー

    //while - break の概念はない
    let rec inputLoop str = 
        let input = System.Console.ReadLine()
        ()|>Console.Clear
        (str,input)||>printfn "%s%s"
        if input <> ":q" then
            str+input|>inputLoop
        else
            ()|>Console.Clear
            printfn "exit"

    ""|>inputLoop

    //read key loop
    printfn "Escapeで終了します"
    let rec readKeyLoop ()=
        let key = Console.ReadKey().Key
        printfn $"押したキーは：{key}"
        match key with
            | ConsoleKey.Enter -> readKeyLoop()
            | ConsoleKey.UpArrow | ConsoleKey.DownArrow 
            | ConsoleKey.LeftArrow | ConsoleKey.RightArrow -> 
                printfn "上下左右"
                readKeyLoop()
            | ConsoleKey.Escape -> ()
            | _ -> readKeyLoop()

    ()|>readKeyLoop         

    0