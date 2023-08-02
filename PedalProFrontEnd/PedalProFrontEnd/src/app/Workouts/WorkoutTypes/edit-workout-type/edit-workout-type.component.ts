import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { WorkoutType } from 'src/app/Models/workout-type';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-workout-type',
  templateUrl: './edit-workout-type.component.html',
  styleUrls: ['./edit-workout-type.component.css']
})
export class EditWorkoutTypeComponent implements OnInit{
  constructor(private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

  editClientTypes:WorkoutType={
    workoutTypeId:0,
    workoutTypeName:'',
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.service.GetWorkoutType(id).subscribe({
            next:(response)=>{
              this.editClientTypes=response;
            }
          })
        }
      }
    })
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
// edit emp type modal
  EditEmpType(){
    if(this.editClientTypes.workoutTypeName)
    {
      this.service.EditWorkoutType(this.editClientTypes.workoutTypeId,this.editClientTypes).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
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
