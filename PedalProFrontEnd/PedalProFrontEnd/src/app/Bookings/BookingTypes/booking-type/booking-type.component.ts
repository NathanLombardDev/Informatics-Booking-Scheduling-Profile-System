import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';


@Component({
  selector: 'app-booking-type',
  templateUrl: './booking-type.component.html',
  styleUrls: ['./booking-type.component.css']
})
export class BookingTypeComponent {
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}

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

// delete method
  DeleteBookingType(id:any)
  { if(confirm('Are You Sure You Want To Delete This Record?'))
    this.service.DeleteBookingType(id).subscribe({
      next:(response)=>{
        
        const index=this.bookingTypes.findIndex((bookingType)=>bookingType.bookingTypeId===id);
        if(index!=-1){
          this.bookingTypes.slice(index,1);
        }
        this.openModal();
        
      }
    })
    
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
