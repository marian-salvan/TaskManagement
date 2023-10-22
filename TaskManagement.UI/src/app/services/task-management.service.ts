import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BaseUrls, Paths } from '../constants/url.constants';
import { UserModel } from '../models/users.models';
import { Observable } from 'rxjs';
import { OdataContext } from '../models/base.models';
import { TaskModel, TaskSummary } from '../models/tasks.models';

@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {
  constructor(private _httpClient: HttpClient) { }

  getAllUsers(): Observable<OdataContext<UserModel[]>> {
    return this._httpClient.get<OdataContext<UserModel[]>>(`${BaseUrls.LocalUrl}${Paths.Users}`);
  }

  getFilteredTasks(filter: string): Observable<OdataContext<TaskModel[]>> {
    return this._httpClient.get<OdataContext<TaskModel[]>>(`${BaseUrls.LocalUrl}${Paths.Tasks}${filter}`);
  }

  getTasksCount(): Observable<number> {
    return this._httpClient.get<number>(`${BaseUrls.LocalUrl}${Paths.Tasks}/\$count`);
  }

  getUserTasksCount(userId: string): Observable<number> {
    return this._httpClient.get<number>(`${BaseUrls.LocalUrl}${Paths.Tasks}/${userId}/TotalUserTasks`);
  }

  getTaskSummary(taskId: string): Observable<TaskSummary> {
    return this._httpClient.get<TaskSummary>(`${BaseUrls.LocalUrl}${Paths.Tasks}/${taskId}/Summary`);
  }
}
