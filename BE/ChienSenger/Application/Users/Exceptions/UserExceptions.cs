namespace Application.Users.Exceptions;

public class UsernameAlreadyExistsException(string message) : Exception(message);
public class InvalidCredentialsException(string message) : Exception(message);
