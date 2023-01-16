using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

                    case 3:
                        DeletePlayer();
                        break;


                    case 4:
                        UpdatePlayer();
                        break;


                    case 5:
                        Console.WriteLine("Closing");
                        Console.ReadKey();
                        open = false;
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

            var filter = Builders<BsonDocument>.Filter.Eq("playerId", id);
            if (collection.Find(filter).ToList().Count != 0) { Console.WriteLine("Player with that id already exsists."); return; }
            collection.InsertOne(player.playerToBsonDocument());

        }
        void FindPlayer()
        {
            PlayerFinder pf = new PlayerFinder(this);
            pf.Menu();
        }
        void UpdatePlayer()
        {
            Console.WriteLine("Enter id of player you want to update");
            int id = Input.ReadInt();
            var filter = Builders<BsonDocument>.Filter.Eq("playerId", id);
            Console.WriteLine("What do you want to update?");
            int selection = Input.SelectInt(new string[] { "Name", "Level", "Exp" }, true);
            if (collection.Find(filter).ToList().Count > 0)
                switch (selection)
                {
                    case 0:
                        Console.WriteLine("Enter new name");
                        string name = Console.ReadLine();
                        var nameUpdate = Builders<BsonDocument>.Update.Set("playerName", name);
                        collection.UpdateOne(filter, nameUpdate);
                        break;
                    case 2:
                        Console.WriteLine("Enter new xp");
                        int newXp = Input.ReadInt();
                        var xpUpdate = Builders<BsonDocument>.Update.Set("exp", newXp);
                        collection.UpdateOne(filter, xpUpdate);
                        break;
                    case 1:
                        Console.WriteLine("Enter new level");
                        int newLevel = Input.ReadInt();
                        var levelUpdate = Builders<BsonDocument>.Update.Set("level", newLevel);
                        collection.UpdateOne(filter, levelUpdate);
                        break;
                }
            else Console.WriteLine("No player with that id");
        }

        void DeletePlayer()
        {
            Console.WriteLine("Enter id of player to be deleted");

            int id = Input.ReadInt();
            var filter = Builders<BsonDocument>.Filter.Eq("playerId", id);
            collection.DeleteOne(filter);

        }

    }
}
