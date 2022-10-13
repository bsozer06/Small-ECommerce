import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseUrl } from 'src/app/contracts/base_url';
import { List_Product } from 'src/app/contracts/list_product';
import { Product_Pagination } from 'src/app/contracts/product_pagination';
import { FileService } from 'src/app/services/common/models/file.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor(
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private fileService: FileService
    ) { }

  baseUrl: BaseUrl;
  products: List_Product[];
  currentPageNo: number;
  totalProductCount: number;
  totalPageCount: number;
  pageSize: number = 12;
  pageList: number[] = [];

  async ngOnInit() {
    this.baseUrl = await this.fileService.getBaseStorageUrl();
    this.activatedRoute.params.subscribe(async params => {
      this.currentPageNo = parseInt(params["pageNo"] ?? 1);
      const data: Product_Pagination = await this.productService.read(this.currentPageNo-1, this.pageSize, () => {
      },
      errorMessage => { });

      this.products = data.products;
      this.products = this.products.map<List_Product>(p => {
        const listProduct: List_Product = {
          name: p.name,
          id: p.id,
          price: p.price,
          stock: p.stock,
          updatedDate: p.updatedDate,
          createdDate: p.createdDate,
          imagePath: p.productImageFiles.length ? p.productImageFiles.find(p => p.showcase).path : "",
          productImageFiles: p.productImageFiles
        };
        return listProduct;
      });

      // this.products.forEach((product, i) => {
      //   let p: any = {
      //     name: product.name,
      //     id: product.id,
      //     price: product.price,
      //     stock: product.stock,
      //     updatedDate: product.updatedDate,
      //     createdDate: product.createdDate,
      //     imagePath: product.productImageFiles.length ? product.productImageFiles.find(p => p.showcase).path : ""
      //   };
      // });

      this.totalProductCount = data.totalProductCount;
      this.totalPageCount = Math.ceil(parseFloat(this.totalProductCount.toString()) / parseFloat(this.pageSize.toString()));

      this.pageList = [];
      if (this.currentPageNo-3 <= 0 ) {
        for(let i=1; i<=7; i++) {
          this.pageList.push(i);
        }
      }
      else if(this.currentPageNo+3 >= this.totalPageCount) {
        for(let i = this.currentPageNo-6; i<=this.totalPageCount; i++) {
          this.pageList.push(i);
        }
      }
      else {
        for(let i = this.currentPageNo-3; i<=this.currentPageNo+3; i++) {
          this.pageList.push(i);
        }
      }

    })


  }

}
