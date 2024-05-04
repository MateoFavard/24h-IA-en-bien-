namespace P24H.Exceptions;

public class TourDejaFiniException : P24HException
{
    public TourDejaFiniException() : base("Le tour est déjà fini") { }
}