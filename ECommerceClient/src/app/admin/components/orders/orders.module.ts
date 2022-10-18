import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';
import { RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { FileUploadModule } from 'src/app/services/common/file-upload/file-upload.module';
import { DialogsModule } from 'src/app/dialogs/dialogs.module';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { DeleteDirectiveModule } from 'src/app/directives/admin/delete.directive.module';



@NgModule({
  declarations: [
    OrdersComponent,
    ListComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: OrdersComponent }
    ]),
    MatSidenavModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatTableModule, MatPaginatorModule,
    DialogsModule,
    DeleteDirectiveModule
  ]
})
export class OrdersModule { }
