public class QuestionDtoListVM
    {
    public int Id { get; set; }
    public string? SubjectCode { get; set; }
    public int? TopicId { get; set; }
    public int? UserId { get; set; }
    public int? Difficulty { get; set; }
    public string? Text { get; set; }
    public int? Type { get; set; }
    public List<QuestionOptionDTOVM> Options { get; set; }
    }

public class QuestionOptionDTOVM
    {
    public string Option { get; set; }
    public bool IsCorrect { get; set; }
    }
