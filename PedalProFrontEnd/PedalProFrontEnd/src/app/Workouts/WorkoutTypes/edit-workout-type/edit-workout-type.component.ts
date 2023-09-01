import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { WorkoutType } from 'src/app/Models/workout-type';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-edit-workout-type',
  templateUrl: './edit-workout-type.component.html',
  styleUrls: ['./edit-workout-type.component.css']
})
export class EditWorkoutTypeComponent implements OnInit{
  constructor(private dialog:MatDialog,private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

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
            },
            error:(err)=>{
              const errorMessage = err.error || 'An error occurred';
              this.openErrorDialog(errorMessage);
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
// edit emp type modal
  EditEmpType(){
    if(this.editClientTypes.workoutTypeName)
    {
      this.service.EditWorkoutType(this.editClientTypes.workoutTypeId,this.editClientTypes).subscribe({
        next:(response)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      })
    }else{
      this.openErrorDialog('Validation error: Please fill in all fields.');
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
