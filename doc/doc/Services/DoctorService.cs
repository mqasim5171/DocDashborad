using MongoDB.Driver;
using doc.Models;
using Microsoft.Extensions.Options;

namespace doc.Services
{
    public class DoctorService
    {
        private readonly IMongoCollection<DoctorModel> doctorsCollection;

        // Constructor that accepts IMongoClient and DoctorDatabaseSettings
        public DoctorService(IMongoClient mongoClient, IOptions<DoctorDatabaseSettings> doctorDatabaseSettings)
        {
            // Get the database name from the configuration settings
            var database = mongoClient.GetDatabase(doctorDatabaseSettings.Value.DatabaseName);
            doctorsCollection = database.GetCollection<DoctorModel>("Doctors");
        }

        // Get all doctors
        public async Task<List<DoctorModel>> GetAllDoctorsAsync()
        {
            return await doctorsCollection.Find(_ => true).ToListAsync();
        }

        // Add a new doctor (same as CreateAsync)
        public async Task AddDoctorAsync(DoctorModel doctor)
        {
            await doctorsCollection.InsertOneAsync(doctor);
        }

        // Create a new doctor
        public async Task CreateAsync(DoctorModel doctor)
        {
            await doctorsCollection.InsertOneAsync(doctor);
        }

        // Get a doctor by ID
        public async Task<DoctorModel> GetDoctorByIdAsync(string id)
        {
            return await doctorsCollection.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        // Update a doctor's details
        public async Task<DoctorModel> UpdateDoctorAsync(string id, DoctorModel doctor)
        {
            var result = await doctorsCollection.ReplaceOneAsync(d => d.Id == id, doctor);
            return result.IsAcknowledged ? doctor : null;
        }

        // Delete a doctor by ID
        public async Task<bool> DeleteDoctorAsync(string id)
        {
            var result = await doctorsCollection.DeleteOneAsync(d => d.Id == id);
            return result.DeletedCount > 0;
        }

        // Fetch a doctor by phone number
        public async Task<DoctorModel> GetDoctorByPhoneAsync(string phone)
        {
            return await doctorsCollection.Find(d => d.Phone == phone).FirstOrDefaultAsync();
        }
    }
}

