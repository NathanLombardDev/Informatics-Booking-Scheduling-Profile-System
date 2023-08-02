import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { WorkoutType } from 'src/app/Models/workout-type';

@Component({
  selector: 'app-workout-type',
  templateUrl: './workout-type.component.html',
  styleUrls: ['./workout-type.component.css']
})
export class WorkoutTypeComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}

  clientTypes:WorkoutType[]=[];

  ngOnInit(): void {
    this.GetEmpTypes();
  }
// get employee types method
  GetEmpTypes()
  {
    this.service.GetWorkoutTypes().subscribe(result=>{
      let clientTypeList:any[]=result
      clientTypeList.forEach((element)=>{
        this.clientTypes.push(element)
      });
    })
    return this.clientTypes;
  }
// delete method
  DeleteEmpType(id:any)
  {
    this.service.DeleteWorkoutType(id).subscribe({
      next:(response)=>{
        
        const index=this.clientTypes.findIndex((clientType)=>clientType.workoutTypeId===id);
        if(index!=-1){
          this.clientTypes.slice(index,1);
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
    this.router.navigate(['WorkoutType'])
  }

  Logout()
  {
    this.service.logout();
  }
}
