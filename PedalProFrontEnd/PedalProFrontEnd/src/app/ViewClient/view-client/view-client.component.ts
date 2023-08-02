import { Component ,OnInit} from '@angular/core';
import { ClientClient } from 'src/app/Models/client-client';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { ClientType } from 'src/app/Models/client-type';

@Component({
  selector: 'app-view-client',
  templateUrl: './view-client.component.html',
  styleUrls: ['./view-client.component.css']
})
export class ViewClientComponent {
  clients:ClientClient[]=[];
  clientTypes:ClientType[]=[];
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    
    this.service.GetRoles().subscribe(data => console.log(data));
    
    this.GetClients();
    
    
    location.reload;
    

    
  }

  GetClients()
  {
    this.service.GetClientsClients().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.clients.push(element)
      });
    })
    return this.clients;
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
    this.router.navigate(['/viewClientsClients'])
  }

  GetClientType(id: any) {
    const categories = this.clientTypes.find(m => m.clientTypeId === id);
  
    if (categories) {
      return categories.clientTypeName;
    } else {
      this.service.GetClientType(id).subscribe(result => {
        this.clientTypes.push(result);
        return result.clientTypeName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Type does not exist';
  }

  sendReminder(clientId: number): void {
    this.service.sendBookingReminder(clientId).subscribe(
      (response) => {
        // Success handling, display the response message in the modal
        console.log('Booking reminder sent successfully');
        this.openModal();
      },
      (error) => {
        // Error handling, display the error message in the console
        console.error('Failed to send booking reminder', error);
      }
    );
  }
  Logout()
  {
    this.service.logout();
  }

}
