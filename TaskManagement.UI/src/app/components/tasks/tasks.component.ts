import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PaginatorState } from 'primeng/paginator';
import { Subscription, forkJoin } from 'rxjs';
import { TaskModel, TaskSummary } from 'src/app/models/tasks.models';
import { TaskManagementService } from 'src/app/services/task-management.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent implements OnInit, OnDestroy {

  //because of the large number of tasks - we will use pagination
  //in this case and infinite scroll might have been a better choice
  public pageTasks: TaskModel[] = [];
  public currentPage = 0;
  public pageSize = 5;
  public totalRecords = 0;
  public taskSummary: TaskSummary | null = null;

  private userId: string | undefined = undefined;
  private subscription: Subscription = new Subscription();
  public visible: boolean = false;
  
  constructor(private route: ActivatedRoute, private taskManagementService: TaskManagementService) { }

  ngOnInit(): void {
    this.userId = this.route.snapshot.queryParams['userId'];
    
    const filter = this.getFilter(this.userId);
    this.getTasksData(filter);
  }

  public getSummary(task: TaskModel) {

    // TODO: add a loading indicator
    this.subscription.add(this.taskManagementService.getTaskSummary(task.Id).subscribe((tasks) => {
      this.taskSummary = tasks;
      this.visible = true;
    }));
  }

  public onPageChange(event: PaginatorState) {
    this.currentPage = event.page as number;
    this.pageSize = event.rows as number;

    const filter = this.getFilter(this.userId);
    this.getTasksData(filter);
  }

  private getTasksData(filter: string): void {
    const taskObservale =  this.taskManagementService.getFilteredTasks(filter);
    const countObservable = this.userId ? this.taskManagementService.getUserTasksCount(this.userId) : 
                                     this.taskManagementService.getTasksCount();

    this.subscription.add(
      forkJoin([taskObservale, countObservable]).subscribe(([data, count]) => {
        this.pageTasks = data.value;
        this.totalRecords = count;
      })
    );
  }

  private getFilter(userId: string | undefined): string {
    let filter = '';
    let top = this.pageSize;
    let skip = this.currentPage * this.pageSize;

    filter = `?$top=${top}&$skip=${skip}`;

    if (userId) {
      filter += `&$filter=userId eq '${userId}'`;
    }

    return filter;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
