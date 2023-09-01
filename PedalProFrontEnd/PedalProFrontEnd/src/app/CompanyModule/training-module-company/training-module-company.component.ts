import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-training-module-company',
  templateUrl: './training-module-company.component.html',
  styleUrls: ['./training-module-company.component.css']
})
export class TrainingModuleCompanyComponent implements OnInit{
  constructor(private dialog:MatDialog,private service:PedalProServiceService,private router:Router, private http:HttpClient){
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
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
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this training module with all corresponding material?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.DeleteModule(id).subscribe({
          next:(response)=>{
            
            const index=this.modules.findIndex((module)=>module.trainingModuleId===id);
            if(index!=-1){
              this.modules.slice(index,1);
            }
            this.openModal();
            
          },
          error:(err)=>{
            const errorMessage = err.error || 'An error occurred';
            this.openErrorDialog(errorMessage);
          }
        })
      }
    });
    
    
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
