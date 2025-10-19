using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices;

public class ContactService : IContactService
{
    private readonly IMongoCollection<Contact> _contactCollection;
    private readonly IMapper _mapper;

    public ContactService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _contactCollection = database.GetCollection<Contact>(databaseSettings.ContactCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultContactDto>> GetAllContactAsync()
    {
        var values = await _contactCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultContactDto>>(values);
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        var value = _mapper.Map<Contact>(createContactDto);
        await _contactCollection.InsertOneAsync(value);
    }

    public Task UpdateContactAsync(UpdateContactDto updateContactDto)
    {
        var value = _mapper.Map<Contact>(updateContactDto);
        return  _contactCollection.ReplaceOneAsync(x => x.ContactId == updateContactDto.ContactId, value);
    }

    public async Task DeleteContactAsync(string id)
    {
        await  _contactCollection.DeleteOneAsync(x=>x.ContactId==id);
    }

    public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
    {
        var values = await  _contactCollection.Find<Contact>(x => x.ContactId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdContactDto>(values);
    }
}