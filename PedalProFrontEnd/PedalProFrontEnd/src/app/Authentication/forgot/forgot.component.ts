import { Component,OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

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

  constructor(private formBuilder: FormBuilder, private service: PedalProServiceService, private route:Router,private dialog:MatDialog) { }

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

          this.route.navigate(['/reset']); // Redirect to protected page
          
        },
        error => {
          const errorsMessage = error.error;
          console.log(error);
          this.openErrorDialog(errorsMessage);
        }
      );
      
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
}
