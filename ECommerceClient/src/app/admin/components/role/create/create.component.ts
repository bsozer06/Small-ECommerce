import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { RoleService } from 'src/app/services/common/models/role.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor(
    private roleService: RoleService,
    private alertifyService: AlertifyService,
    spinner: NgxSpinnerService
    ) {
      super(spinner);
    }

  ngOnInit(): void {
  }

  @Output() createdRole: EventEmitter<string> = new EventEmitter();

  create(name:HTMLInputElement) {
    this.showSpinner(SpinnerType.BallAtom);
    this.roleService.create(name.value , () => {
      this.hideSpinner(SpinnerType.BallAtom);
      this.alertifyService.message("Role was added successfully.", {
        dismissOthers: true,
        messageType: MessageType.Success,
        position: Position.TopRight
      });

      this.createdRole.emit(name.value);
    }, errorMessage => {
      this.alertifyService.message(errorMessage, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
    });
  }

}
