import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { UserViewModel } from 'src/app/Models/user-view-model';// Import the UserViewModel interface or model as needed
import { Router } from '@angular/router';

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
    // Initialize other properties as needed
  };
  loading: boolean = false;
  constructor(private userService: PedalProServiceService,private router:Router) { }

  onSubmit(): void {
    this.userService.updateDetails(this.userDetails).subscribe(
      (response) => {
        // Handle the response after successful update
        console.log('User details updated:', response);
        this.router.navigate(['/ViewAccount']); // Redirect to protected page
      },
      (error) => {
        // Handle any errors during the update process
        console.error('Error updating user details:', error);
      }
    );
  }
}
