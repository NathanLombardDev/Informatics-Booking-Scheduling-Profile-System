import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { Workout } from 'src/app/Models/workout';
import { WorkoutType } from 'src/app/Models/workout-type';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.css']
})
export class WorkoutComponent implements OnInit{

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){}
  modules:TrainingModule[]=[];
  clientDetails: any;
  cartnumber:any;
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
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

  ngOnInit(): void {
    this.GetWorkouts();
    this.GetModules();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
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
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this workout?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) { 
        this.service.DeleteWorkout(id).subscribe({
          next:(response)=>{
            
            const index=this.workouts.findIndex((workout)=>workout.workoutId===id);
            if(index!=-1){
              this.workouts.slice(index,1);
            }
            this.openModal();
            
          },
          error:(err)=>{
            const errorMessage = err.error || 'An error occurred';
            this.openErrorDialog(errorMessage);
          }
        })
       }
    });
    
    
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
