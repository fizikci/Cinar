namespace Membership.DTO
{
    public class ServiceRequest<T>
    {
        public string APIKey { get; set; }
        public int ResellerId { get; set; }
        public T Data { get; set; }

        public string ClientIP { get; set; }
        public string Client { get; set; }
        public string SessionId { get; set; }
    }
}