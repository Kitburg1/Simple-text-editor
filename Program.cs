class MyProgram{
    static async Task Main(string[] args){
        if(args.Length==0){
            Console.WriteLine("Введите имя файла!");
            Environment.Exit(0);
        }
        Console.WriteLine("Добро Пожаловать в мой текстовый редактор!");
        Console.WriteLine("");
        string? path = args[0];
        int i = (await File.ReadAllLinesAsync(path)).Length + 1;
        while(true){
            Console.Write("{0} >> ", i);
            string? text = Console.ReadLine();
            if (text != null && text.StartsWith("/delete ")) {
                var range = text.Substring(8).Split('-');
                var lines = await File.ReadAllLinesAsync(path);

                if (range.Length == 1 && int.TryParse(range[0], out int singleLine)) {
                    if (singleLine > 0 && singleLine <= lines.Length) {
                        var newLines = lines.Where((line, index) => index != singleLine - 1).ToArray();
                        await File.WriteAllLinesAsync(path, newLines);
                        Console.WriteLine("Строка {0} удалена.", singleLine);
                        i = newLines.Length + 1;
                    } else {
                        Console.WriteLine("Неверный номер строки.");
                    }
                } else if (range.Length == 2 && int.TryParse(range[0], out int startLine) && int.TryParse(range[1], out int endLine)) {
                    if (startLine > 0 && endLine >= startLine && endLine <= lines.Length) {
                        var newLines = lines.Where((line, index) => index < startLine - 1 || index > endLine - 1).ToArray();
                        await File.WriteAllLinesAsync(path, newLines);
                        Console.WriteLine("Строки с {0} по {1} удалены.", startLine, endLine);
                        i = newLines.Length + 1;
                    } else {
                        Console.WriteLine("Неверный диапазон строк.");
                    }
                } else {
                    Console.WriteLine("Неверный формат команды.");
                }
            } else {
                using (StreamWriter writer = new StreamWriter(path, true)){
                    await writer.WriteLineAsync(text);
                }
                i++;
            }
        }
    }
}