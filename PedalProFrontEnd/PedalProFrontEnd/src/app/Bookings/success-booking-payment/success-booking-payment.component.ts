import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/Dialogs/confirmation-dialog/confirmation-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-success-booking-payment',
  templateUrl: './success-booking-payment.component.html',
  styleUrls: ['./success-booking-payment.component.css']
})
export class SuccessBookingPaymentComponent implements OnInit{

  bookingId=0;
  disableBackButton: boolean = true;
  constructor(private service:PedalProServiceService,private router:Router,private dialog: MatDialog){

  }
  
  ngOnInit(): void {
    this.bookingId = parseInt(localStorage.getItem('bookingId') || '0', 10);

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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
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

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      localStorage.setItem('bookingId', '0');
      
      modelDiv.style.display='block';
    }
  }

  savePayment(): void {
    this.service.saveBookingPayment(this.bookingId).subscribe(
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
