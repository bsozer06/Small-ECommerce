import { NgxSpinnerService } from 'ngx-spinner';

export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) { }

  showSpinner(nameType: SpinnerType) {
    this.spinner.show(nameType);

    setTimeout(() => this.hideSpinner(nameType), 1000);
  }

  hideSpinner(nameType: SpinnerType) {
    this.spinner.hide(nameType);
  }
}


export enum SpinnerType {
  BallAtom= "s1",
  BallScaleMultiple="s2",
  BallSpinClockwiseFadeRotating="s3"
}
