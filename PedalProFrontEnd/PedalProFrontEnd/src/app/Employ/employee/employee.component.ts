import { Component ,OnInit} from '@angular/core';
import { Employee } from '../../Models/employee';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { EmployeeType } from '../../Models/employee-type';
import { EmployeeStatus } from '../../Models/employee-status';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
  }
  //Employee Array
  employees:Employee[]=[];
  employeeTypes:EmployeeType[]=[];
  employeeStatuses:EmployeeStatus[]=[];
  searchTerm:string='';

  ngOnInit(): void {
    this.GetEmployees();
  }


  GetEmployees(){
    this.service.GetEmployees().subscribe(result=>{
      let employeeList:any[]=result
      employeeList.forEach((element)=>{
        this. employees.push(element)
      });
    })
    return this.employees;
  }

  //Filter out the table
  filteredEmployees(){
    return this.employees.filter(employee=>{
      const name=employee.empName.toLowerCase();
      const surname = employee.empSurname.toLowerCase();
      const phoneNumber=employee.empPhoneNum;
      const empType=this.GetEmployeeType(employee.empTypeId).toLowerCase();
      const empTitle=employee.empTitle.toLowerCase();
      const empStatus=this.GetEmployeeStatus(employee.empStatusId).toLowerCase();
      const term = this.searchTerm.toLowerCase();
      return name.includes(term) || surname.includes(term)|| phoneNumber.includes(term)|| empType.includes(term)||empTitle.includes(term)|| empStatus.includes(term);
    })
  }

  GetEmployeeType(id: any) {
    const empType = this.employeeTypes.find(m => m.empTypeId === id);
  
    if (empType) {
      return empType.empTypeName;
    } else {
      this.service.GetEmployeetype(id).subscribe(result => {
        this.employeeTypes.push(result);
        return result.empTypeName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Module does not exist';

    
  }

  GetEmployeeStatus(id: any) {
    const empStatus = this.employeeStatuses.find(m => m.empStatusId === id);
  
    if (empStatus) {
      return empStatus.empStatusName;
    } else {
      this.service.GetEmployeeStatus(id).subscribe(result => {
        this.employeeStatuses.push(result);
        return result.empStatusName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Module does not exist';

    
  }
 
  //Remove employee from system
  DeleteEmployee(id:any)
  {
    this.service.DeleteEmployee(id).subscribe({
      next:(response)=>{
        
        const index=this.employees.findIndex((employee)=>employee.employeeId===id);
        if(index!=-1){
          this.employees.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }


  //Reload Employee Page
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
