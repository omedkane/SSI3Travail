namespace DatabaseSimulator;
public interface INumChain
{
	// Retourne le nombre
	public int Get();

	// Ajoute "num" au nombre
	public NumChain Add(int num);
	
	// Soustrait "num" au nombre
	public NumChain Sub(int num);
	
	// Multiplie le nombre par "num";
	public NumChain Times(int num);

	// Divise le nombre par "num"
	public NumChain DivBy(int num);

	// Incrémente le nombre
	public NumChain Increment();

	// Décremente le nombre
	public NumChain Decrement();
}