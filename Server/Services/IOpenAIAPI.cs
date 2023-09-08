using AIChef.Shared;

namespace AIChef.Server.Services
{
    public interface IOpenAIAPI
    {
        Task<List<Idea>> CreateRecipeideas(string mealtime, List<string> ingredients);

    }
}
