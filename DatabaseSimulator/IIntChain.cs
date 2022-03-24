namespace DatabaseSimulator;

public interface IIntChain
{
    // Retourne le nombre
    public int Get();

    // Ajoute "num" au nombre
    public IntChain Add(int num);

    // Soustrait "num" au nombre
    public IntChain Sub(int num);

    // Multiplie le nombre par "num";
    public IntChain Times(int num);

    // Divise le nombre par "num"
    public IntChain DivBy(int num);

    // Incrémente le nombre
    public IntChain Increment();

    // Décremente le nombre
    public IntChain Decrement();
}
