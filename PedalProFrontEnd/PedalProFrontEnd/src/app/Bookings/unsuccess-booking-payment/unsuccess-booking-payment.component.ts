import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/Dialogs/confirmation-dialog/confirmation-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-unsuccess-booking-payment',
  templateUrl: './unsuccess-booking-payment.component.html',
  styleUrls: ['./unsuccess-booking-payment.component.css']
})
export class UnsuccessBookingPaymentComponent implements OnInit{
  
  constructor(private service:PedalProServiceService,private router:Router,private dialog: MatDialog){}
  disableBackButton: boolean = true;
  bookingId:any;

  ngOnInit(): void {
    // Call the deleteBooking method here with the desired bookingId
    this.bookingId = parseInt(localStorage.getItem('bookingId') || '0', 10);
    const token = this.service.getToken();

    if (token && this.service.isTokenValid(token)) {
      // Token is valid, user is authenticated
      // Display payment success content
      this.deleteBooking();
    } else {
      // Token is invalid or missing, handle accordingly
    }

    // Disable back button when entering the page
    
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

  deleteBooking() {
    this.service.deleteBooking(this.bookingId).subscribe(
      () => {
        console.log('Booking deleted successfully.');
        // Perform any additional actions or updates here
        this.openModal();
      },
      (error) => {
        const errorMessage=error.error;
        this.openErrorDialog(errorMessage);
      }
    );
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
      modelDiv.style.display='block';
    }
  }
}
