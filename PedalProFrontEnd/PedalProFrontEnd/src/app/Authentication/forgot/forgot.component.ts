import { Component,OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css']
})
export class ForgotComponent implements OnInit{
  forgotPasswordForm!: FormGroup;
  submitted = false;
  successMessage: string="";
  errorMessage: string="";

  constructor(private formBuilder: FormBuilder, private service: PedalProServiceService, private route:Router) { }

  ngOnInit(): void {
    this.forgotPasswordForm = this.formBuilder.group({
      emailAddress: ['', [Validators.required, Validators.email]]
    });
  }

  get f() { return this.forgotPasswordForm.controls; }

  onSubmit(): void {
    this.submitted = true;

    if (this.forgotPasswordForm.invalid) {
      return;
    }

    this.service.forgotPassword(this.forgotPasswordForm.controls['emailAddress'].value)
      .subscribe(
        response => {
          this.successMessage = response;
          this.errorMessage = "";

          
        },
        error => {
          this.errorMessage = error.error;
          this.successMessage = "";
        }
      );
      this.route.navigate(['/reset']); // Redirect to protected page
  }
}
