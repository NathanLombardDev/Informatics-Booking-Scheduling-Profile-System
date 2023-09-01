import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service'; 
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';


@Component({
  selector: 'app-reactivate-account',
  templateUrl: './reactivate-account.component.html',
  styleUrls: ['./reactivate-account.component.css']
})
export class ReactivateAccountComponent {
  email: string = '';
  password: string = '';
  constructor(private service: PedalProServiceService, private router:Router,private dialog:MatDialog) {}

  onLogin(): void {
    this.service.reactivateAccount(this.email, this.password).subscribe(
      (response) => {
        this.openModal();
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

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }

    
  }

  cancel_continue(){
    this.router.navigate(['login']);
  }
}
