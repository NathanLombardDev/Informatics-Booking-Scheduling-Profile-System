import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Bicycle } from '../../Models/bicycle';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { TrainingModule } from '../../Models/training-module';
import { BicycleCategory } from '../../Models/bicycle-category';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { WorkoutType } from 'src/app/Models/workout-type';
import { Workout } from 'src/app/Models/workout';

@Component({
  selector: 'app-add-workout',
  templateUrl: './add-workout.component.html',
  styleUrls: ['./add-workout.component.css']
})
export class AddWorkoutComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  
  ngOnInit(): void {
    this.GetModules();
    this.GetWorkoutTypes();
  }
  
  addBicycles:Bicycle={
    bicycleId:0,
    bicycleName:'',
    bicycleBrandId:0,
    //clientId:1,
    bicycleCategoryId:0
  }

  addworkouts:Workout={
    workoutId:0,
    duration:'',
    distance:0,
    heartRate:0,
    workoutTypeId:0
  }
  
  modules:TrainingModule[]=[];
  //categories:BicycleCategory[]=[];
  //brands:BicycleBrand[]=[];
  workoutTypes:WorkoutType[]=[];
  

  
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


  GetWorkoutTypes(){
    this.service.GetWorkoutTypes().subscribe(result=>{
      let brandList:any[]=result
      brandList.forEach((element)=>{
        this.workoutTypes.push(element)
      });
    })
    return this.workoutTypes;
  }

// add bicycle method
  addBicycle(){
    if(this.addBicycles.bicycleBrandId && this.addBicycles.bicycleCategoryId && this.addBicycles.bicycleName)
    {
      this.service.AddBicycle(this.addBicycles).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
  }

  addWorkout(){
    if(this.addworkouts.workoutTypeId && this.addworkouts.duration && this.addworkouts.distance && this.addworkouts.heartRate)
    {
      this.service.AddWorkout(this.addworkouts).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
  }
  cancel_continue(){
    this.router.navigate(['/Workouts']);
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

  Logout()
  {
    this.service.logout();
  }
}
