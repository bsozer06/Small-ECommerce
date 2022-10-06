import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService) { }

  async create(user: User): Promise<Create_User> {
    const observeble = this.httpClientService.post<Create_User | User>({
      controller: "users"
    }, user)

    return await firstValueFrom(observeble) as Create_User;
  }
}
