namespace DatabaseSimulator;
public class Order
{
	public Guid ID;
	public int Quantity;
	public string ProductName;
	public double ProductUnitPrice;
	public double TotalPrice { get => Quantity * ProductUnitPrice; }

	public Order(Guid id, int quantity, string productName, double productUnitPrice)
    {
		ID = id;
		Quantity = quantity;
		ProductName = productName;
		ProductUnitPrice = productUnitPrice;
    }

	public void IncreaseQuantity()
    {
		if(Quantity < 100)
		Quantity++;
    }

	public void DecreaseQuantity()
    {
		if(Quantity > 2)
		Quantity--;
    }

}
