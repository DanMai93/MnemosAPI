#nullable disable
namespace MnemosAPI.Models;

public partial class Area
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}