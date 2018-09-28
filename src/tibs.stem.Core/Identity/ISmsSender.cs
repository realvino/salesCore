using System.Threading.Tasks;

namespace tibs.stem.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}