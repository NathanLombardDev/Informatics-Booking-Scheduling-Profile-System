import { Component, OnInit, ViewChild } from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { response } from 'express';
import { Package } from '../../Models/package';
import { ModuleStatus } from '../../Models/module-status';

@Component({
  selector: 'app-edit-module',
  templateUrl: './edit-module.component.html',
  styleUrls: ['./edit-module.component.css']
})
export class EditModuleComponent implements OnInit{
  @ViewChild('moduleForm') moduleForm:NgForm |undefined;
  
    

  editModules:TrainingModule={
    trainingModuleId:0,
    trainingModuleName:'',
    trainingModuleDescription:'',
    PackageId:0,
    TrainingModuleStatusId:1
  }
  
  packages:Package[]=[];
  statuses:ModuleStatus[]=[];

  constructor(private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){

  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.dataservice.GetModuleTwo(id).subscribe({
            next:(response)=>{
              this.editModules=response;
            }
          })
        }
      }
    })

    this.getPackages();
    this.GetModStatuses();
  }

  UpdateModule(){
    if(this.editModules.trainingModuleName && this.editModules.trainingModuleDescription && this.editModules.PackageId )
    {
      this.dataservice.EditModule(this.editModules.trainingModuleId,this.editModules).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['traingModuleCompany'])
  }

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
