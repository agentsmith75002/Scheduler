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
                Projects = db.Projects;
            }
        });
        public MainViewModel()
        {
            try
            {
                using (var db = new SchedulerContext())
                {
                    db.Projects.Add(new Project { Name = "Root", BeginDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) });
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
