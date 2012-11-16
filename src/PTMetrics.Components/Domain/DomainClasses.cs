using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTMetrics.Components.Domain
{
    using System.Data.Entity;

    public class Project
    {
        public int Id { get; set; }
        public int Tracker_Id { get; set; }
        public TrackerType TrackerType { get; set; }
        public List<Story> Stories { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }

    public class Story
    {
        public int Id { get; set; }
        public int Tracker_Id { get; set; }
        public TrackerType TrackerType { get; set; }
        public virtual Project Project { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAtDate { get; set; }
        public DateTime? UpdatedAtDate { get; set; }
        public DateTime? AcceptedAtDate { get; set; } 
    }

    // enum for tracker type (PT vs. Trello vs. AgileZen)
    public enum TrackerType
    {
        PivotalTracker = 1,
        AgileZen = 2,
        Trello = 3
    }

    public class PMToolsContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Story> Stories { get; set; }
    }
}
