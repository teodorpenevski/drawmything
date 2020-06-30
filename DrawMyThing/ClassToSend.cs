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
        Another, //Drug user go pogodil zborot
        Guess, //Zbor za pogotok
        LobbyUpdate, //Update za igraci vo lobby
        Guessed, //Pogoden zbor
        Miss, //Ne ste go pogodile zborot
        Drawing, //Civ red e za crtanje
        Start, //Start Game
        Picture, //Se prakja niza od linija
        TimesUp, //Isteceno vreme za crtanje
        PointUpdate, //Azuriranje poeni
        RoundEnd, //Kraj na rundata
        RoundStartTest, //Pocetok na rundata
        CloseConnection, //Zatvoranje na konekcija
        Ping
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
        public List<ServerUser> Users { get; set; }
        public List<Line> Lines { get; set; }
        public int Points { get; set; }
        public int RoundId { get; set; }
        public string DrawerName { get; set; }

        public ClassToSend()
        {
            Lines = new List<Line>();
            Users = new List<ServerUser>();
        }

        public ClassToSend(ClassToSend cts)
        {
            this.WordToGuess = cts.WordToGuess;
            this.Type = cts.Type;
            this.GuessedWord= cts.GuessedWord;
            this.Name = cts.Name;
            this.Id = cts.Id;
            this.Turn = cts.Turn;
            this.Users = new List<ServerUser>();
            this.Lines = new List<Line>();
            this.Points = cts.Points;
            this.RoundId = cts.RoundId;
            this.DrawerName = cts.DrawerName;
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
