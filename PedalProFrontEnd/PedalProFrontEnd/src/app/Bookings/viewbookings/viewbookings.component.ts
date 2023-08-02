import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BookingType } from 'src/app/Models/booking-type';
import { of } from 'rxjs';
import { Testingdate } from 'src/app/Models/testingdate';
import { TrainingModule } from 'src/app/Models/training-module';


@Component({
  selector: 'app-viewbookings',
  templateUrl: './viewbookings.component.html',
  styleUrls: ['./viewbookings.component.css']
})
export class ViewbookingsComponent {
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  category:BookingType[]=[];
  clientTypes:any[]=[];
  datedatedates:Testingdate[]=[];
  modules:TrainingModule[]=[];

  ngOnInit(): void {
    this.GetBookings();
    this.GetModules();
  }
// get employee types method
  GetBookings()
  {
    this.service.getClientBookings().subscribe(result=>{
      let clientTypeList:any[]=result
      clientTypeList.forEach((element)=>{
        this.clientTypes.push(element)
      });
    })
    return this.clientTypes;
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
