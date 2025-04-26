namespace ps40165_User.Shared.Enums;

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
