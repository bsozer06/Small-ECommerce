import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { FileUploadDialogComponent, FileUploadDialogState } from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { DialogService } from '../dialog.service';
import { HttpClientService } from '../http-client.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent {

  constructor(
    private httpClientService: HttpClientService,
    private customToastrService: CustomToastrService,
    private alertifyService: AlertifyService,
    private dialogService: DialogService,
    private spinner: NgxSpinnerService
  ) { }

  public files: NgxFileDropEntry[];

  @Input() options: Partial<FileUploadOptions>;

  public selectedFiles(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    for (const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
        fileData.append(_file.name, _file, file.relativePath);
      })
    }

    this.dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      afterClosed : () => {
        this.spinner.show(SpinnerType.BallAtom);
        this.httpClientService.post({
          controller: this.options.controller,
          actions: this.options.action,
          queryString: this.options.queryString,
          headers: new HttpHeaders({ "responseType": "blob" })
        }, fileData).subscribe(data => {
          const message: string = "Files was uploaded successfuly"
          this.spinner.hide(SpinnerType.BallAtom);

          if (this.options.isAdminPanel) {
            this.alertifyService.message(message, {
              dismissOthers: true,
              messageType: MessageType.Success,
              position: Position.TopRight
            })
          } else {
            this.customToastrService.message(message, "file upload", {
              messageType: ToastrMessageType.Success,
              position: ToastrPosition.TopRight
            });
          }

        }, (error: HttpErrorResponse) => {
          this.spinner.hide(SpinnerType.BallAtom);

          const message: string = "Files did not be uploaded"
          if (this.options.isAdminPanel) {
            this.alertifyService.message(message, {
              dismissOthers: true,
              messageType: MessageType.Error,
              position: Position.TopRight
            })
          } else {
            this.customToastrService.message(message, "file upload", {
              messageType: ToastrMessageType.Error,
              position: ToastrPosition.TopRight
            })
          }

        });
      }
    });




  }


  // openDialog(afterClosedCallBack?: any): void {
  //   const dialogRef = this.dialog.open(FileUploadComponent, {
  //     width: '250px',
  //     data: FileUploadDialogState.Yes,
  //   });
  //   dialogRef.afterClosed().subscribe(result => {
  //     if ( result == FileUploadDialogState.Yes) {
  //       afterClosedCallBack();
  //     }
  //   });
  // }

}



export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explaination?: string;
  accept?: string;
  isAdminPanel?: boolean = false;
}
