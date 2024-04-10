using BackendLab01;

namespace WebAPI.Dto;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<QuizItemDto> Items { get; set; }
    
    public static QuizDto of(Quiz quiz)
    {
        var dto = new QuizDto()
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Items = new List<QuizItemDto>()
        };

        foreach (var item in quiz.Items) dto.Items.Add(QuizItemDto.of(item));

        return dto;
    }
}