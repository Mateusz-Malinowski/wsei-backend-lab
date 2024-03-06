using ApplicationCore.Interfaces.Repository;
using BackendLab01;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetService<IGenericRepository<QuizItem, int>>();
            var quizItems1 = new List<QuizItem>()
            {
                new QuizItem(1, "Czy Burj Khalifa to najwyższy budynek na świecie?", new List<string>() { "nie" },
                    "tak"),
                new QuizItem(2, "Czy Nil jest najszerszą rzeką na świecie?", new List<string>() { "tak" }, "nie"),
                new QuizItem(3, "Czy Afryka to największy kontynent na świecie?", new List<string>() { "tak" }, "nie"),
            };
            var quiz1 = new Quiz(1, quizItems1, "Rekordy wielkości");
            var quizItems2 = new List<QuizItem>()
            {
                new QuizItem(4, "W którym roku wybuchła I Wojna Światowa?", new List<string>() { "1910", "1939", "1918" },
                    "1914"),
                new QuizItem(5, "W którym roku wybuchła II Wojna Światowa?", new List<string>() { "1945", "1918", "1914" }, "1939"),
                new QuizItem(6, "Kto był pierwszym prezydentem III RP?", new List<string>() { "Andrzej Duda", "Bronisław Komorowski", "Lech Wałęsa" }, "Aleksander Kwaśniewski"),
            };
            var quiz2 = new Quiz(2, quizItems1, "Historia");
            
            quizRepo.Add(quiz1);
            quizRepo.Add(quiz2);
            foreach (var element in quizItems1) quizItemRepo.Add(element);
            foreach (var element in quizItems2) quizItemRepo.Add(element);
        }
    }
}