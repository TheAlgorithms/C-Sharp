using System;

namespace musicalbox
{
    class Program
    {
      static void ShowMessage(string text, string message)
      {
        Console.WriteLine(text + " " + message);
      }
      

      static void Main(string[] args)
      {
        var musicalBox = new MusicalBox();
        musicalBox.MessageHandler = ShowMessage;

        //for test
        musicalBox.AddMusicalWord("perro");
        musicalBox.AddMusicalWord("lado");
        musicalBox.AddMusicalWord("casa");
        musicalBox.AddMusicalWord("piano");
        musicalBox.AddMusicalWord("MUSICA");
        

        ShowMessage("Palabras en la cajita musica: ", musicalBox.GetWords());
        
        

      }
    }
}
