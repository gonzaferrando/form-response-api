using FormResponse.Api.DTOs;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var filloutForm = await _fillOutApi.GetAsync<FilloutFormResponse>("/forms/cLZojxk94ous");
            return Ok(filloutForm);
        }
    }
}
