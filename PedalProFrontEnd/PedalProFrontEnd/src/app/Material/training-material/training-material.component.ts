import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { TrainingMaterial } from '../../Models/training-material';
import { TrainingModule } from '../../Models/training-module';

@Component({
  selector: 'app-training-material',
  templateUrl: './training-material.component.html',
  styleUrls: ['./training-material.component.css']
})
export class TrainingMaterialComponent implements OnInit{
  materials:TrainingMaterial[]=[];
  searchTerm:string='';
  module:TrainingModule[]=[];

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    this.GetMaterials();
  }

  //getmaterials function
  GetMaterials()
  {
    this.service.GetMaterials().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.materials.push(element)
      });
    })
    
    return this.materials;
    
  }

  //filter for search
  filteredModules(){
    return this.materials.filter(material=>{
      const name=material.trainingMaterialName.toLowerCase();
      const description = material.content.toLowerCase();
      const modname=this.GetModule(material.trainingModuleId).toLowerCase();
      const term = this.searchTerm.toLowerCase();
      return name.includes(term) || description.includes(term)|| modname.includes(term);
    })
  }

  GetModule(id: any) {
    const modules = this.module.find(m => m.trainingModuleId === id);
  
    if (modules) {
      return modules.trainingModuleName;
    } else {
      this.service.GetModuleTwo(id).subscribe(result => {
        this.module.push(result);
        return result.trainingModuleName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Module does not exist';

    
  }

  //delete function
  DeleteMaterial(id:any)
  {
    this.service.DeleteMaterial(id).subscribe({
      next:(response)=>{
        
        const index=this.materials.findIndex((material)=>material.trainingMaterialId===id);
        if(index!=-1){
          this.materials.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  ReloadPage()
  {
    location.reload();
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

  Logout()
  {
    this.service.logout();
  }
}
