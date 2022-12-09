import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, Observable, of } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';
import { UserAuthService } from './models/user-auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(
    private toastrService: CustomToastrService,
    private userAuthService: UserAuthService,
    private router: Router,
    private spinner: NgxSpinnerService
  ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log("inteceptor");
    return next.handle(req).pipe(catchError(error => {
      console.log("inteceptor", error);
      switch (error.status) {
        case HttpStatusCode.Unauthorized:


          this.userAuthService.refreshTokenLogin(localStorage.getItem("refreshToken"), (state) => {
            if (!state) {
              const url = this.router.url;
              if (url == "/products") {
                this.toastrService.message("you should enter your account !!", "Unauthorized", {
                  messageType: ToastrMessageType.Warning,
                  position: ToastrPosition.TopRight
                });
              } else {
                this.toastrService.message("you are not authorized !!", "Unauthorized", {
                  messageType: ToastrMessageType.Warning,
                  position: ToastrPosition.BottomFullWidth
                });
              }
            }
          }).then(data => {
            this.toastrService.message("you are not authorized !!", "Unauthorized", {
              messageType: ToastrMessageType.Warning,
              position: ToastrPosition.BottomFullWidth
            });
          });
          break;
        case HttpStatusCode.InternalServerError:
          this.toastrService.message("you did not access server !!", "InternalServerError", {
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          break;
        case HttpStatusCode.BadRequest:
          this.toastrService.message("your request is invalid !!", "BadRequest", {
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          break;
        case HttpStatusCode.NotFound:
          this.toastrService.message("page cannot be found !!", "NotFound", {
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          break;
        default:
          this.toastrService.message("unexpected error occurred. !!", "UnexpectedError", {
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.BottomFullWidth
          });
          break;
      }

      this.spinner.hide(SpinnerType.BallAtom);
      return of(error);
    }))
  }
}
