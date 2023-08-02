import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Package } from '../../Models/package';
import { Router } from '@angular/router';
import { Price } from '../../Models/price';

@Component({
  selector: 'app-add-package',
  templateUrl: './add-package.component.html',
  styleUrls: ['./add-package.component.css']
})
export class AddPackageComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService,private router:Router){}


  addPackages:Package={
    packageId:0,
    packageName:'',
    packageDescription:'',
    price1:0
  }

  ngOnInit(): void {
    
  }

  addPackage(){
    if (this.addPackages.packageName && this.addPackages.packageDescription && this.addPackages.price1) 
    {
      this.dataservice.AddPackage(this.addPackages).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }else {
      // Display a message asking for all fields to be filled in
      alert('Validation error: Please fill in all fields.');
    }
    
  }
  cancel_continue(){
    this.router.navigate(['ViewPackages']);
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
