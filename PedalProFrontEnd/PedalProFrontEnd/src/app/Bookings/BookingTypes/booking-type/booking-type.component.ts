import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-booking-type',
  templateUrl: './booking-type.component.html',
  styleUrls: ['./booking-type.component.css']
})
export class BookingTypeComponent {
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){}

  bookingTypes:BookingType[]=[];

  ngOnInit(): void {
    this.GetBookingTypes();
  }
// get booking types method
GetBookingTypes()
  {
    this.service.GetBookingTypes().subscribe(result=>{
      let bookingTypeList:any[]=result
      bookingTypeList.forEach((element)=>{
        this.bookingTypes.push(element)
      });
    })
    return this.bookingTypes;
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

// delete method
  DeleteBookingType(id:any)
  { 
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this booking type?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) { 
        this.service.DeleteBookingType(id).subscribe({
          next:(response)=>{
            
            const index=this.bookingTypes.findIndex((bookingType)=>bookingType.bookingTypeId===id);
            if(index!=-1){
              this.bookingTypes.slice(index,1);
            }
            this.openModal();
          },
          error:(err)=> {
            const errorMessage = err.error || 'An error occurred';
            this.openErrorDialog(errorMessage);
          }
        })
      }
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

  
  cancel_continue(){
    this.router.navigate(['viewBookingType'])
  }

  Logout()
  {
    this.service.logout();
  }
}
