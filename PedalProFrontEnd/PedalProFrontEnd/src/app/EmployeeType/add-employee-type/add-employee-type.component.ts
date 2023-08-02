import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { EmployeeType } from '../../Models/employee-type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-employee-type',
  templateUrl: './add-employee-type.component.html',
  styleUrls: ['./add-employee-type.component.css']
})
export class AddEmployeeTypeComponent implements OnInit{
  constructor(private dataService:PedalProServiceService,private router:Router) { }


  //Add Employee Type Array
  addEmpTypes:EmployeeType={
    empTypeId:0,
    empTypeName:'',
    empTypeDescription:''
  }

  ngOnInit(): void {
    
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

//Add Name and Description
  addEmpType(){
    if(this.addEmpTypes.empTypeName && this.addEmpTypes.empTypeDescription)
    {
      this.dataService.AddEmployeeType(this.addEmpTypes).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
    
  }// redirect to View Employee Type
  cancel_continue(){
    this.router.navigate(['viewEmployeeTypes']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
