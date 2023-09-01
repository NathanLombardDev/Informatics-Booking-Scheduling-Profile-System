import { Component ,OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Booking } from 'src/app/Models/booking';
import { Router } from '@angular/router';
import { BookingType } from 'src/app/Models/booking-type';
import { TrainingModule } from 'src/app/Models/training-module';
import { ComplexBooking } from 'src/app/Models/complex-booking';
import { Bicycle } from 'src/app/Models/bicycle';
import { BicyclePart } from 'src/app/Models/bicycle-part';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-make-booking',
  templateUrl: './make-booking.component.html',
  styleUrls: ['./make-booking.component.css']
})
export class MakeBookingComponent implements OnInit{
  constructor(private dialog:MatDialog,private route:ActivatedRoute, private service:PedalProServiceService,private router:Router){}

  message: string="";
  clientDetails: any;
  cartnumber:any;
  
  schedule: ComplexBooking = {
    timeslotId: 0, // Provide appropriate values here
    bookingTypeId: 0,
    bicycleId: 0,
    description: '',
    bicyclePartId:0,
    bookingId:0
  };
  

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  public g=0;
  BookingTypes:BookingType[]=[];
  bicycles:Bicycle[]=[];
  bicycleParts:BicyclePart[]=[];
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
    this.GetBicycles();
    this.GetBikeParts();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
  }

  addBooking:Booking={
    bookingId:0,
    //scheduleId:0,
    bookingTypeId:0,
    timeslotId:0,
    //bookingStatusId:0,
    //referenceNum:"",
    //clientId:0
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

  makeBooking() {
    if (this.addBooking.bookingTypeId) {
      // Update timeslotId here with the current value of g
      this.addBooking.timeslotId = this.g;
      console.log(this.addBooking.timeslotId);
      this.service.AddBooking(this.addBooking).subscribe({
        next: (course) => {
          //window.location.href=course.paymentUrl;
          //this.openModal();
          localStorage.setItem('bookingId', course.bookingId.toString());
        },
        error:(err)=> {
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });

      this.service.getbookingcheckouturl(this.addBooking.bookingTypeId).subscribe(
        response=>{
          window.location.href=response.paymentUrl;
        }
      );

      
    } else {
      this.openErrorDialog('Validation error: Please fill in all fields.');
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

  GetBikeParts()
  {
    this.service.GetBicycleParts().subscribe(result=>{
      let bikePartList:any[]=result
      bikePartList.forEach((element)=>{
        this.bicycleParts.push(element)
      });
    })
    return this.bicycleParts;
  }

  addBookingtwo(): void {
    if (this.schedule.bookingTypeId&& this.schedule.bicycleId&&this.schedule.bicyclePartId&&this.schedule.description) {
      // Update timeslotId here with the current value of g
      this.schedule.timeslotId = this.g;
      console.log(this.schedule.timeslotId);
      this.service.addBookingtwo(this.schedule).subscribe({
        next: (course) => {
          //window.location.href=course.paymentUrl;
          //this.openModal();
          localStorage.setItem('bookingId', course.bookingId.toString());
          
        },
        error:(err)=> {
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });

      this.service.getbookingcheckouturl(this.schedule.bookingTypeId).subscribe(
        response=>{
          window.location.href=response.paymentUrl;
        }
      );
        
      
    } else {
      this.openErrorDialog('Validation error: Please fill in all fields.');
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

  GetBicycles()
  {
    this.service.GetBicycles().subscribe(result=>{
      let bicycleList:any[]=result
      bicycleList.forEach((element)=>{
        this.bicycles.push(element)
      });
    })
    
    return this.bicycles;
    
  }
}
