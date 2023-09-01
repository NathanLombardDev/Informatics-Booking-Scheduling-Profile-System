import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicyclePart } from '../../Models/bicycle-part';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-bicycle-part',
  templateUrl: './add-bicycle-part.component.html',
  styleUrls: ['./add-bicycle-part.component.css']
})
export class AddBicyclePartComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataService:PedalProServiceService,private router:Router) { }

  addBicyclePart:BicyclePart={
    bicyclePartId:0,
    bicyclePartName:''
  }

  ngOnInit(): void {
    
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  //Modal Pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  //Add bicycle part 
  AddBicyclePart(){
    if(this.addBicyclePart.bicyclePartName)
    {
      this.dataService.AddBicyclePart(this.addBicyclePart).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        },
        error:(err)=> {
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });
    }else{
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
  }
  //Cancel button
  cancel_continue(){
    this.router.navigate(['BicyclePart']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
