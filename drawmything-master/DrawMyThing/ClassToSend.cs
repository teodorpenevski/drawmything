using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    public enum Type
    {
        Guess,//Zbor za pogotok
        LobbyUpdate, //Update za igraci vo lobby
        Help,//Pomosni bukvi vo zborot
        Miss,//Ne ste go pogodile zborot
        Drawing,//Civ red e za crtanje
        Start,//Start Game
        Picture,//Se prakja niza od linija
        TimesUp//Isteceno vreme za crtanje
    }
    [Serializable]
    public class ClassToSend
    {
        public string WordToGuess { get; set; }
        public Type Type { get; set; }
        public string GuessedWord { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Turn { get; set; }
        public List<User> Users { get; set; }
        public List<Line> Lines { get; set; }
        
        public ClassToSend()
        {
            Lines = new List<Line>();
        }
        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Draw(g);
            }

        }
        public void Clear()
        {
            Lines.Clear();
        }
    }
}
