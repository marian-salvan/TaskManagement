export interface TaskModel {
    Id: string;
    Name: string;
    Description: string;
    Status: string;
    UserId: string;
    CreatedDate: Date;
}

export interface TaskSummary {
    taskId: string;
    summary: string;
}