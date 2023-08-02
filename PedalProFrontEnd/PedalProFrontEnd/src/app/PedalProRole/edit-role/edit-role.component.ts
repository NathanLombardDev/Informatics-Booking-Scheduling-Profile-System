import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css']
})
export class EditRoleComponent implements OnInit{
  addroles:PedalProRole={
    roleId:0,
    roleName:''
  }
  constructor(private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.service.getRole(id).subscribe({
            next:(response)=>{
              this.addroles=response;
            }
          })
        }
      }
    })
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  updateRole(){
    if(this.addroles.roleName)
    {
      this.service.updateRole(this.addroles.roleId,this.addroles).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }

  cancel_continue(){
    this.router.navigate(['pedalprorole'])
  }

  Logout()
  {
    this.service.logout();
  }
}
