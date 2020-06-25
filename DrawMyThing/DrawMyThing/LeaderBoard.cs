using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DrawMyThing
{
    public class LeaderBoard
    {
        List<Entry> LB { get; set; }
        public LeaderBoard()
        {
            LB = new List<Entry>();
        }
        public LeaderBoard(List<Entry> LB)
        {
            this.LB = LB;
        }
        public static void createEmpty(string location)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(location, FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, new List<Entry>());
            fs.Close();
        }
        public static LeaderBoard getLeaderBoard(string location)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(location,FileMode.Open,FileAccess.Write);
            List<Entry> LB = (List<Entry>)bf.Deserialize(fs);
            fs.Close();
            return new LeaderBoard(LB);
        }
        public void setLeaderBoard(List<Entry> LB,string location)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(location, FileMode.Open, FileAccess.Read);
            bf.Serialize(fs, LB);
            fs.Close();
        }
    }
}
