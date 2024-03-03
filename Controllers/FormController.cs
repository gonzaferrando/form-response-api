using FormResponse.Api.DTOs;
using FormResponse.Api.ThirdParties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<ActionResult> Get()
        {
            var response = await _fillOutApi.GetAsync("/forms/cLZojxk94ous");
            var rrrppp = await response.Content.ReadAsStringAsync();
            var filloutForm = JsonConvert.DeserializeObject<FilloutFormResponse>(rrrppp);
            return Ok(filloutForm);
        }
    }
}
