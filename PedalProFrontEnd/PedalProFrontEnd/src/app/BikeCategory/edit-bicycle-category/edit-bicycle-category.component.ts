import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleCategory } from '../../Models/bicycle-category';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-bicycle-category',
  templateUrl: './edit-bicycle-category.component.html',
  styleUrls: ['./edit-bicycle-category.component.css']
})
export class EditBicycleCategoryComponent implements OnInit{
  constructor(private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

  editBicycleCat:BicycleCategory={
    bicycleCategoryId:0,
    bicycleCategoryName:''
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.service.GetBicycleCategory(id).subscribe({
            next:(response)=>{
              this.editBicycleCat=response;
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

  EditBikeCat(){
    if(this.editBicycleCat.bicycleCategoryName)
    {
      this.service.EditBicycleCategory(this.editBicycleCat.bicycleCategoryId,this.editBicycleCat).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }
    
  }

  cancel_continue(){
    this.router.navigate(['BicycleCategory'])
  }

  Logout()
  {
    this.service.logout();
  }
}
