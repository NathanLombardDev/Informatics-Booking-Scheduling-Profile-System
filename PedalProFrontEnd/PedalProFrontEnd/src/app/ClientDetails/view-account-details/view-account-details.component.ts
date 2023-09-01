import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ClientClient } from 'src/app/Models/client-client';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-view-account-details',
  templateUrl: './view-account-details.component.html',
  styleUrls: ['./view-account-details.component.css']
})
export class ViewAccountDetailsComponent implements OnInit{
  clientDetails: any;


  
  constructor(private dialog:MatDialog,private http: HttpClient,private service:PedalProServiceService,private router:Router) {}

  ngOnInit() {
    this.fetchClientDetails();
  }

  fetchClientDetails() {
    this.service.getClientDetails().subscribe(
      (response) => {
        this.clientDetails = response;
      },
      (error) => {
        const errorMessage = error.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  openEditDialog(){
    
    this.router.navigate(['/UpdateAccount']); // Redirect to company side code
  }

  deactivateAccount(): void {
    this.service.deactivateAccount().subscribe(
      response => {
        // Handle success
        console.log(response);
        this.openModal();
        
      },
      error => {
        const errorMessage = error.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }

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

  cancel_continue(){
    this.Logout();
  }
}
