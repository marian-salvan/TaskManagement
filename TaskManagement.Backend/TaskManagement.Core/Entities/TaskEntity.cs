namespace TaskManagement.Core.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
