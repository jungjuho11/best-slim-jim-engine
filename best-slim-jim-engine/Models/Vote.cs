using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace best_slim_jim_engine.Models;

[Table("votes")]
public class Vote : BaseModel
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("flavor_id")]
    public int FlavorId { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}