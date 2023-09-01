import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Package } from '../../Models/package';
import { Router } from '@angular/router';
import { Price } from '../../Models/price';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-package',
  templateUrl: './add-package.component.html',
  styleUrls: ['./add-package.component.css']
})
export class AddPackageComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService,private router:Router){}


  addPackages:Package={
    packageId:0,
    packageName:'',
    packageDescription:'',
    price1:0,
    numPackageBookings:0
  }

  ngOnInit(): void {
    
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  addPackage(){
    if (this.addPackages.packageName && this.addPackages.packageDescription && this.addPackages.price1&&this.addPackages.numPackageBookings) 
    {
      this.dataservice.AddPackage(this.addPackages).subscribe({
        next:(course)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });
    }else {
      // Display a message asking for all fields to be filled in
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['ViewPackages']);
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
    this.dataservice.logout();
  }
}
