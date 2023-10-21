namespace TaskManagement.Core.Responses
{
    public class GetTaskResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
