import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { EmployeeType } from '../../Models/employee-type';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-edit-employee-type',
  templateUrl: './edit-employee-type.component.html',
  styleUrls: ['./edit-employee-type.component.css']
})
export class EditEmployeeTypeComponent implements OnInit{
  editEmpTypes:EmployeeType={
    empTypeId:0,
    empTypeName:'',
    empTypeDescription:''
  }

  constructor(private dialog:MatDialog,private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.service.GetEmployeetype(id).subscribe({
            next:(response)=>{
              this.editEmpTypes=response;
            },
            error:(err)=>{
              const errorMessage = err.error || 'An error occurred';
              this.openErrorDialog(errorMessage);
            }
          })
        }
      }
    })
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  //Edit Employee Type Details
  EditEmpType(){
    if(this.editEmpTypes.empTypeName && this.editEmpTypes.empTypeDescription)
    {
      this.service.EditEmployeeType(this.editEmpTypes.empTypeId,this.editEmpTypes).subscribe({
        next:(response)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      })
    }else{//error
      this.openErrorDialog('Validation error: Please fill in all fields.');
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
