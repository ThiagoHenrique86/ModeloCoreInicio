
namespace Prodesp.Domain.Exceptions;
   
public class TokenExpiradoException : System.Exception
{
    public TokenExpiradoException() : base() { }

    public TokenExpiradoException(string message) : base(message) { }
}



