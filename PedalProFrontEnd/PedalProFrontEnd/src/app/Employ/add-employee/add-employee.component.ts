import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Employee } from '../../Models/employee';
import { Router } from '@angular/router';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { EmployeeStatus } from '../../Models/employee-status';
import { EmployeeType } from '../../Models/employee-type';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService,private router:Router){}

  //Employee Array
  addEmployees:Employee={
    employeeId:0,
    empTitle:'',
    empName:'',
    empSurname:'',
    empPhoneNum:'',
    empStatusId:0,
    empTypeId:0,
    //roleId:0,
    //username:'',
    password:'',
    emailAddress:''
  }

  //Role Array
  Roles:PedalProRole[]=[];
  empStatuses:EmployeeStatus[]=[];
  empTypesTwo:EmployeeType[]=[];

  ngOnInit(): void {
    this.GetEmpStatuses();
    this.GetTypes();
    this.GetRoles();
  }

  GetRoles(){
    this.dataservice.GetRoles().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this. Roles.push(element)
      });
    })
    return this.Roles;
  }

  //Employee Status
  GetEmpStatuses(){
    this.dataservice.GetEmployeeStatuses().subscribe(result=>{
      let empStatusList:any[]=result
      empStatusList.forEach((element)=>{
        this. empStatuses.push(element)
      });
    })
    return this.empStatuses;
  }

  // Get Employee Type
  GetTypes(){
    this.dataservice.GetEmployeeTypes().subscribe(result=>{
      let empTypeList:any[]=result
      empTypeList.forEach((element)=>{
        this.empTypesTwo.push(element)
      });
    })
    return this.empTypesTwo;
  }

//Add An Employee to the system
  addEmployee(){
    if(this.addEmployees.empName && this.addEmployees.emailAddress && this.addEmployees.empPhoneNum && this.addEmployees.empSurname && this.addEmployees.empStatusId && this.addEmployees.empTitle && this.addEmployees.password && this.addEmployees.empTypeId)
    {
      this.dataservice.AddEmployee(this.addEmployees).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
  }
  cancel_continue(){
    this.router.navigate(['viewEmployees']);
  }

  //Notification
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
    this.dataservice.logout();
  }
}
