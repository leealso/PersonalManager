using PersonalManager.Application.Categories.Commands.CreateCategory;
using PersonalManager.Application.Categories.Commands.DeleteCategory;
using PersonalManager.Application.Categories.Commands.UpdateCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PersonalManager.Application.Categories.Queries.GetCategories;

namespace PersonalManager.WebUI.Controllers
{
    //[Authorize]
    public class CategoriesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<CategoriesVm>> Get()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
