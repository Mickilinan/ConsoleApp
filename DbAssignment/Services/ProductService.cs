using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ProductService(ProductRepository productRepository, CategoryService categoryService)
{

    private readonly ProductRepository _productRepository = productRepository;
    private readonly CategoryService _categoryService = categoryService;

    public ProductEntity CreateProduct(string productName, decimal price, string description,string categoryName)
    {

        
        var existingProduct = _productRepository.Get(x => x.ProductName == productName);
        if (existingProduct != null)
        {
            return null;
        }

        var categoryEntity = _categoryService.CreateCategory(categoryName);

        var productEntity = new ProductEntity
        {
            ProductName = productName,
            Price = price,
            Description = description,
            CategoryId = categoryEntity.Id
        };

        productEntity = _productRepository.Create(productEntity);
        return productEntity;

    }

    public ProductEntity GetProductById(int id)
    {
        var productEntity = _productRepository.Get(x => x.Id == id);
        return productEntity;
    }

    public IEnumerable<ProductEntity> GetAllProducts()
    {
        var products = _productRepository.GetAll();
        return products;
    }

    public ProductEntity UpdateProduct(ProductEntity productEntity)
    {
        var updatedProduct = _productRepository.Update(x => x.Id == productEntity.Id, productEntity);
        return updatedProduct;
    }


    public bool DeleteProduct(int Id)
    {
        var product = _productRepository.Get(x => x.Id == Id);
        if (product == null)
        {
            return false;
        }

        _productRepository.Delete(x => x.Id == Id);
        return true;

    }
}
