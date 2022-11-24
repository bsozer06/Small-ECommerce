import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { ListComponent } from './list/list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { DialogsModule } from 'src/app/dialogs/dialogs.module';
import { DeleteDirectiveModule } from 'src/app/directives/admin/delete.directive.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    UserComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: UserComponent }
    ]),
    MatSidenavModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatTableModule, MatPaginatorModule,
    DialogsModule,
    DeleteDirectiveModule
  ]
})
export class UserModule { }
