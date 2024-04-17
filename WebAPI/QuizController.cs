using BackendLab01;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dto;

namespace WebAPI;

[Route("/api/v1/quizzes")]
public class QuizController : Controller
{
    private IQuizUserService _service;

    public QuizController(IQuizUserService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        var quizzes = _service.FindAll();
        return quizzes.Select(quiz => QuizDto.of(quiz)).ToList();
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var quiz = _service.FindQuizById(id);
        if (quiz is null) return NotFound();

        return Ok(QuizDto.of(quiz));
    }
    
    [HttpPost]
    [Route("{quizId}/items/{itemId}")]
    public void SaveAnswer([FromBody] QuizItemAnswerDto dto, int quizId, int itemId)
    {
        _service.SaveUserAnswerForQuiz(quizId, 1, itemId, dto.Answer);
    }

    [Route("{id}/results")]
    public ActionResult<QuizResultsDto> GetResults(int id)
    {
        var quiz = _service.FindQuizById(id);
        if (quiz is null) return NotFound();
        var numberOfCorrectAnswers = _service.CountCorrectAnswersForQuizFilledByUser(id, 1);
        var numberOfQuestions = quiz.Items.Count();

        return Ok(new QuizResultsDto()
            { NumberOfQuestions = numberOfQuestions, NumberOfCorrectAnswers = numberOfCorrectAnswers });
    }
}