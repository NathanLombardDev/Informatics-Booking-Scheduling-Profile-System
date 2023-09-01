import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/Dialogs/confirmation-dialog/confirmation-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';


@Component({
  selector: 'app-success-checkout',
  templateUrl: './success-checkout.component.html',
  styleUrls: ['./success-checkout.component.css']
})
export class SuccessCheckoutComponent implements OnInit{

  cartId=0;

  disableBackButton: boolean = true;

  constructor(private dialog: MatDialog,private service:PedalProServiceService,private router:Router){

  }

  ngOnInit(): void {
    
    this.cartId = parseInt(localStorage.getItem('cartId') || '0', 10);
    
    const token = this.service.getToken();

    if (token && this.service.isTokenValid(token)) {
      // Token is valid, user is authenticated
      // Display payment success content
      this.savePayment();
      
    } else {
      // Token is invalid or missing, handle accordingly
    }

    history.pushState({}, '', window.location.href);
    window.onpopstate = (event) => {
      if (this.disableBackButton) {
        event.preventDefault();
      }
    };

    this.preventBackButton();
    
    
    
  }

  preventBackButton(): void {
    history.replaceState(null, document.title, location.href);
    window.addEventListener('popstate', () => {
      history.pushState(null, document.title, location.href);
    });
  }

  Logout()
  {
    this.service.logout();
  }

  cancel_continue(){
    this.router.navigate(['clientLanding']);
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      localStorage.setItem('cartId', '0');
      localStorage.setItem('cartQuantity', '0');
      modelDiv.style.display='block';
    }
  }

  savePayment(): void {
    this.service.savePayment(this.cartId).subscribe(
      response => {
        // Handle the successful response, e.g., show a success message
        console.log('Payment saved successfully:', response);
        this.openModal();
      },
      error => {
        const errorMessage=error.error;
        this.openErrorDialog(errorMessage);
      }
    );
  }

}
