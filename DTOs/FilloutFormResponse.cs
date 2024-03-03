namespace FormResponse.Api.DTOs
{
    public class FilloutFormResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
