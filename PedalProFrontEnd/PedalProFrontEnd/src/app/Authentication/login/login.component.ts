import { Component } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private service: PedalProServiceService, private router:Router) {}

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
        // Handle login error (e.g., show an error message to the user)
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
  
  RegisterPage()
  {
    this.router.navigate(['/register']); // Redirect to the register page
  }
}
