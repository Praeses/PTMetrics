using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivotalTrackerService
{
    using System.Xml.Serialization;

    [XmlType("iteration_length")]
    public class Iteration_Length
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("start_date")]
    public class Start_Date
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("first_iteration_start_time")]
    public class First_Iteration_Start_Time
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("current_iteration_number")]
    public class Current_Iteration_Number
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("enable_tasks")]
    public class Enable_Tasks
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("last_activity_at")]
    public class Last_Activity_At
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("memberships")]
    public class Memberships
    {
        public string type { get; set; }
        public object membership { get; set; }
    }

    [XmlType("integrations")]
    public class Integrations
    {
        public string type { get; set; }
    }

    [XmlType("project")]
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public Iteration_Length iteration_length { get; set; }
        public string week_start_day { get; set; }
        public string point_scale { get; set; }
        public string account { get; set; }
        public string start_date { get; set; }
        public string first_iteration_start_time { get; set; }
        public Current_Iteration_Number current_iteration_number { get; set; }
        public Enable_Tasks enable_tasks { get; set; }
        public string velocity_scheme { get; set; }
        public string current_velocity { get; set; }
        public string initial_velocity { get; set; }
        public string number_of_done_iterations_to_show { get; set; }
        public string labels { get; set; }
        public string last_activity_at { get; set; }
        public string allow_attachments { get; set; }
        public string use_https { get; set; }
        public string bugs_and_chores_are_estimatable { get; set; }
        public string commit_mode { get; set; }
        public Memberships memberships { get; set; }
        public Integrations integrations { get; set; }
    }

    [XmlType("id")]
    public class Id
    {
        public string type { get; set; }
        public int text { get; set; }
    }

    [XmlType("project_id")]
    public class Project_Id
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("estimate")]
    public class Estimate
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("created_at")]
    public class Created_At
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("updated_at")]
    public class Updated_At
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("accepted_at")]
    public class Accepted_At
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("notes")]
    public class Notes
    {
        public string type { get; set; }
        public object note { get; set; }
    }

    [XmlType("tasks")]
    public class Tasks
    {
        public string type { get; set; }
        public object task { get; set; }
    }

    [XmlType("deadline")]
    public class Deadline
    {
        public string type { get; set; }
        public string text { get; set; }
    }

    [XmlType("story")]
    public class Story
    {
        public int id { get; set; }
        public Project_Id project_id { get; set; }
        public string story_type { get; set; }
        public string url { get; set; }
        public Estimate estimate { get; set; }
        public string current_state { get; set; }
        public string name { get; set; }
        public string requested_by { get; set; }
        public string owned_by { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string accepted_at { get; set; }
        public string labels { get; set; }
        public Notes notes { get; set; }
        public string description { get; set; }
        public Tasks tasks { get; set; }
        public Deadline deadline { get; set; }
    }

    [XmlType("stories")]
    public class Stories
    {
        public string type { get; set; }
        public string count { get; set; }
        public string total { get; set; }
        public Story[] story { get; set; }
    }

}
