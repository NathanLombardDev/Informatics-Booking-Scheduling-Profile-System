import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { DateWithTimeslotDto } from 'src/app/Models/date-with-timeslot-dto';

@Component({
  selector: 'app-viewtimeslots',
  templateUrl: './viewtimeslots.component.html',
  styleUrls: ['./viewtimeslots.component.css']
})
export class ViewtimeslotsComponent implements OnInit{
  timeslots:DateWithTimeslotDto[]=[];

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  ngOnInit(): void {
    this.GetBikeParts();
  }
  GetBikeParts()
  {
    this.service.getTimeslotsSlots().subscribe(result=>{
      let bikePartList:any[]=result
      bikePartList.forEach((element)=>{
        this.timeslots.push(element)
      });
    })
    return this.timeslots;
  }

  //Delete Bicycle
  DeleteBikePart(id:any)
  {
    this.service.DeleteTimeslot(id).subscribe({
      next:(response)=>{
        
        const index=this.timeslots.findIndex((bicyclePart)=>bicyclePart.timeslotId===id);
        if(index!=-1){
          this.timeslots.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  //Refresh the page
  ReloadPage()
  {
    location.reload();
  }


  //Modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  //  Cancel button
  cancel_continue(){
    this.router.navigate(['/Viewtimeslot'])
  }

  Logout()
  {
    this.service.logout();
  }
}
