using System;
using System.Collections.Generic;

namespace musicalbox
{

public class MusicalBox
{
    public Action<string, string> MessageHandler;

    private List<string> words = null;

    private string[] notas = {"do","re","mi","fa","sol","la","si"};

    public MusicalBox() => words = new List<string>();

    private bool IsMusicalWord(string word)
    {
      bool val = false;

      foreach(var nota in notas)
      {
        if (word.ToLower().IndexOf(nota) != -1)
        {
          val = true;
        }
      }
      
      return val;
    }

    public void AddMusicalWord(string word)
    {
      if (IsMusicalWord(word))
      {
        words.Add(word);
        MessageHandler?.Invoke(word, "entro en la cajita musical");
      }
      else
      {
        MessageHandler?.Invoke(word, "no entro en la cajita musical");
      }
    }

    public string GetWords()
    {
      string val = "[ ";
      
      foreach(string word in words)
      {
        val=val + word + " ";
      }

      val = val + "]";
      
      return val;
    }
}

}
