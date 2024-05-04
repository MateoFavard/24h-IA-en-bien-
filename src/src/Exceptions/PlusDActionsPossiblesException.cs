namespace P24H.Exceptions;

public class PlusDActionsPossiblesException : P24HException
{
    public PlusDActionsPossiblesException() : base("Maximum d'actions possibles atteint pour ce tour") { }
}