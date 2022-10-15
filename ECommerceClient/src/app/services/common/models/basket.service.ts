import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { Create_Basket_Item } from 'src/app/contracts/basket/create_basket_item';
import { List_Basket_Item } from 'src/app/contracts/basket/list-basket-item';
import { Update_Basket_Item } from 'src/app/contracts/basket/update_basket_item';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private httpClientService: HttpClientService) { }

  async get() : Promise<List_Basket_Item[]> {
  const observeble: Observable<List_Basket_Item[]> = this.httpClientService.get({
    controller: "baskets"
  });
  return await firstValueFrom(observeble);
}


async add(basketItem: Create_Basket_Item) : Promise<void> {
 const observeble: Observable<any> = this.httpClientService.post({
    controller: "baskets"
  }, basketItem);
   await firstValueFrom(observeble);
}

async updateQuantity(basketItem: Update_Basket_Item) : Promise<void> {
  const observeble: Observable<any> = this.httpClientService.put({
     controller: "baskets"
   }, basketItem);
    await firstValueFrom(observeble);
 }


 async remove(basketItemId: string) : Promise<void> {
  const observeble: Observable<any> = this.httpClientService.delete({
     controller: "baskets"
   }, basketItemId);
    await firstValueFrom(observeble);
 }

}
