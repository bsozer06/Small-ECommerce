import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
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
    private alertifyService: AlertifyService
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

    this.httpClientService.post({
      controller: this.options.controller,
      actions: this.options.action,
      queryString: this.options.queryString,
      headers: new HttpHeaders({ "responseType": "blob" })
    }, fileData).subscribe(data => {
      const message: string = "Files was uploaded successfuly"
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


}


export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explaination?: string;
  accept?: string;
  isAdminPanel?: boolean = false;
}
