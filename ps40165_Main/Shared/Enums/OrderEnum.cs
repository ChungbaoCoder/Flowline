namespace ps40165_Main.Shared.Enums;

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public enum PaymentStatus
{
    Unpaid,
    Paid,
    Refunded,
    Failed
}
