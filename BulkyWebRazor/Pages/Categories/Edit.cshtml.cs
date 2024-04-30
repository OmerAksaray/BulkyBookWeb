using BulkyWebRazor.Data;
using BulkyWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db=db;
        }


        public void OnGet(int? categoryId)
        {
            if (categoryId != null)
            {
                Category = (_db.Categories.Find(categoryId));
            }
            }

        public IActionResult OnPost()
        {
            _db.Categories.Update(Category);
            _db.SaveChanges();
            TempData["success"] = "Category updated successffully";
            return RedirectToPage("Index");
        }
    }
}
