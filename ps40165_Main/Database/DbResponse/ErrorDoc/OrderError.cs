namespace ps40165_Main.Database.DbResponse.ErrorDoc;

public class OrderError : IError
{
    public string Type => "Order";
    public List<string> Detail { get; }

    public OrderError()
    {
        Detail = new List<string>();
    }

    public OrderError NotFound()
    {
        Detail.Add("Không tìm thấy hóa đơn");
        return this;
    }

    public OrderError ItemsNotFounded(int productId, string name)
    {
        Detail.Add($"Không tìm thấy sản phẩm {name} có mã {productId}");
        return this;
    }

    public OrderError MismatchPrice(decimal productPrice, decimal comparePrice)
    {
        Detail.Add($"Giá sản phẩm {comparePrice} của bạn không trùng với giá của sản phẩm là {productPrice}");
        return this;
    }

    public OrderError LowStock(int productStock, int orderItemStock)
    {
        Detail.Add($"Hiện số lượng sản phẩm còn là {productStock} so với số lượng mua là {orderItemStock}");
        return this;
    }
}
