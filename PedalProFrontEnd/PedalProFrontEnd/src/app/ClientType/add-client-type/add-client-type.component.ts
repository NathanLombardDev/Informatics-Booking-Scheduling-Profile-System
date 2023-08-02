import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ClientType } from '../../Models/client-type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-client-type',
  templateUrl: './add-client-type.component.html',
  styleUrls: ['./add-client-type.component.css']
})
export class AddClientTypeComponent implements OnInit{
  constructor(private dataService:PedalProServiceService,private router:Router) { }

  addClientTypes:ClientType={
    clientTypeId:0,
    clientTypeName:''
  }

  ngOnInit(): void {
    
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

// add employee type modal
  addEmpType(){
    if(this.addClientTypes.clientTypeName)
    {
      this.dataService.AddClientType(this.addClientTypes).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['ClientType']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
