using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

class WriteAllLines
{
    public static async Task ExampleAsync(List<string> str)
    {
        await File.WriteAllLinesAsync("WriteLines.txt", str);
    }
}