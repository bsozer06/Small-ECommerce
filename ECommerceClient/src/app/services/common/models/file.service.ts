import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { BaseUrl } from 'src/app/contracts/base_url';
import { CustomToastrService } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) { }

  async getBaseStorageUrl(): Promise<BaseUrl> {
    const observable: Observable<BaseUrl> = this.httpClientService.get<BaseUrl>({
      controller: "files",
      actions: "GetBaseStorageUrl"
    });

    return await firstValueFrom(observable);
  }

}
