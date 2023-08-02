import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { TrainingMaterial } from '../../Models/training-material';
import { TrainingModule } from '../../Models/training-module';
import { ModuleTwo } from '../../Models/module-two';
import { ModuleStatus } from '../../Models/module-status';

@Component({
  selector: 'app-add-material',
  templateUrl: './add-material.component.html',
  styleUrls: ['./add-material.component.css']
})
export class AddMaterialComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService,private router:Router){}
  addMaterials:TrainingMaterial={
    trainingModuleId:0,
    trainingMaterialId:0,
    trainingMaterialName:'',
    content:'',
    videoTypeId:0,
    videoUrl:'',
  }
  // Training module and videotypes
  matModules:TrainingModule[]=[];
  matVideoTypes:VideoType[]=[];
  

  ngOnInit(): void {
    this.GetModules();
    this.GetVideoTypes();
  }

  // Add function
  addMaterial(){
    if(this.addMaterials.trainingMaterialName && this.addMaterials.content && this.addMaterials.trainingModuleId && this.addMaterials.videoUrl && this.addMaterials.videoTypeId)
    {
      this.dataservice.AddMaterial(this.addMaterials).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }
    else{
      alert('Validation error: Please fill in all fields.');
    }
    
    
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['trainingmaterial']);
  }

  //modal-pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
  //getmodules
  GetModules(){
    this.dataservice.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. matModules.push(element)
      });
    })
    return this.matModules;
  }
  //getvideotypes
  GetVideoTypes(){
    this.dataservice.GetVideoTypes().subscribe(result=>{
      let videoTypeList:any[]=result
      videoTypeList.forEach((element)=>{
        this. matVideoTypes.push(element)
      });
    })
    return this.matVideoTypes;
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
