import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { ClientType } from '../../Models/client-type';

@Component({
  selector: 'app-client-type',
  templateUrl: './client-type.component.html',
  styleUrls: ['./client-type.component.css']
})
export class ClientTypeComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}

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
    this.service.DeleteClientType(id).subscribe({
      next:(response)=>{
        
        const index=this.clientTypes.findIndex((clientType)=>clientType.clientTypeId===id);
        if(index!=-1){
          this.clientTypes.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  ReloadPage()
  {
    location.reload();
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
