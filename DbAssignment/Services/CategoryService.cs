

using DbAssignment.Entities;
using DbAssignment.Repositories;

namespace DbAssignment.Services;

public class CategoryService(CategoryRepository categoryRepository)
{

    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public CategoryEntity CreateCategory(string categoryName)
    {
        var categoryEntity = _categoryRepository.Get(x => x.CategoryName == categoryName);
        categoryEntity ??= _categoryRepository.Create(new CategoryEntity { CategoryName = categoryName });
       
        return categoryEntity;
        
    }   

    public CategoryEntity GetCategoryByCategoryName(string categoryName)
    {
        var categoryEntity = _categoryRepository.Get(x => x.CategoryName == categoryName);
        return categoryEntity;
    }

    public IEnumerable<CategoryEntity> GetAllCategories()
    {
        var categories = _categoryRepository.GetAll();
        return categories;
    }

    public CategoryEntity UpdateCategory(CategoryEntity categoryEntity)
    {
        var updatedCategory = _categoryRepository.Update(x => x.Id == categoryEntity.Id, categoryEntity);
        return updatedCategory;
    }


    public void DeleteCategory(int Id)
    {
        _categoryRepository.Delete(x => x.Id == Id);
        
    }
}
