using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileRenamer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Folder Location: ");
                var folderLoc = Console.ReadLine();

                var d = new DirectoryInfo(folderLoc ?? throw new InvalidOperationException());
                var fileArray = d.GetFiles();
                Console.WriteLine(fileArray.Length);
                try
                {
                    Parallel.ForEach(fileArray, (file) =>
                    {
                        Console.WriteLine(file.Name);
                        var newName = "";
                        var oldName = file.Name;
                        var fluffRemoved = false;
                        foreach (var character in oldName)
                        {
                            if (fluffRemoved) newName += character;
                            else if (character == '_') fluffRemoved = true;
                        }

                        if (newName == "") return;
                        Console.WriteLine($"Renamed: {oldName} to {newName}");
                        File.Move(d.FullName + "\\" + oldName, d.FullName + "\\" + newName);
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("Renaming complete!");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}
