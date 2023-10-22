import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { OdataContext } from 'src/app/models/base.models';
import { UserModel } from 'src/app/models/users.models';
import { TaskManagementService } from 'src/app/services/task-management.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, OnDestroy {

  public users: UserModel[] = [];
  private subscription: Subscription = new Subscription();

  constructor(private taskManagementService: TaskManagementService) { }

  ngOnInit(): void {
    this.subscription.add(
      this.taskManagementService.getAllUsers().subscribe((data: OdataContext<UserModel[]>) => {
        this.users = data.value;
      })
    );
  }

  ngOnDestroy(): void {
   this.subscription.unsubscribe();
  }
  
}
