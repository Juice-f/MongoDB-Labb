using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBFReal
{
    public class MainMenu
    {
        public MongoClient dbclient;
        public IMongoDatabase db;
        public IMongoCollection<BsonDocument> collection;

        public MainMenu(MongoClient dbClient)
        {
            this.dbclient = dbClient;
            db = dbclient.GetDatabase("Labb_Players");
            collection = db.GetCollection<BsonDocument>("Players");

            OpenMainMenu();
        }

        public void OpenMainMenu()
        {
            Console.WriteLine("Options:");
            bool open = true;
            int selection;



            while (open)
            {
                selection = Input.SelectInt(new string[] { "List all players", "Find player", "Create player", "Delete player", "Update player", "Close program" }, true);
                switch (selection)
                {
                    case 0:
                        ListPlayers();
                        break;
                    case 1:
                        FindPlayer();
                        break;

                    case 2:
                        CreatePlayer();
                        break;
                }
            }
        }


        void ListPlayers()
        {
            var documents = collection.Find(new BsonDocument()).ToList();
            foreach (var item in documents)
            {
                Console.WriteLine("----------");
                Player player = new Player(item);
                Console.WriteLine($"Id: {player.playerId} Name: {player.playerName}");

            }
            Console.WriteLine("----------\nPress any key to return to menu");

            Console.ReadKey();
        }

        void CreatePlayer()
        {
            Console.WriteLine("Enter player name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter player id");
            int id = Input.ReadInt();
            Console.WriteLine("Enter player level");
            int level = Input.ReadInt();
            Console.WriteLine("Enter player xp");
            int xp = Input.ReadInt();

            Player player = new Player(id, name, level, xp);

            collection.InsertOne(player.playerToBsonDocument());

        }
        void FindPlayer()
        {
            PlayerFinder pf = new PlayerFinder(this);
            pf.Menu();
        }
    }
}
