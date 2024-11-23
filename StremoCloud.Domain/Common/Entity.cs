using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StremoCloud.Domain.Common;

public class Entity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } 
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; } 
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; } 
    public bool IsDeleted { get; set; }
}
