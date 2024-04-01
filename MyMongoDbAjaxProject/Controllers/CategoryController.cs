using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMongoDbAjaxProject.Dal.Entities;
using MyMongoDbAjaxProject.Dal.Settings;

namespace MyMongoDbAjaxProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryController(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient("DatabaseSettings");
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection.Database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
