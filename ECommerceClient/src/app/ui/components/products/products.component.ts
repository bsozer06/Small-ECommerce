import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(
    spinner: NgxSpinnerService,
    private httpClientService: HttpClientService
    ) {
    super(spinner);
   }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom);

    // this.httpClientService.post({
    //   controller: "products"
    // },
    // {
    //   name: "Kalem",
    //   stock: 100,
    //   price: 10
    // }).subscribe();


    this.httpClientService.get<Create_Product[]>({
      controller: "products"
    }).subscribe(data => console.log(data));
  }

}
