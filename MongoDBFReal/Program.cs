
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBFReal;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//Move string somewhere secret.
MongoClient dbClient = StartMenu.OpenStartMenu();

MainMenu mainMenu = new MainMenu(dbClient);



var dbList = dbClient.ListDatabases().ToList();

var database = dbClient.GetDatabase("Test_Database");
var collection = database.GetCollection<BsonDocument>("grades");


//FindOneWithFilter();

void AddDocument()
{
    var document = new BsonDocument
{

    {"student_id", 1001 },
    {"scores", new BsonArray
        {
        new BsonDocument{ { "type", "exams" }, { "score", 8866.1234} },
        new BsonDocument{ { "type", "testsss" }, { "score", 46644.1234} }
        }
    }
};

    collection.InsertOne(document);


}


void FindOneWithFilter()
{
    var filter = Builders<BsonDocument>.Filter.Eq("student_id", 1000);
    var studentDocument = collection.Find(filter).FirstOrDefault();
    Console.WriteLine(studentDocument.ToString());
}
