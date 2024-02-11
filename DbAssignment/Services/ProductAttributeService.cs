using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ProductAttributeService(ProductAttributeRepository productAttributeRepository, ProductService productService)
{

    private readonly ProductAttributeRepository _productAttributeRepository = productAttributeRepository;
    private readonly ProductService _productService = productService;

    public ProductAttributeEntity CreateProductAttribute(string productName, string attributeName, decimal price, string description, string categoryName)
    {

        var productEntity = _productService.CreateProduct(productName, price, description, categoryName);

        if (productEntity == null)
        {
            return null;
        }

        var productAttributeEntity = new ProductAttributeEntity
        {
            AttributeName = attributeName,
            ProductId = productEntity.Id
        };

        productAttributeEntity = _productAttributeRepository.Create(productAttributeEntity);
        return productAttributeEntity;
    }

    public ProductAttributeEntity GetProductAttributeById(int id)
    {
        var productAttributeEntity = _productAttributeRepository.Get(x => x.Id == id);
        return productAttributeEntity;
    }

    public IEnumerable<ProductAttributeEntity> GetAllProductAttributes()
    {
        var productAttributes = _productAttributeRepository.GetAll();
        return productAttributes;
    }

    public ProductAttributeEntity UpdateProductAttribute(ProductAttributeEntity productAttributeEntity)
    {
        var updatedProductAttribute = _productAttributeRepository.Update(x => x.Id == productAttributeEntity.Id, productAttributeEntity);
        return updatedProductAttribute;
    }


    public void DeleteProductAttribute(int Id)
    {
        _productAttributeRepository.Delete(x => x.Id == Id);

    }
}
