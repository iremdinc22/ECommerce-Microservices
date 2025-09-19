using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices;

public class ProductImageService : IProductImageService
{
    private readonly IMongoCollection<ProductImage> _productImageCollection;
    private readonly IMapper _mapper;

    public ProductImageService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _productImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
        _mapper = mapper;
    }

    public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
    {
        var value = _mapper.Map<ProductImage>(createProductImageDto);
        await _productImageCollection.InsertOneAsync(value);
    }

    public async Task DeleteProductImageAsync(string id)
    {
        await _productImageCollection.DeleteOneAsync(x=>x.ProductImageId==id);
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
    {
        var values = await _productImageCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultProductImageDto>>(values);
    }

    public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
    {
         var values = await  _productImageCollection.Find<ProductImage>(x => x.ProductImageId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdProductImageDto>(values);
    }

    public Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
    {
        var value = _mapper.Map<ProductImage>(updateProductImageDto);
        return  _productImageCollection.ReplaceOneAsync(x => x.ProductImageId == updateProductImageDto.ProductImageId, value);
    }
}