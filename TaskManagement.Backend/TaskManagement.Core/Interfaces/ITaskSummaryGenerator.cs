namespace TaskManagement.Core.Interfaces
{
   public interface ITaskSummaryGenerator
   {
        Task<string> GetTaskDescription();
   }
}
