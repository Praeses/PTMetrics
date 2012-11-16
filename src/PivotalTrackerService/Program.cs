using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerService
{
    using System.IO;
    using System.Xml.Serialization;

    using PTMetrics.Components.Domain;

    //using RestSharp.Deserializers;
    //using RestSharp.Serializers;

    class Program
    {
        private const string PT_TOKEN_NAME = "X-TrackerToken";
        private const string PT_TOKEN = "331246f2a6cf05c9d19291cfad7d036d";

        static void Main(string[] args)
        {
            string url = "http://www.pivotaltracker.com/";
            var client = new RestClient(url);

            var projectsRequest = GetRestRequest("services/v3/projects", Method.GET);
            var projectsResponse = client.Execute(projectsRequest);

            var projects = DeserializeResponse<Project[]>(projectsResponse, "projects");

            foreach (var project in projects)
            {
                var storiesRequest = GetRestRequest(string.Format("services/v3/projects/{0}/stories", project.id), Method.GET);
                var storiesResponse = client.Execute(storiesRequest);

                var stories = DeserializeResponse<Story[]>(storiesResponse, "stories");
                //Console.WriteLine("deserialized stories for project id: {0}", project.id);

                using (var db = new PMToolsContext())
                {
                    var persisted_project = MapProject(project);
                    db.Projects.Add(persisted_project);
                    db.SaveChanges();

                    persisted_project.Stories = new List<PTMetrics.Components.Domain.Story>();
                    foreach (var story in stories)
                    {
                        var persisted_story = MapStory(story, persisted_project);
                        db.SaveChanges();
                    }
                }
            }


            using (var db = new PMToolsContext())
            {
                var query = db.Projects;
                foreach (var project in query)
                {
                    Console.WriteLine("Project with id: {0} pulled from the database.  Tracker_Id: {1}, TrackerType: {2}", project.Id, project.Tracker_Id, project.TrackerType);
                }
            }

            Console.ReadKey();
        }

        private static PTMetrics.Components.Domain.Story MapStory(Story story, PTMetrics.Components.Domain.Project persisted_project)
        {
            var persisted_story = new PTMetrics.Components.Domain.Story();
            persisted_story.Tracker_Id = story.id;
            persisted_story.TrackerType = TrackerType.PivotalTracker;
            persisted_story.Url = story.url;
            persisted_story.Name = story.name;

            if (!string.IsNullOrWhiteSpace(story.created_at))
            {
                var createdAt = new DateTime();
                bool createdAtHasValue = DateTime.TryParse(story.created_at.Replace(" UTC", ""), out createdAt);
                persisted_story.CreatedAtDate = createdAtHasValue ? createdAt : persisted_story.CreatedAtDate;
            }

            if (!string.IsNullOrWhiteSpace(story.updated_at))
            {
                var updatedAt = new DateTime();
                bool updatedAtHasValue = DateTime.TryParse(story.updated_at.Replace(" UTC", ""), out updatedAt);
                persisted_story.UpdatedAtDate = updatedAtHasValue ? updatedAt : persisted_story.UpdatedAtDate;
            }

            if (!string.IsNullOrWhiteSpace(story.accepted_at))
            {
                var acceptedAt = new DateTime();
                bool acceptedAtHasValue = DateTime.TryParse(story.accepted_at.Replace(" UTC", ""), out acceptedAt);
                persisted_story.AcceptedAtDate = acceptedAtHasValue ? acceptedAt : persisted_story.AcceptedAtDate;
            }

            persisted_story.Project = persisted_project;
            persisted_project.Stories.Add(persisted_story);

            return persisted_story;
        }

        private static PTMetrics.Components.Domain.Project MapProject(Project project)
        {
            var persisted_project = new PTMetrics.Components.Domain.Project();
            persisted_project.Tracker_Id = project.id;
            persisted_project.TrackerType = TrackerType.PivotalTracker;
            persisted_project.Name = project.name;

            if (!string.IsNullOrWhiteSpace(project.first_iteration_start_time))
            {
                var startDate = new DateTime();
                bool startDateHasValue = DateTime.TryParse(
                    project.first_iteration_start_time.Replace(" UTC", ""), out startDate);
                persisted_project.StartDate = startDateHasValue ? startDate : persisted_project.StartDate;
            }

            if (!string.IsNullOrWhiteSpace(project.last_activity_at))
            {
                var lastActivityDate = new DateTime();
                bool lastActivityDateHasValue = DateTime.TryParse(
                    project.last_activity_at.Replace(" UTC", ""), out lastActivityDate);
                persisted_project.LastActivityDate = lastActivityDateHasValue
                                                         ? lastActivityDate
                                                         : persisted_project.LastActivityDate;
            }
            return persisted_project;
        }

        private static RestRequest GetRestRequest(string resource, Method httpMethod)
        {
            var request = new RestRequest(resource, httpMethod);
            request.RequestFormat = DataFormat.Xml;
            request.AddHeader(PT_TOKEN_NAME, PT_TOKEN);
            return request;
        }

        private static T DeserializeResponse<T>(IRestResponse response, string xmlRootAttributeName)
        {
            var byteArray = Encoding.ASCII.GetBytes(response.Content);
            var stream = new MemoryStream(byteArray);
            var deserializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));
            var returnValue = (T)deserializer.Deserialize(stream);

            return returnValue;
        }
    }
}
