namespace FrogPay.API.Token
{
    public interface ITokenGenerator
    {
        string GenerateTokenV2();

        string GenerateJwtToken();
    }
}
