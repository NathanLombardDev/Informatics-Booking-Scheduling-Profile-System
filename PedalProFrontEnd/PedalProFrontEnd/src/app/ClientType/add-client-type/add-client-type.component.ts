import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ClientType } from '../../Models/client-type';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-client-type',
  templateUrl: './add-client-type.component.html',
  styleUrls: ['./add-client-type.component.css']
})
export class AddClientTypeComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataService:PedalProServiceService,private router:Router) { }

  addClientTypes:ClientType={
    clientTypeId:0,
    clientTypeName:''
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
    if(this.addClientTypes.clientTypeName)
    {
      this.dataService.AddClientType(this.addClientTypes).subscribe({
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
  cancel_continue(){
    this.router.navigate(['ClientType']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
