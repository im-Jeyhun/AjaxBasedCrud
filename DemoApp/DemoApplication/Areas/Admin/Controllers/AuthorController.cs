using DemoApplication.Areas.Admin.ViewModels.Author;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/author")]
    public class AuthorController : Controller
    {
        private readonly DataContext _dataContext;

        public AuthorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "admin-author-list")]
        public  IActionResult List()
        {
            var model = _dataContext.Authors
                .Select(a => new ListItemViewModel(a.Id, a.FirstName, a.LastName))
                .ToList();


            return  View(model);

          

        }

        [HttpPost("add-author", Name ="add-author")]

        public async Task<IActionResult> AddAuthorAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var author = new Author
            {
                FirstName = model.Name,
                LastName = model.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _dataContext.Authors.AddAsync(author);

            await _dataContext.SaveChangesAsync();

            var id = author.Id;

            return Created("admin-author-list", id);

        }

        [HttpDelete("delete-author/{id}", Name ="delete-author")]
        public  IActionResult DeleteAuthorAsync(int id)
        {
            var author =  _dataContext.Authors.FirstOrDefault(a => a.Id == id);
            
            if(author is null)
            {
                return BadRequest();
            }

            _dataContext.Authors.Remove(author);

            _dataContext.SaveChanges();

            var modell = _dataContext.Authors
               .Select(a => new ListItemViewModel(a.Id, a.FirstName, a.LastName))
               .ToList();

            return NoContent();

        }
    }
}
