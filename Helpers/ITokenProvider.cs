using System.Threading.Tasks;

namespace CPMS_AUTO.Helpers
{
    public interface ITokenProvider
    {
        Task<TokenResponse> GetToken(TokenRequest request);
    }
}
