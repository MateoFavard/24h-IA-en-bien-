namespace P24H.Model;

public enum TypeActivite
{
    Piller1 = 1,
    Piller2 = 2,
    Piller3 = 3,
    Piller4 = 4,
    Reparation = 10,
    Recele = 11,
    Attaque = 12,
    Aucune = 13
}

public static class TypeActiviteExtensions
{
    public static bool EstPillage(this TypeActivite @this)
    {
        return @this == TypeActivite.Piller1
               || @this == TypeActivite.Piller2
               || @this == TypeActivite.Piller3
               || @this == TypeActivite.Piller4;
    }
}