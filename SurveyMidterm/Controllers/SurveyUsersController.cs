using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyMidterm.Models.Models.SurveyUser;
using SurveyMidterm.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SurveyMidterm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SurveyUsersController : ControllerBase
    {
        //You said to put the id somewhere so here it is in the both controllers - Matej Gjozinski ID:4200
        private readonly ISurveyUserService _service;
        public SurveyUsersController(ISurveyUserService service)
        {
            _service = service;
        }
        [HttpGet("/SurveyUsers/Get/{id:int}", Name = nameof(Get))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyUserModelBase))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _service.Get(id);
            return result != null
                ? (IActionResult)Ok(result)
                : NoContent();
        }

        [HttpGet("/SurveyUsers/GetAll", Name = nameof(GetAll))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SurveyUserModelBase>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.Get();
            return result != null && result.Any()
                ? (IActionResult)Ok(result)
                : NoContent();
        }

        [HttpPost("", Name = nameof(PostSurveyUser))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostSurveyUser([FromBody] SurveyUserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await _service.Insert(model);

                if (item != null)
                {
                    return CreatedAtRoute(nameof(Get), item, item.Id);
                }
                return Conflict();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SurveyUserModelBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyUserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                var result = await _service.Update(model);

                return result != null
                    ? (IActionResult)Ok(result)
                    : NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.Delete(id));
            }
            return BadRequest();
        }
    }
}
