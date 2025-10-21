using HWW16.Entities;

public class Vote
{
    public int Id { get; set; } 
    public int UserId { get; set; } 
    public int SurveyId { get; set; } 
    public int? QuestionId { get; set; } 
    public int? SelectedOptionId { get; set; }
    public User User { get; set; }
    public Survey Survey { get; set; }
    public Question Question { get; set; }
    public Option SelectedOption { get; set; }
}