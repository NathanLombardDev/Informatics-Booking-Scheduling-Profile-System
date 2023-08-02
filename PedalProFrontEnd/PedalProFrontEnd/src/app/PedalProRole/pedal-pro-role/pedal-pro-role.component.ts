import { Component ,OnInit} from '@angular/core';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';

@Component({
  selector: 'app-pedal-pro-role',
  templateUrl: './pedal-pro-role.component.html',
  styleUrls: ['./pedal-pro-role.component.css']
})
export class PedalProRoleComponent implements OnInit{
  roles:PedalProRole[]=[];

  dummies:any[]=[];

  
  
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    
    this.service.GetRoles().subscribe(data => console.log(data));
    
    this.GetRoles();
    
    
    location.reload;
    

    
  }

  GetRoles()
  {
    this.service.GetRoles().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.roles.push(element)
      });
    })
    return this.roles;
  }

  DeleteRole(id:any)
  {
    this.service.deleteRole(id).subscribe({
      next:(response)=>{
        
        const index=this.roles.findIndex((role)=>role.roleId===id);
        if(index!=-1){
          this.roles.slice(index,1);
        }
        this.openModal();
        
      }
    })
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
    this.router.navigate(['pedalprorole'])
  }

  Logout()
  {
    this.service.logout();
  }
}
