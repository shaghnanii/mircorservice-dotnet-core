using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderWebApi.Models;

[Serializable, BsonIgnoreExtraElements]
public class OrderDetail
{
    [BsonElement("product_id"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
    public int ProductId { get; set; }
    [BsonElement("quantity"), BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
    public decimal Quantity { get; set; }
    [BsonElement("unit_price"), BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
    public decimal UnitPrice { get; set; }
}