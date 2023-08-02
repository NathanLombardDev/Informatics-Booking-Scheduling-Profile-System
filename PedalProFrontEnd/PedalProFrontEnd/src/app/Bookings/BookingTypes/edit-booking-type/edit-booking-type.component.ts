import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
@Component({
  selector: 'app-edit-booking-type',
  templateUrl: './edit-booking-type.component.html',
  styleUrls: ['./edit-booking-type.component.css']
})
export class EditBookingTypeComponent {
  constructor(private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){

  }



  editBookingTypes:BookingType={
    bookingTypeId:0,
    bookingTypeName:''
  }

  

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.dataservice.GetBookingType(id).subscribe({
            next:(response)=>{
              this.editBookingTypes=response;
            }
          })

        }
      }
    })


  }

  //update function
  EditBookingType(){
    if(this.editBookingTypes.bookingTypeName)
    {
      this.dataservice.EditBookingType(this.editBookingTypes.bookingTypeId,this.editBookingTypes).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['viewBookingType'])
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
