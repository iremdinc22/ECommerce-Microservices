using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
        _mapper = mapper;
        _categoryCollection =database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }
    public async Task  CreateProductAsync(CreateProductDto createProductDto)
    {
       var value = _mapper.Map<Product>(createProductDto);
        await _productCollection.InsertOneAsync(value);
    }

    public async Task DeleteProductAsync(string id)
    {
        await _productCollection.DeleteOneAsync(x=>x.ProductId==id);
    }

    public async Task<List<ResultProductDto>> GetAllProductAsync()
    {
       var values = await _productCollection.Find(x => true).ToListAsync();
       return _mapper.Map<List<ResultProductDto>>(values);
    }

    public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
    {
       var values = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();

       return _mapper.Map<GetByIdProductDto>(values);
    }
    
    public Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        var value =  _mapper.Map<Product>(updateProductDto);
        return _productCollection.ReplaceOneAsync(x => x.ProductId == updateProductDto.ProductId, value);
    }
    
    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
    {
        var values = await _productCollection.Find(x => true).ToListAsync();

        foreach (var item in values)
        {
            item.Category = await _categoryCollection
                .Find(x => x.CategoryId == item.CategoryId)
                .FirstOrDefaultAsync();
        }

        // === ÖNEMLİ: Liste olarak map et ===
        return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
    }

    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId)
    {
        var values = await _productCollection.Find(x => x.CategoryId == CategoryId).ToListAsync();

        foreach (var item in values)
        {
            item.Category = await _categoryCollection
                .Find(x => x.CategoryId == item.CategoryId)
                .FirstOrDefaultAsync();
        }
        return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        
    }
}