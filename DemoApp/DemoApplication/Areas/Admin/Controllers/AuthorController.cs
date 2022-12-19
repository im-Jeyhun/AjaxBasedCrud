using DemoApplication.Areas.Admin.ViewModels.Author;
using DemoApplication.Areas.Client.ViewComponents;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult List()
        {
            var model = _dataContext.Authors
                .Select(a => new ListItemViewModel(a.Id, a.FirstName, a.LastName))
                .ToList();


            return View(model);



        }

        [HttpPost("add-author", Name = "add-author")]

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
        [HttpGet("update-author/{id}", Name = "update-author")]
        public async Task<IActionResult> UpdateAuthorAsync(int id)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Name = author.FirstName,
                LastName = author.LastName
            };

            return PartialView("~/Areas/Admin/Views/Shared/Partials/_AuthorUpdateModalPartial",model);

        }

        [HttpPut("update-author/{id}", Name = "update-author")]
        public async Task<IActionResult> UpdateAuthorAsync(UpdateViewModel updateView)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == updateView.Id);

            if(author == null)
            {
                return NotFound();
            }

            author.FirstName = updateView.Name;
            author.LastName = updateView.LastName;
            author.CreatedAt = DateTime.Now;
            author.UpdatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return Ok();

        }


        [HttpDelete("delete-author/{id}",Name ="delete-author")]
        public async Task<IActionResult> DeleteAuthorAsync(int id)
        {
            var author = await _dataContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author is null)
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
