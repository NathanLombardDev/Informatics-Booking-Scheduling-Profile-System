import { Component,OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  email: string = '';
  password: string = '';

  constructor(private service: PedalProServiceService, private router:Router,private dialog:MatDialog) {}

  ngOnInit(): void {
    history.pushState({}, '', window.location.href);
    window.onpopstate = function(event) {
      history.go(1);
    };
  }

  onLogin(): void {
    this.service.login(this.email, this.password).subscribe(
      () => {
        // Login successful, token is now saved in local storage
        const token = localStorage.getItem('jwt');
        console.log('Token:', token); // Check if the token is present in the local storage
        const decodedToken = this.decodeToken(token!); // Use non-null assertion here
        console.log('Decoded Token:', decodedToken); // Check the decoded token contents
  
        // Check user's roles and redirect accordingly
        if (decodedToken && decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']) {
          const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
          console.log('User Role:', userRole); // Check the extracted role value
  
          if (userRole === 'Client') {
            this.router.navigate(['/clientLanding']); // Redirect to client code
          } else if (userRole === 'Admin' || userRole === 'Employee') {
            this.router.navigate(['/companyLanding']); // Redirect to company side code
          } else {
            // Handle other roles or no roles
            // Redirect to an appropriate default page or show an error message
          }
        }
      },
      (error) => {
        console.error('Login failed:', error);
        const errorMessage = error.error || 'An error occurred';
        // Handle login error (e.g., show an error message to the user)
        this.openErrorDialog(errorMessage); // Call a method to open error dialog with the error message
      }
    );
  }

  private decodeToken(token: string): any {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  ForgotPage(){this.router.navigate(['/forgot']); }

  ReactivatePage(){this.router.navigate(['/Reactivate']); }
  
  RegisterPage()
  {
    this.router.navigate(['/register']); // Redirect to the register page
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
}
