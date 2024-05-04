namespace P24H.Exceptions;

public class UnknownMessageException: P24HException
{
    public UnknownMessageException(String badMessage) : base($"Message invalide : '{badMessage}'") { }
}