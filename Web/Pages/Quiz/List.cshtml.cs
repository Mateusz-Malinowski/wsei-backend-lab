using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendLab01.Pages;

public class List : PageModel
{
    public List<BackendLab01.Quiz> QuizList { get; set; }
    
    private readonly IQuizUserService _userService;
    
    public List(IQuizUserService userService)
    {
        _userService = userService;
    }
    
    public void OnGet()
    {
        QuizList = _userService.FindAll();
    }
}