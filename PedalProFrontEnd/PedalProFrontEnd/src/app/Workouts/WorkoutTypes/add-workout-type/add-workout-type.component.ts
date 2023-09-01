import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { WorkoutType } from 'src/app/Models/workout-type';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-workout-type',
  templateUrl: './add-workout-type.component.html',
  styleUrls: ['./add-workout-type.component.css']
})
export class AddWorkoutTypeComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataService:PedalProServiceService,private router:Router) { }

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
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });
    }else{
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['WorkoutType']);
  }

  Logout()
  {
    this.dataService.logout();
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
}
