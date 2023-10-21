export interface TaskModel {
    id: string;
    name: string;
    description: string;
    status: string;
    userId: string;
    createdDate: Date;
}

export interface TaskSummary {
    taskId: string;
    summary: string;
}