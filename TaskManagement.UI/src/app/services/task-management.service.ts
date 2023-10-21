import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { BaseUrls, Paths } from '../constants/url.constants';
import { UserModel } from '../models/users.models';
import { Observable } from 'rxjs';
import { OdataContext } from '../models/base.models';

@Injectable({
  providedIn: 'root'
})
export class TaskManagementService {
  constructor(private _httpClient: HttpClient) { }

  getAllUsers(): Observable<OdataContext<UserModel[]>> {
    return this._httpClient.get<OdataContext<UserModel[]>>(`${BaseUrls.LocalUrl}${Paths.Users}`);
  }
}
