using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    public enum Type
    {
        Guess,
        LobbyUpdate,
        Help,
        Miss,
        Drawing,
        Start
    }
    [Serializable]
    public class ClassToSend
    {
        public Type Type { get; set; }
        public string GuessedWord { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Turn { get; set; }
        public List<User> Users { get; set; }


    }
}
