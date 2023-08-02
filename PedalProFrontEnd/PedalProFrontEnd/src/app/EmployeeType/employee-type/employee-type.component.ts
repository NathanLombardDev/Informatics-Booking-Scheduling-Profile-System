import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { EmployeeType } from '../../Models/employee-type';


@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}


  employeeTypes:EmployeeType[]=[];

  ngOnInit(): void {
    this.GetEmpTypes();
  }
//Get Employee Types to view
  GetEmpTypes()
  {
    this.service.GetEmployeeTypes().subscribe(result=>{
      let empTypeList:any[]=result
      empTypeList.forEach((element)=>{
        this.employeeTypes.push(element)
      });
    })
    return this.employeeTypes;
  }
//Remove Employee Type
  DeleteEmpType(id:any)
  {
    this.service.DeleteEmployeeType(id).subscribe({
      next:(response)=>{
        
        const index=this.employeeTypes.findIndex((empType)=>empType.empTypeId===id);
        if(index!=-1){
          this.employeeTypes.slice(index,1);
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

  
  cancel_continue(){
    this.router.navigate(['viewEmployeeTypes'])
  }

  Logout()
  {
    this.service.logout();
  }
}
