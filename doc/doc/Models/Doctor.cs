using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace doc.Models
{
    public class DoctorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Specialization")]
        public string Specialization { get; set; }

        [BsonElement("Department")]
        public string Department { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Phone")]
        public string Phone { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("ExperienceYears")]
        public int ExperienceYears { get; set; }

        [BsonElement("ConsultationFee")]
        public double ConsultationFee { get; set; }
    }
}
