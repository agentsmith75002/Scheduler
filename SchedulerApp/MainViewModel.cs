using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataAccess.Controller;
using DataAccess.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace SchedulerApp
{
    public class MainViewModel : BindableBase
    {
        private IEnumerable<Project> _projects;

        public IEnumerable<Project> Projects
        {
            get { return _projects; }
            set { SetProperty(ref _projects, value); }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get { return _errorMsg; }
            set { SetProperty(ref _errorMsg, value); }
        }
        public ICommand ShowProjectsCmd => new DelegateCommand(() =>
        {
            using (var db = new SchedulerContext())
            {
                ErrorMsg = null;
                Projects = db.Projects;
            }
        });
        public MainViewModel()
        {
            try
            {
                using (var db = new SchedulerContext())
                {
                    db.Projects.RemoveRange(db.Projects);
                    var p0 = new Project
                    {
                        Name = "Niveau 0",
                        BeginDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(30)
                    };
                    var p1 = new Project
                    {
                        IdParent = p0.Id,
                        Name = "Niveau 1",
                        BeginDate = p0.BeginDate,
                        EndDate = p0.BeginDate.AddDays(10)
                    };
                    var p2 = new Project
                    {
                        IdParent = p0.Id,
                        Name = "Niveau 1",
                        BeginDate = p0.BeginDate.AddDays(10),
                        EndDate = p0.BeginDate.AddDays(30)
                    };
                    db.Projects.Add(p0);
                    db.Projects.Add(p1);
                    db.Projects.Add(p2);
                    var count = db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
        }
    }
}
