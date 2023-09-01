import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleCategory } from '../../Models/bicycle-category';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-bicycle-category',
  templateUrl: './add-bicycle-category.component.html',
  styleUrls: ['./add-bicycle-category.component.css']
})
export class AddBicycleCategoryComponent {
  constructor(private dialog:MatDialog,private dataService:PedalProServiceService,private router:Router) { }

  addBicycleCat:BicycleCategory={
    bicycleCategoryId:0,
    bicycleCategoryName:''
  }

  ngOnInit(): void {
    
  }

  
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }


  AddBicycleCat(){
    if(this.addBicycleCat.bicycleCategoryName)
    {
      this.dataService.AddBicycleCategory(this.addBicycleCat).subscribe({
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
  cancel_continue(){
    this.router.navigate(['BicycleCategory']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
