using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BackendLab01.Pages
{
    
    public class QuizModel : PageModel
    {
        private readonly IQuizUserService _userService;

        private readonly ILogger _logger;
        public QuizModel(IQuizUserService userService, ILogger<QuizModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [BindProperty]
        public string Question { get; set; }
        [BindProperty]
        public List<string> Answers { get; set; }
        
        [BindProperty]
        public String UserAnswer { get; set; }
        
        [BindProperty]
        public int QuizId { get; set; }
        
        [BindProperty]
        public int ItemId { get; set; }
        
        public void OnGet(int quizId, int itemId)
        {
            QuizId = quizId;
            ItemId = itemId;
            var quiz = _userService.FindQuizById(quizId);
            var quizItem = quiz?.Items.Find(item => item.Id == itemId);
            Question = quizItem?.Question;
            Answers = new List<string>();
            if (quizItem is not null)
            {
                Answers.AddRange(quizItem?.IncorrectAnswers);
                Answers.Add(quizItem?.CorrectAnswer);
            }
        }

        public IActionResult OnPost()
        {
            var quiz = _userService.FindQuizById(QuizId);
            var quizItem = quiz.Items.Find(item => item.Id == ItemId);
            var quizItemIndex = quiz.Items.FindIndex(item => item.Id == ItemId);
            
            _userService.SaveUserAnswerForQuiz(QuizId, 1, quizItem.Id, UserAnswer);
            
            if (quiz.Items.Count() == quizItemIndex + 1)
                return RedirectToPage("Summary", new { quizId = QuizId });
            return RedirectToPage("Item", new {quizId = QuizId, itemId = ItemId + 1});
        }
    }
}
