class MyProgram{
    static async Task Main(string[] args){
        if(args.Length==0){
            Console.WriteLine("Введите имя файла!");
            Environment.Exit(0);
        }
        Console.WriteLine("Добро Пожаловать в мой текстовый редактор!");
        Console.WriteLine("");
        string? path = args[0];
        Console.Write(">> ");
        string? text = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(path, true)){
            await writer.WriteLineAsync(text);
        }
    }
}