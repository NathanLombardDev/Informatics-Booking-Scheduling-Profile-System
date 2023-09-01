import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { ClientType } from '../../Models/client-type';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-client-type',
  templateUrl: './client-type.component.html',
  styleUrls: ['./client-type.component.css']
})
export class ClientTypeComponent implements OnInit{
  constructor(private service:PedalProServiceService,private dialog:MatDialog,private router:Router, private http:HttpClient){}

  clientTypes:ClientType[]=[];

  ngOnInit(): void {
    this.GetEmpTypes();
  }
// get employee types method
  GetEmpTypes()
  {
    this.service.GetClientTypes().subscribe(result=>{
      let clientTypeList:any[]=result
      clientTypeList.forEach((element)=>{
        this.clientTypes.push(element)
      });
    })
    return this.clientTypes;
  }
// delete method
  DeleteEmpType(id:any)
  {

    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this client type?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.DeleteClientType(id).subscribe({
          next:(response)=>{
            
            const index=this.clientTypes.findIndex((clientType)=>clientType.clientTypeId===id);
            if(index!=-1){
              this.clientTypes.slice(index,1);
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }


// modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  
  cancel_continue(){
    this.router.navigate(['ClientType'])
  }
  Logout()
  {
    this.service.logout();
  }
}
