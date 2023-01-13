using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBFReal
{
    public class Player
    {
        public int playerId;
        public string playerName;
        public int level;
        public int exp;


        public BsonDocument playerToBsonDocument()
        {
            return new BsonDocument
            {
                {"playerId", playerId},
                {"playerName", playerName },
                {"level", level },
                {"exp", exp }
            };
        }

        public Player(BsonDocument bson)
        {
            try
            {
                playerId = bson.GetValue("playerId").ToInt32();
                playerName = bson.GetValue("playerName").ToString();
                level = bson.GetValue("level").ToInt32();
                exp = bson.GetValue("exp").ToInt32();
            }
            catch { }
        }


        public void PrintPlayer()
        {

            Console.WriteLine($"Id: {playerId}\nName: {playerName}\nLevel: {level}\nExp:{exp}");
        }


        public Player(int playerId, string name, int level, int exp)
        {
            this.playerId = playerId;
            this.playerName = name;
            this.level = level;
            this.exp = exp;
        }
    }
}
