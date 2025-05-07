namespace MnemosAPI.Models
{
    public class ManagementTool
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
