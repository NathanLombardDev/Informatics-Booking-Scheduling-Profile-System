import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Booking } from 'src/app/Models/booking';
import { Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { TrainingModule } from 'src/app/Models/training-module';

@Component({
  selector: 'app-make-booking',
  templateUrl: './make-booking.component.html',
  styleUrls: ['./make-booking.component.css']
})
export class MakeBookingComponent implements OnInit{
  constructor(private route:ActivatedRoute, private service:PedalProServiceService,private router:Router){}

  public g=0;
  BookingTypes:BookingType[]=[];
  modules:TrainingModule[]=[];

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.g=+id;
        }
      }
    })
    console.log(this.g);
    this.GetRoles();
    this.GetModules();
  }

  addBooking:Booking={
    //bookingId:0,
    //scheduleId:0,
    bookingTypeId:0,
    timeslotId:0,
    //bookingStatusId:0,
    //referenceNum:"",
    //clientId:0
  }

  makeBooking() {
    if (this.addBooking.bookingTypeId) {
      // Update timeslotId here with the current value of g
      this.addBooking.timeslotId = this.g;
      console.log(this.addBooking.timeslotId);
      this.service.AddBooking(this.addBooking).subscribe({
        next: (course) => {
          this.openModal();
        }
      });
    } else {
      alert('Validation error: Please fill in all fields.');
    }
  }
  cancel_continue(){
    this.router.navigate(['/calendar']);
  }

  //Notification
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  GetRoles(){
    this.service.GetBookingTypes().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.BookingTypes.push(element)
      });
    })
    return this.BookingTypes;
  }

  Logout()
  {
    this.service.logout();
  }

  // get modules method
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
