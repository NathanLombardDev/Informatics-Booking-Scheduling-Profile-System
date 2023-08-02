import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleCategory } from '../../Models/bicycle-category';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-bicycle-category',
  templateUrl: './add-bicycle-category.component.html',
  styleUrls: ['./add-bicycle-category.component.css']
})
export class AddBicycleCategoryComponent {
  constructor(private dataService:PedalProServiceService,private router:Router) { }

  addBicycleCat:BicycleCategory={
    bicycleCategoryId:0,
    bicycleCategoryName:''
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


  AddBicycleCat(){
    if(this.addBicycleCat.bicycleCategoryName)
    {
      this.dataService.AddBicycleCategory(this.addBicycleCat).subscribe({
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
    this.router.navigate(['BicycleCategory']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
