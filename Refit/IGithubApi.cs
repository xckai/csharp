using System.Threading.Tasks;
using Refit;

namespace RefitTest
{
    public interface IGithubApi
    {
        [Get("/users/list")]
        Task<string> GetUsers();
    }
}