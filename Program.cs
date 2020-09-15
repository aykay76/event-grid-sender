using System;
using System.Collections.Generic;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace event_grid_sender
{
    class PortalUser
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Attributes { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string topicEndpoint = "https://<< event grid topic endpoint here >>.eventgrid.azure.net/api/events";
            string topicKey = "<< insert key here >>";
            string topicHostname = new Uri(topicEndpoint).Host;

            TopicCredentials topicCredentials = new TopicCredentials(topicKey);
            EventGridClient client = new EventGridClient(topicCredentials);
            client.PublishEventsAsync(topicHostname, GetEventsList()).GetAwaiter().GetResult();

            Console.Write("Published events to Event Grid.");
        }

        static IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();
            for (int i = 0; i < 1; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "created",
                    Data = new PortalUser()
                    {
                        ID = Guid.NewGuid(),
                        Name = "Alan Kelly",
                        Description = "Architect, Developer, Engineer",
                        Attributes = new List<string> {
                            "developer",
                            "architect",
			    "engineer"
                        }
                    },
                    EventTime = DateTime.Now,
                    Subject = "aykay76",
                    Topic = "system-portal-user",
                    DataVersion = "1.0"
                });
            }
            return eventsList;
        }
    }
}
