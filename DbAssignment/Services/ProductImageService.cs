using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class ProductImageService(ProductImageRepository productImageRepository, ProductService productService)
{

    private readonly ProductImageRepository _productImageRepository = productImageRepository;
    private readonly ProductService _productService = productService;

    public ProductImageEntity CreateProductImage(string productName, string imagePath, decimal price, string description, string categoryName)
    {

        var productEntity = _productService.CreateProduct(productName, price, description, categoryName);

        if (productEntity == null)
        {
            return null;
        }

        var productImageEntity = new ProductImageEntity
        {
            ImagePath = imagePath,
            ProductId = productEntity.Id
        };

        productImageEntity = _productImageRepository.Create(productImageEntity);
        return productImageEntity;
    }

    public ProductImageEntity GetProductImageById(int id)
    {
        var productImageEntity = _productImageRepository.Get(x => x.Id == id);
        return productImageEntity;
    }

    public IEnumerable<ProductImageEntity> GetAllProductImages()
    {
        var productImages = _productImageRepository.GetAll();
        return productImages;
    }

    public ProductImageEntity UpdateProductImage(ProductImageEntity productImageEntity)
    {
        var updatedProductImage = _productImageRepository.Update(x => x.Id == productImageEntity.Id, productImageEntity);
        return updatedProductImage;
    }


    public void DeleteProductImage(int Id)
    {
        _productImageRepository.Delete(x => x.Id == Id);

    }
}
