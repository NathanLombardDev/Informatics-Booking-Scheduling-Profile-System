import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BookingType } from 'src/app/Models/booking-type';
import { of } from 'rxjs';
import { Testingdate } from 'src/app/Models/testingdate';
import { TrainingModule } from 'src/app/Models/training-module';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/Dialogs/confirmation-dialog/confirmation-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';



@Component({
  selector: 'app-viewbookings',
  templateUrl: './viewbookings.component.html',
  styleUrls: ['./viewbookings.component.css']
})
export class ViewbookingsComponent {
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog: MatDialog){}
  category:BookingType[]=[];
  clientTypes:any[]=[];
  datedatedates:Testingdate[]=[];
  modules:TrainingModule[]=[];
  clientDetails: any;
  reportDatatwo: any[]=[];
  cartnumber:any;
  bookingdate:any;
  bookingtime:any;

  ngOnInit(): void {
    this.GetBookings();
    this.GetModules();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
  }
// get employee types method
  GetBookings()
  {
    this.service.getClientBookings().subscribe(result=>{
      let clientTypeList:any[]=result
      clientTypeList.forEach((element)=>{
        this.service.getDateandTimeslot(element.scheduleId).subscribe(nullDate=>{
          element.dateDate=nullDate.dateDate;
          element.timeTime=nullDate.timeTime;
          this.clientTypes.push(element)
        });
      });
    })
    return this.clientTypes;
  }

  fetchClientDetails() {
    this.service.getClientDetails().subscribe(
      (response) => {
        this.clientDetails = response;
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }
  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  ReloadPage()
  {
    location.reload();
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

  onCancelBooking(bookingId: number, reason: string): void {
    this.service.cancelBooking(bookingId, reason).subscribe(
      response => {
        // Handle success response
        console.log('Booking canceled:', response);
      },
      error => {
        // Handle error
        const errorMessage=error.error;
        this.openErrorDialog(errorMessage);
      }
    );
  }

  openConfirmationDialog(bookingId: number): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '300px',
      data: { reason: '' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.onCancelBooking(bookingId, result);
      } else {
        // User canceled the dialog
      }
    });
  }
  
  cancel_continue(){
    this.router.navigate(['ClientType'])
  }

  GetType(id: any) {
    const categories = this.category.find(m => m.bookingTypeId === id);
  
    if (categories) {
      return categories.bookingTypeName;
    } else {
      this.service.GetBookingType(id).subscribe(result => {
      
        
        this.category.push(result);
        return result.bookingTypeName;
      });
    }

    
  
    // add a return statement here to handle the case where the module is not found
    return 'Booking Type does not exist';
  }

  GetDateDate(id: any): void {
    const gummies = this.clientTypes.find((clientType) => clientType.scheduleId === id);

    if (gummies) {
      return gummies.date1;
    } else {
      this.service.GetDateDate(id).subscribe(result => {
        this.datedatedates.push(result);
        return result.date1;
      });
    }

  }

  Logout()
  {
    this.service.logout();
  }

  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }
}
