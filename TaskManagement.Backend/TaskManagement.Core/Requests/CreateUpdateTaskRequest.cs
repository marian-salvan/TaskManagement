namespace TaskManagement.Core.Requests
{
    public class CreateUpdateTaskRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
    }
}
