import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { UserViewModel } from 'src/app/Models/user-view-model';
import { Router } from '@angular/router';

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

  constructor(private service: PedalProServiceService, private router:Router) { }

  registerUser(): void {
    this.loading = true;
    this.service.registerUser(this.user).subscribe(
      (response) => {
        // Registration successful, handle the response as needed
        console.log('Registration successful:', response);
        // Optionally, you can navigate to a success page or show a success message
        this.loading = false;
        this.router.navigate(['/login']); // Redirect to protected page
      },
      (error) => {
        console.error('Registration failed:', error);
        // Handle registration error (e.g., show an error message to the user)
        this.loading = false;
      }
    );
  }
}
