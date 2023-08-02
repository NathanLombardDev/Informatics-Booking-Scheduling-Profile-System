import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { WorkoutType } from 'src/app/Models/workout-type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-workout-type',
  templateUrl: './add-workout-type.component.html',
  styleUrls: ['./add-workout-type.component.css']
})
export class AddWorkoutTypeComponent implements OnInit{
  constructor(private dataService:PedalProServiceService,private router:Router) { }

  addClientTypes:WorkoutType={
    workoutTypeId:0,
    workoutTypeName:''
  }

  ngOnInit(): void {
    
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

// add employee type modal
  addEmpType(){
    if(this.addClientTypes.workoutTypeName)
    {
      this.dataService.AddWorkoutType(this.addClientTypes).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['WorkoutType']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
