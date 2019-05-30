using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;
using System.Data.Entity;

namespace DictaFoule.Common.Tools
{
    public static class ProjectTools
    {
        public static void UpdateProjectState(int IdProject, ProjectState State)
        {
            var entities = new Entities();
            var project = entities.projects.Find(IdProject);
            project.state = (int)State;
            entities.Entry(project).State = EntityState.Modified;
            entities.SaveChanges();
        }
    }
}
