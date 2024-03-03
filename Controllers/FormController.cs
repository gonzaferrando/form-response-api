using FormResponse.Api.DTOs;
using FormResponse.Api.Filters;
using FormResponse.Api.ThirdParties;
using Microsoft.AspNetCore.Mvc;

namespace FormResponse.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IHttpApiClient _fillOutApi;

        public FormController(IHttpApiClient fillOutApi)
        {
            _fillOutApi = fillOutApi;
        }

        [HttpGet("{formId}/filteredResponses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get([FromRoute] string formId, [FromQuery] List<Filter>? filters)
        {
            var filloutForm = await _fillOutApi.GetAsync<FilloutFormResponse>($"/forms/{formId}/submissions");

            return Ok(filloutForm);
        }
    }
}
