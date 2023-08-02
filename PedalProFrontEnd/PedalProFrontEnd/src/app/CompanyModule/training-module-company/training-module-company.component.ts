import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-training-module-company',
  templateUrl: './training-module-company.component.html',
  styleUrls: ['./training-module-company.component.css']
})
export class TrainingModuleCompanyComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
  }
  statuses:ModuleStatus[]=[];
  modules:TrainingModule[]=[];
  //roles:PedalProRole[]=[];
  searchTerm:string='';

  dummies:any[]=[];
  ngOnInit(): void {
    this.GetModules();
    this.service.GetModules().subscribe(data => console.log(data));
  }

  GetStatuses(){
    this.service.GetStatuses().subscribe(result=>{
      let statusList:any[]=result
      statusList.forEach((element)=>{
        this. statuses.push(element)
      });
    })
    return this.statuses;
  }

  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }

  filteredModules(){
    return this.modules.filter(module=>{
      const name=module.trainingModuleName.toLowerCase();
      const description = module.trainingModuleDescription.toLowerCase();
      const term = this.searchTerm.toLowerCase();
      return name.includes(term) || description.includes(term);
    })
  }

  DeleteModule(id:any)
  {
    this.service.DeleteModule(id).subscribe({
      next:(response)=>{
        
        const index=this.modules.findIndex((module)=>module.trainingModuleId===id);
        if(index!=-1){
          this.modules.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  ReloadPage()
  {
    location.reload();
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
}
