import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Location } from '@angular/common';


@Component({
  selector: 'app-reset',
  templateUrl: './reset.component.html',
  styleUrls: ['./reset.component.css']
})
export class ResetComponent implements OnInit{
  resetPasswordForm!: FormGroup;
  submitted = false;
  errorMessage: string = "";

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private service: PedalProServiceService,private location: Location
  ) { }

  ngOnInit(): void {
    this.resetPasswordForm = this.formBuilder.group({
      code: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    }, {
      validator: this.passwordMatchValidator
    });
  }

  get f() { return this.resetPasswordForm.controls; }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')!.value;
    const confirmPassword = formGroup.get('confirmPassword')!.value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword')!.setErrors({ passwordMismatch: true });
    } else {
      formGroup.get('confirmPassword')!.setErrors(null);
    }
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.resetPasswordForm.invalid) {
      return;
    }

    this.service.resetPassword(this.resetPasswordForm.value)
      .subscribe(
        response => {
          // Password reset successful, redirect to login page or any other appropriate page
          this.router.navigate(['/login']);
        },
        error => {
          this.errorMessage = error.error;
        }
      );
  }

  goBack(): void {
    this.location.back();
  }
}
