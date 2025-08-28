using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace best_slim_jim_engine.Models;

[Table("flavors")]
public class SlimJimFlavor : BaseModel
{
    [Column("id")]
    public int Id { get; set; } //Auto maps
    [Column("flavor")]
    public string Flavor { get; set; } //Auto maps
    [Column("description")]
    public string? Description { get; set; } //Auto maps
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}