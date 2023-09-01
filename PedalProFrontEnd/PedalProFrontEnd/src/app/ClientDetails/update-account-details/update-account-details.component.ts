import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { UserViewModel } from 'src/app/Models/user-view-model';// Import the UserViewModel interface or model as needed
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-update-account-details',
  templateUrl: './update-account-details.component.html',
  styleUrls: ['./update-account-details.component.css']
})
export class UpdateAccountDetailsComponent {
  userDetails: UserViewModel = {
    emailAddress: '',
    password: '',
    clientName: '',
    clientSurname: '',
    clientDateOfBirth: new Date(),
    clientPhoneNum:'',
    clientPhysicalAddress:'',
    clientTitle:''
  };
  loading: boolean = false;
  constructor(private dialog:MatDialog,private userService: PedalProServiceService,private router:Router) { }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  onSubmit(): void {
    this.userService.updateDetails(this.userDetails).subscribe(
      (response) => {
        // Handle the response after successful update
        console.log('User details updated:', response);
        //this.router.navigate(['/ViewAccount']); // Redirect to protected page
        this.openModal();
      },
      (error) => {
        const errorMessage = error.error || 'An error occurred';
        this.openErrorDialog(errorMessage); // Call a method to open error dialog with the error message
      }
    );
  }

  Logout()
  {
    this.userService.logout();
  }

  
  cancel_continue(){
    this.router.navigate(['ViewAccount'])
  }


// modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
}
