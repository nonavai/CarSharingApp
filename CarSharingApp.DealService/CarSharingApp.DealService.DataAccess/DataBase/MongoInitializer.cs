using CarSharingApp.DealService.DataAccess.Entities;
using CarSharingApp.DealService.Shared.Enums;
using MongoDB.Driver;

namespace CarSharingApp.DealService.DataAccess.DataBase;

public class MongoInitializer
{
    private static IMongoDatabase _database;

    public static void Initialize(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        CreateCollections();
        InsertTestData();
    }

    private static void CreateCollections()
    {
        _database.CreateCollection("Feedbacks");
        _database.CreateCollection("Deals");
        _database.CreateCollection("Answers");
    }

    private static void InsertTestData()
    {
        var feedbacksCollection = _database.GetCollection<Feedback>("Feedbacks");
        var dealsCollection = _database.GetCollection<Deal>("Deals");
        var answersCollection = _database.GetCollection<Answer>("Answers");
        
        var feedback = new Feedback
        {
            Id = null,
            DealId = "deal1",
            UserId = "user1",
            IssueType = IssueType.None,
            Posted = DateTime.Now,
            Text = "Test feedback",
            Rating = 5,
            IsChanged = false
        };

        var deal = new Deal
        {
            Id = null,
            CarId = "car1",
            UserId = "user1",
            Requested = DateTime.Now,
            Finished = null,
            State = DealState.Active,
            TotalPrice = 1000,
            Rating = 5,
        };

        var answer = new Answer
        {
            FeedBackId = feedback.Id,
            UserId = "user1",
            Posted = DateTime.Now,
            Text = "Test answer",
            IsChanged = false
        };
        
        feedbacksCollection.InsertOne(feedback);
        dealsCollection.InsertOne(deal);
        answersCollection.InsertOne(answer);
    }
}