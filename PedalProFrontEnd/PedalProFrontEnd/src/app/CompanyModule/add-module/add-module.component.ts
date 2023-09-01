import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { TrainingMaterial } from '../../Models/training-material';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { Package } from '../../Models/package';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-module',
  templateUrl: './add-module.component.html',
  styleUrls: ['./add-module.component.css']
})
export class AddModuleComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService,private router:Router){

  }
  
  //modules
  addModules:TrainingModule={
    trainingModuleId:0,
    trainingModuleName:'',
    trainingModuleDescription:'',
    PackageId:0,
    TrainingModuleStatusId:1
  }

  //packages and statuses
  packages:Package[]=[];
  statuses:ModuleStatus[]=[];

  ngOnInit(): void {
    this.getPackages();
    this.GetModStatuses();
  }

  //add function
  addModule(){
    if(this.addModules.trainingModuleName && this.addModules.trainingModuleDescription  && this.addModules.TrainingModuleStatusId)
    {
      this.dataservice.AddModule(this.addModules).subscribe({
        next:(course)=>{
          this.openModal();
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
  //redirect
  cancel_continue(){
    this.router.navigate(['traingModuleCompany']);
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
  
  //modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  GetModStatuses(){
    this.dataservice.GetStatuses().subscribe(result=>{
      let statusList:any[]=result
      statusList.forEach((element)=>{
        this.statuses.push(element)
      });
    })
    return this.statuses;
  }

  getPackages(){
    this.dataservice.GetPackages().subscribe(result=>{
      let packageList:any[]=result
      packageList.forEach((element)=>{
        this.packages.push(element)
      });
    })
    return this.packages;
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
