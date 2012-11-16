using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTMetrics.Controllers
{
    using PTMetrics.Components.Domain;
    using PTMetrics.Models;

    public class ChartsController : Controller
    {
        //
        // GET: /Charts/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProjectTimeline()
        {
            var categories = "";
            using (var db = new PMToolsContext())
            {
                var projects = db.Projects.Where(x => x.LastActivityDate != null && x.StartDate != null);
                foreach (var project in projects)
                {
                    categories += "'" + project.Name + "',";
                }
                categories = categories.Remove(categories.Length-2, 1);
            }
            var vm = new ProjectTimelineVM();
            vm.Categories = categories;

            return View(vm);
        }

        public ActionResult StoryBreakdown()
        {
            List<Object> drilldownData = new List<object>();
            using (var db = new PMToolsContext())
            {
                var projects = db.Projects.Where(x => x.LastActivityDate != null && x.StartDate != null);
                foreach (var project in projects)
                {

                    var data = new
                    {
                        name = project.Name,
                        y = project.Stories.Count,
                        drilldown = new [] {
                            new {
                                name = "Accepted",
                                y = project.Stories.Where(s => s.AcceptedAtDate.HasValue).Count()
                            },
                            new {
                                name = "Not Accepted",
                                y = project.Stories.Where(s => !s.AcceptedAtDate.HasValue).Count()
                            }
                        }
                    
                    };
                    drilldownData.Add(data);
                }
            }
            var vm = new StoryBreakdownVM();
            vm.dataStories = drilldownData;

            return View(vm);
        }

    }
}
