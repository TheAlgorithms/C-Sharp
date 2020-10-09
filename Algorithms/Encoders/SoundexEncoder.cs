using System;
using System.Linq;

namespace Algorithms.Encoders
{
    public class SoundexEncoder
    {
        public SoundexEncoder() {}

        public string Encode(string text)
        {
            string soundex = $"{text.ToUpper()[0]}";
            text = string.Join("", text.Select(ch => {
                switch(char.ToLower(ch))
                {
                    //remove aeiouyhw (map to zero)
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                    case 'y':
                        return '0';

                    case 'h':
                    case 'w':
                        return '8';

                    //bfpv -> 1
                    case 'b':
                    case 'f':
                    case 'p':
                    case 'v':
                        return '1';

                    //cgjkqsxz -> 2
                    case 'c':
                    case 'g':
                    case 'j':
                    case 'k':
                    case 'q':
                    case 's':
                    case 'x':
                    case 'z':
                        return '2';

                    //dt -> 3
                    case 'd':
                    case 't':
                        return '3';

                    //l -> 4
                    case 'l': return '4';

                    //mn -> 5
                    case 'm':
                    case 'n':
                        return '5';

                    //r -> 6
                    case 'r': return '6';

                    default: return '0';
                }
            }));

            //remove doubles, retain up to 4 chars (first letter + 3 digits)
            for(int i = 1; i < text.Length; i++)
            {
                if(text[i] != text[i - 1] && text[i] != '0' && text[i] != '8')
                {
                    //exception to doubles: sep. by 'h' or 'w'  (8)
                    if(!(i > 1 && text[i - 1] == '8' && text[i] == text[i - 2]))
                        soundex += text[i];
                }

                if(soundex.Length == 4) break;
            }

            //append to 3 digits if shorter
            while(soundex.Length < 4) soundex += '0';

            return soundex;
        }
    }
}
