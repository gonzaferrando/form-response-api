using FormResponse.Api.DTOs;
using FormResponse.Api.Filters;
using FormResponse.Api.ThirdParties;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

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
            var listOfQuestions = filloutForm.Responses.SelectMany(response => response.Questions);

            var filtered = ApplyFilters(listOfQuestions, filters);

            return Ok(filtered);
        }

        protected IEnumerable<T> ApplyFilters<T>(IEnumerable<T> source, List<Filter>? filters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (filters == null || !filters.Any())
            {
                return source;
            }

            var queryData = new List<T>();
            foreach (var filter in filters)
            {
                if (string.IsNullOrEmpty(filter.Id))
                {
                    throw new ArgumentNullException("Filter id cannot be empty.");
                }

                var parameter = Expression.Parameter(typeof(T), "item");
                var property = Expression.Property(parameter, "value");
                var constant = Expression.Constant(filter.Value);

                var propertyId = Expression.Property(parameter, "id");
                var constantId = Expression.Constant(filter.Id);

                // Find the response with specific Id.
                Expression idMatch = Expression.Equal(propertyId, constantId);

                Expression filterExpression;

                switch (filter.Condition.ToLower())
                {
                    case "equals":
                        filterExpression = Expression.AndAlso(idMatch, Expression.Equal(property, constant));
                        break;
                    case "does_not_equal":
                        filterExpression = Expression.AndAlso(idMatch, Expression.NotEqual(property, constant));
                        break;
                    case "greater_than":
                        filterExpression = Expression.AndAlso(idMatch, Expression.GreaterThan(property, constant));
                        break;
                    case "less_than":
                        filterExpression = Expression.AndAlso(idMatch, Expression.LessThan(property, constant));
                        break;
                    default:
                        throw new ArgumentException($"Unsupported condition operator: {filter.Condition}");
                }

                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                var filteredResults = source.AsQueryable().Where(lambda).ToList();

                // If no match, return empty.
                if (filteredResults.Count == 0)
                {
                    return new List<T>();
                }
                queryData.AddRange(source.AsQueryable().Where(lambda).ToList());
            }

            return queryData;
        }

    }
}
