namespace FormResponse.Api.DTOs
{
    public class FilloutFormResponse
    {
        public List<FilloutResponses> Responses { get; set; }

    }

    public class FilloutResponses
    {
        public List<Answer> Questions { get; set; }
    }

    public class Answer
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
