using DemoApplication.Areas.Admin.ViewModels.Author;
using DemoApplication.Areas.Client.ViewModels.Basket;
using DemoApplication.Database;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DemoApplication.Areas.Client.ViewComponents
{

    public class AuthorUpdate : ViewComponent
    {
        private readonly DataContext _dataContext;
        public AuthorUpdate(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IViewComponentResult Invoke(UpdateViewModel model)
        {
            var author = _dataContext.Authors.FirstOrDefault(a => a.Id == model.Id);

            return View(model);
        }
    }
}
