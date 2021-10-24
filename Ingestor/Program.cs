using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialSite.Data;
using SocialSite.Data.Models;

namespace Ingestor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var reader = new StreamReader("Source_File.csv");

            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<CsvMessageMap>();

            var records = csvReader.GetRecords<CsvMessage>().ToList();

            var messages = records.Select(x => new Message
            {
                OriginalId = x.Id,
                Text = x.Text,
                Name = x.Name,
                ScreenName = x.ScreenName,
                Utc = DateTimeOffset.Parse(x.UTC),
                CreatedAt = DateTimeOffset.ParseExact(x.CreatedAt, "ddd MMM dd HH:mm:ss +0000 yyyy", CultureInfo.InvariantCulture),
                Favorites = int.Parse(x.Favorites),
                Shares = int.Parse(x.Retweets),
                Language = x.Language,
                Client = x.Client,
                Type = x.TweetType,
                Urls = x.URLs,
                Hashtags = x.Hashtags,
                Mentions = x.Mentions,
                MediaType = x.MediaType,
                MediaUrls = string.Join(' ', x.MediaURLs)
            });

            SubmitChangesToDatabase(messages);
        }

        private static void SubmitChangesToDatabase(IEnumerable<Message> messages)
        {
            Console.WriteLine("Writing to Database");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.connectionstrings.json")
                .Build();
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();

            var connectionToUse = Environment.OSVersion.Platform == PlatformID.Unix 
                ? "DockerDbConnection"
                : "DefaultConnection";
            var connectionString = configuration.GetConnectionString(connectionToUse);
            
            var res = dbContextOptions.UseSqlServer(connectionString);

            using var context = new AppDbContext(res.Options);

            var missingItems = messages.Where(x => !context.Messages
                .Select(xi => xi.OriginalId).Contains(x.OriginalId))
                .ToList();

            context.Messages.AddRange(missingItems);
            context.SaveChanges();

            Console.WriteLine("Done");
        }
    }

    public sealed class CsvMessageMap : ClassMap<CsvMessage>
    {
        public CsvMessageMap()
        {
            Map(x => x.Id).Index(0);
            Map(x => x.Text).Index(1);
            Map(x => x.Name).Index(2);
            Map(x => x.ScreenName).Index(3);
            Map(x => x.UTC).Index(4);
            Map(x => x.CreatedAt).Index(5);
            Map(x => x.Favorites).Index(6);
            Map(x => x.Retweets).Index(7);
            Map(x => x.Language).Index(8);
            Map(x => x.Client).Index(9);
            Map(x => x.TweetType).Index(10);
            Map(x => x.URLs).Index(11);
            Map(x => x.Hashtags).Index(12);
            Map(x => x.Mentions).Index(13);
            Map(x => x.MediaType).Index(14);

            Map(x => x.MediaURLs).Convert(row =>
                new List<string>
                {
                    row.Row.GetField(15),
                    row.Row.GetField(16),
                    row.Row.GetField(17),
                    row.Row.GetField(18)
                }.Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
        }
    }

    public class CsvMessage
    {
        public string Id {get;set;}

        public string Text {get;set;}

        public string Name {get;set;}

        public string ScreenName {get;set;}

        public string UTC {get;set;}

        public string CreatedAt {get;set;}

        public string Favorites {get;set;}

        public string Retweets {get;set;}

        public string Language {get;set;}

        public string Client {get;set;}

        public string TweetType {get;set;}

        public string URLs {get;set;}

        public string Hashtags {get;set;}

        public string Mentions {get;set;}

        public string MediaType {get;set;}

        public ICollection<string> MediaURLs {get;set;}
    }
}
