import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { User } from 'src/app/entities/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  frm: FormGroup;
  submitted: boolean = false;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      adSoyad: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(2)
      ]],
      kullaniciAdi: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(2)
      ]],
      email: ["", [
        Validators.required,
        Validators.maxLength(150),
        Validators.email
      ]],
      sifre: ["", [
        Validators.required,
        // Validators.maxLength(50),
        // Validators.minLength(2)
      ]],
      sifreTekrar: ["", [
        Validators.required,
        // Validators.maxLength(50),
        // Validators.minLength(2)
      ]],
    }, {
      validators: (group: AbstractControl): ValidationErrors | null => {
        let sifre = group.get("sifre").value;
        let sifreTekrar = group.get("sifreTekrar").value;
        return sifre === sifreTekrar ? null : { notSame: true };
      }
    })
  }

  // property mantığı, fonksiyon değil.
  get component() {
    return this.frm.controls;
  }

  onSubmit(data: User) {
    this.submitted = true;
    if (this.frm.invalid)
      return;
    // console.log(data)
  }

}
