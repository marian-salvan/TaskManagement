namespace TaskManagement.Core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<TaskEntity> Tasks { get; set; }
    }
}
