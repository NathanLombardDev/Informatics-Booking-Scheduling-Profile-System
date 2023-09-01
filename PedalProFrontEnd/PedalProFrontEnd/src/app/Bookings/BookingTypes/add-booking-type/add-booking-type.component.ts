import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-booking-type',
  templateUrl: './add-booking-type.component.html',
  styleUrls: ['./add-booking-type.component.css']
})
export class AddBookingTypeComponent {
  constructor(private dialog:MatDialog,private dataService:PedalProServiceService,private router:Router) { }

  /*openSnackBar() {
    this._snackBar.open(this.displayMessage, 'Close', {
      duration: 3000
    });
  }*/

  addBookingTypes:BookingType={
    bookingTypeId:0,
    bookingTypeName:'',
    price:0
  }

  ngOnInit(): void {
    
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
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

// add booking type modal
AddBookingType(){
    if(this.addBookingTypes.bookingTypeName)
    {
      this.dataService.AddBookingTypes(this.addBookingTypes).subscribe({
        next:(course)=>{
          this.openModal();
        },
        error:(err)=> {
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });
    }else{
      /*this.displayMessage = "Please fill in all the required fields.";
      this.openSnackBar();*/
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['viewBookingType']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
