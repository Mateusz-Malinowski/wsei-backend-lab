using BackendLab01;

namespace WebAPI.Dto;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }
    
    public static QuizItemDto of(QuizItem quizItem)
    {
        var dto = new QuizItemDto()
        {
            Id = quizItem.Id,
            Question = quizItem.Question,
            Options = new List<string>(quizItem.IncorrectAnswers)
        };
        
        dto.Options.Add(quizItem.CorrectAnswer);
        dto.Options.Shuffle();

        return dto;
    }
}