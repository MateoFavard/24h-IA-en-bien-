namespace P24H.Network;

public interface ICommand
{
    public String BuildMessage();
    
    public bool TermineTour { get; }
}

public record Piller(int numeroRoute) : ICommand
{
    public string BuildMessage() => $"PILLER|{this.numeroRoute}";
    public bool TermineTour => true;
}

public record Reparer() : ICommand
{
    public string BuildMessage() => "REPARER";
    public bool TermineTour => true;
}

public record Receler() : ICommand
{
    public string BuildMessage() => "RECELER";
    public bool TermineTour => true;
}

public record Recruter() : ICommand
{
    public string BuildMessage() => "RECRUTER";
    public bool TermineTour => false;
}

public record Trahir(int numeroJoueur) : ICommand
{
    public string BuildMessage() => $"TRAHIR|{this.numeroJoueur}";
    public bool TermineTour => true;
}