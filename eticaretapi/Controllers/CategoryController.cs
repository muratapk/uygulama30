using eticaretapi.Data;
using eticaretapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticaretapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryList()
        {
            return await _context.categories.ToListAsync();

        }
        [HttpGet("id")]
        public async Task<ActionResult<Category>> GetCategoryId(int id)
        {
            return await _context.categories.FindAsync(id);
           
        }
        [HttpDelete("id")]
        public async Task<ActionResult<Category>>DeleteCategory(int id)
        {
            var result = await _context.categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if(result != null) 
            {
                _context.Remove(result);
                await _context.SaveChangesAsync(); 
                return Ok(result);
                
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPut("id")]
       public async Task<ActionResult<Category>>EditCategory(Category category,int id)
        {
            var result=await _context.categories.Where(x=>x.CategoryId == id).FirstOrDefaultAsync();
            if (result!=null)
            {
                result.CategoryName = category.CategoryName;
                await _context.SaveChangesAsync();
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<ActionResult<Category>>AddCategory(Category category)
        {
           _context.categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
