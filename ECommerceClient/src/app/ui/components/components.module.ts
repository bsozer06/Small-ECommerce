import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ProductsComponent,
    BasketsModule,
    HomeModule
  ]
})
export class ComponentsModule { }
