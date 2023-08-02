import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent implements OnInit{
  constructor(private dataService:PedalProServiceService,private router:Router) { }
  addroles:PedalProRole={
    roleId:0,
    roleName:''
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

  addRole(){
    if(this.addroles.roleName){
      this.dataService.AddRole(this.addroles).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        }
      });
    }
    else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['pedalprorole']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
