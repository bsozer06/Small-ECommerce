import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { Create_User } from 'src/app/contracts/users/create_user';
import { List_User } from 'src/app/contracts/users/list_user';
import { User } from 'src/app/entities/user';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private httpClientService: HttpClientService,
    private toastrService: CustomToastrService) { }

  async create(user: User): Promise<Create_User> {
    const observable = this.httpClientService.post<Create_User | User>({
      controller: "users"
    }, user)

    return await firstValueFrom(observable) as Create_User;
  }

  async updatePassword(userId: string, resetToken: string, password: string, passwordConfirm: string, successCallback?: () => void, errorCallback?: (error) => void): Promise<any> {
    const observable = this.httpClientService.post<any>({
      controller: "users",
      actions: "update-password"
    }, {
      userId: userId,
      resetToken: resetToken,
      password: password,
      passwordConfirm: passwordConfirm
    });

    const promiseData: Promise<any> = firstValueFrom(observable);
    promiseData
      .then(value => successCallback())
      .catch(error => errorCallback(error));

    await promiseData;
  }


  async getAllUsers(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ totalUsersCount: number; users: List_User[] }> {
    const observable: Observable<{ totalUsersCount: number; users: List_User[] }> = this.httpClientService.get({
      controller: "users",
      queryString: `page=${page}&size=${size}`
    });

    const promiseData = firstValueFrom(observable);
    promiseData.then(value => successCallBack())
      .catch(error => errorCallBack(error));

    return await promiseData;
  }

  async assignRoleToUser(id: string, roles: string[], successCallBack?: () => void, errorCallBack?: (error) => void) {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "users",
      actions: "assign-role-to-user"
    }, {
      userId: id,
      roles: roles
    })

    const promiseData = observable.subscribe({
      next: successCallBack,
      error: errorCallBack
    });

    await promiseData;
  }

  async getRolesToUser(userId: string, successCallBack?: () => void, errorCallBack?: (error) => void): Promise<string[]> {
    const observable: Observable<{ userRoles: string[] }> = this.httpClientService.get({
      controller: "users",
      actions: "get-roles-to-user"
    }, userId);

    const promiseData = firstValueFrom(observable);
    promiseData.then(() => successCallBack())
      .catch((error) => errorCallBack(error));

    return (await promiseData).userRoles;
  }

}
