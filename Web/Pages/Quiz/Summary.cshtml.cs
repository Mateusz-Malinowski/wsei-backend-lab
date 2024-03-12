using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class Summary : PageModel
{
    public int NumberOfCorrectAnswers { get; set; }
    public double PercentageScore { get; set; }
    public BackendLab01.Quiz Quiz { get; set; }
    
    private readonly IQuizUserService _userService;
    
    public Summary(IQuizUserService userService)
    {
        _userService = userService;
    }
    
    public void OnGet(int quizId)
    {
        Quiz = _userService.FindQuizById(quizId);
        NumberOfCorrectAnswers = _userService.CountCorrectAnswersForQuizFilledByUser(quizId, 1);
        PercentageScore = (double)NumberOfCorrectAnswers / Quiz.Items.Count() * 100;
    }
}