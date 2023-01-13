using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBFReal
{
    public class PlayerFinder
    {
        MainMenu mainMenu;

        public PlayerFinder(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;

        }


        public void Menu()
        {
            Console.WriteLine("Search how?");
            int selection = Input.SelectInt(new string[] { "By Id", "By name" }, true);
            switch (selection)
            {
                case 0:
                    ById();
                    break;
                case 1:
                    ByName();
                    break;
                default:
                    break;
            }
        }

        void ById()
        {
            Console.WriteLine("Enter Id\n");
            var filter = Builders<BsonDocument>.Filter.Eq("playerId", Input.ReadInt());
            var result = mainMenu.collection.Find(filter).FirstOrDefault();
            if (result != null)
                new Player(result).PrintPlayer();
            else
            {
                Console.WriteLine("No player with this Id");
            }
            Console.WriteLine("------------------\n");

        }

        void ByName()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            var filter = Builders<BsonDocument>.Filter.Eq("playerName", name);
            var result = mainMenu.collection.Find(filter).ToList();

            if (result.Count > 1)
            {
                Console.WriteLine("Multiple players with that name");
                foreach (var item in result)
                {
                    new Player(item).PrintPlayer();
                }
            }
            else if (result.Count == 1)
            {
                new Player(result[0]).PrintPlayer();
            }
            else Console.WriteLine("No player by that name");

        }
    }
}
