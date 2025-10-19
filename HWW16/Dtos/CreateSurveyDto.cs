
public class CreateSurveyDto
{
    public string Title { get; set; }
    public List<QuestionDto> Questions { get; set; } = []; 
}