import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { Workout } from 'src/app/Models/workout';
import { WorkoutType } from 'src/app/Models/workout-type';

@Component({
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.css']
})
export class WorkoutComponent implements OnInit{

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  modules:TrainingModule[]=[];
  
  //bicycles:Bicycle[]=[];
  //category:BicycleCategory[]=[];
  workoutType:WorkoutType[]=[];

  workouts:Workout[]=[];


  numNum:number=0;

  incrementCounter() {
    if (this.numNum < this.workouts.length) {
      this.numNum++;
    }
  }
  

  ngOnInit(): void {
    this.GetWorkouts();
    this.GetModules();
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
// get bicycles method
  GetWorkouts()
  {
    this.service.GetWorkouts().subscribe(result=>{
      let workoutList:any[]=result
      workoutList.forEach((element)=>{
        this.workouts.push(element)
      });
    })
    console.log(this.workouts)
    return this.workouts;
    
  }

  GetWorkoutType(id: any) {
    const categories = this.workoutType.find(m => m.workoutTypeId === id);
  
    if (categories) {
      return categories.workoutTypeName;
    } else {
      this.service.GetWorkoutType(id).subscribe(result => {
        this.workoutType.push(result);
        return result.workoutTypeName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Type does not exist';
  }
  

  

  DeleteWorkout(id:any)
  {
    this.service.DeleteWorkout(id).subscribe({
      next:(response)=>{
        
        const index=this.workouts.findIndex((workout)=>workout.workoutId===id);
        if(index!=-1){
          this.workouts.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  ReloadPage()
  {
    location.reload();
  }
// pop-up modal
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  convertDurationFromTimespan(timespanString: string): string {
    const [hours, minutes, seconds] = timespanString.split(':');
    return `${hours}h ${minutes}m ${seconds}s`;
  }

  Logout()
  {
    this.service.logout();
  }
}
