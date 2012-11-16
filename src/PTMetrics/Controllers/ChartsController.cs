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
            var categories = this.GetCategories();
            var data = this.GetData();


            var vm = new ProjectTimelineVM();
            vm.Categories = categories;
            vm.Data = data;

            return View(vm);
        }

        private string GetCategories()
        {
            var categories = "";
            using (var db = new PMToolsContext())
            {
                var projects = db.Projects.Where(x => x.LastActivityDate != null && x.StartDate != null);
                foreach (var project in projects)
                {
                    categories += "'" + project.Name + "',";
                }
                categories = categories.Remove(categories.Length - 1, 1); // chop off last comma.
            }
            return categories;
        }

        private string GetData()
        {
            var data = "";
            using (var db = new PMToolsContext())
            {
                var projects = db.Projects.Where(x => x.LastActivityDate != null && x.StartDate != null);
                foreach (var project in projects)
                {
                    string dataPointFormat = "[Date.UTC({0}, {1}, {2}), Date.UTC({3}, {4}, {5})],";
                    var dataPoint = string.Format(
                        dataPointFormat,
                        project.StartDate.Value.Year,
                        project.StartDate.Value.Month,
                        project.StartDate.Value.Day,
                        project.LastActivityDate.Value.Year,
                        project.LastActivityDate.Value.Month,
                        project.LastActivityDate.Value.Day);

                    data += dataPoint;
                }
                data = data.Remove(data.Length - 1, 1); // chop off last comma.
            }

            return data;
        }
    }
}
