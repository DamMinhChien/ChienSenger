namespace Application.Friends.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) {}
}