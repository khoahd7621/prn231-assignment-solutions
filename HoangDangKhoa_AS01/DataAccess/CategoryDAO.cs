using BusinessObjects;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listCategories = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public static Category FindCategoryById(int categoryID)
        {
            var category = new Category();
            try
            {
                using (var context = new MyDBContext())
                {
                    category = context.Categories.SingleOrDefault(c => c.CategoryID == categoryID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        public static void SaveCategory(Category category)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(category).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var categoryToDelete = context
                        .Categories
                        .SingleOrDefault(c => c.CategoryID == category.CategoryID);
                    context.Categories.Remove(categoryToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}