import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { UserViewModel } from 'src/app/Models/user-view-model';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user: UserViewModel = {
    clientName:"",
    clientTitle:"",
    clientSurname:"",
    clientDateOfBirth: new Date(), // Initialize with the current date or a specific default date
    clientPhoneNum:"",
    clientPhysicalAddress:"",
    password:"",
    emailAddress:""
  }
  loading: boolean = false;
  

  constructor(private dialog:MatDialog,private service: PedalProServiceService, private router:Router,private datePipe: DatePipe) {
    
   }

   getCurrentDate(): string {
    // Get the current date and format it as 'yyyy-MM-dd'
    return new Date().toISOString().split('T')[0];
  }

  getTwoYearsAgo(): string {
    const currentDate = new Date();
    currentDate.setFullYear(currentDate.getFullYear() - 2);
    
    // Format the date as 'yyyy-MM-dd'
    return currentDate.toISOString().split('T')[0];
  }

  registerUser(): void {
    this.loading = true;
    this.service.registerUser(this.user).subscribe(
      (response) => {
        console.log('Registration successful:', response);
        this.loading = false;
        this.router.navigate(['/login']);
      },
      (error) => {
        this.loading = false;
        const errorMessage = error.error || 'An error occurred';

        this.openErrorDialog(errorMessage); 
      }
    );
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
}
