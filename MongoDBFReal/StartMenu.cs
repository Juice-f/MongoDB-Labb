using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBFReal
{
    public class StartMenu
    {
        public static MongoClient OpenStartMenu()
        {
            Console.WriteLine("Enter client credentials");

            bool loggedIn = false;
            MongoClient dbClient = null;
            while (!loggedIn)
            {
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();

                dbClient = new MongoClient($"mongodb+srv://{username}:{password}@cluster0.ld6r5jl.mongodb.net/?retryWrites=true&w=majority");



                try
                {
                    var dbList = dbClient.ListDatabases().ToList();
                    loggedIn = true;
                } catch (Exception ex)
                {
                    Console.WriteLine("Invalid login");
                }
            }

            Console.WriteLine("logged in");
            return dbClient;



        }
    }
}
