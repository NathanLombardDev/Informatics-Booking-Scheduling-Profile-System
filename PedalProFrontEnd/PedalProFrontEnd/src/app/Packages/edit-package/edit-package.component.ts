import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Package } from '../../Models/package';

@Component({
  selector: 'app-edit-package',
  templateUrl: './edit-package.component.html',
  styleUrls: ['./edit-package.component.css']
})
export class EditPackageComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){}


  editPackages:Package={
    packageId:0,
    packageName:'',
    packageDescription:'',
    price1:0
  }


  UpdatePackage(){
    if (this.editPackages.packageName && this.editPackages.packageDescription && this.editPackages.price1)
    {
      this.dataservice.EditPackage(this.editPackages.packageId,this.editPackages).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    } 
    
  }
  cancel_continue(){
    this.router.navigate(['ViewPackages'])
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');
        if(id)
        {
          this.dataservice.GetPackage(id).subscribe({
            next:(response)=>{
              this.editPackages=response;
            }
          })
        }
      }
    })
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
