import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Package } from '../../Models/package';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';
@Component({
  selector: 'app-edit-package',
  templateUrl: './edit-package.component.html',
  styleUrls: ['./edit-package.component.css']
})
export class EditPackageComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){}


  editPackages:Package={
    packageId:0,
    packageName:'',
    packageDescription:'',
    price1:0,
    numPackageBookings:0
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }


  UpdatePackage(){
    if (this.editPackages.packageName && this.editPackages.packageDescription && this.editPackages.price1 && this.editPackages.numPackageBookings)
    {
      this.dataservice.EditPackage(this.editPackages.packageId,this.editPackages).subscribe({
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
    this.router.navigate(['ViewPackages'])
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');
        if(id)
        {
          this.dataservice.GetPackage(id).subscribe({
            next:(response)=>{
              this.editPackages=response;
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

  Logout()
  {
    this.dataservice.logout();
  }
}
