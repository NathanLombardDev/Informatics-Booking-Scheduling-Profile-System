import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { TrainingMaterial } from '../../Models/training-material';
import { TrainingModule } from '../../Models/training-module';
import { ModuleTwo } from '../../Models/module-two';
import { ModuleStatus } from '../../Models/module-status';

@Component({
  selector: 'app-edit-material',
  templateUrl: './edit-material.component.html',
  styleUrls: ['./edit-material.component.css']
})
export class EditMaterialComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){

  }

  //training material
  addMaterials:TrainingMaterial={
    trainingModuleId:0,
    trainingMaterialId:0,
    trainingMaterialName:'',
    content:'',
    videoTypeId:0,
    videoUrl:'',
    
  }

  //modules and video types
  matModules:TrainingModule[]=[];
  matVideoTypes:VideoType[]=[];

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.dataservice.GetMaterial(id).subscribe({
            next:(response)=>{
              this.addMaterials=response;
            }
          })

        }
      }
    })


    this.GetModules();
    this.GetVideoTypes();
  }

  //update function
  UpdateMaterial(){
    if(this.addMaterials.trainingMaterialName && this.addMaterials.content && this.addMaterials.trainingModuleId && this.addMaterials.videoUrl && this.addMaterials.videoTypeId)
    {
      this.dataservice.EditMaterial(this.addMaterials.trainingMaterialId,this.addMaterials).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['trainingmaterial'])
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  GetModules(){
    this.dataservice.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. matModules.push(element)
      });
    })
    return this.matModules;
  }

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
