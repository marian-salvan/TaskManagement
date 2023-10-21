namespace TaskManagement.Core.Responses
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
