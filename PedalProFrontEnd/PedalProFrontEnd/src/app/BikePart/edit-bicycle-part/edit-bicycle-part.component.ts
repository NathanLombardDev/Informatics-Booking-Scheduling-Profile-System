import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ClientType } from '../../Models/client-type';
import { BicyclePart } from '../../Models/bicycle-part';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-bicycle-part',
  templateUrl: './edit-bicycle-part.component.html',
  styleUrls: ['./edit-bicycle-part.component.css']
})
export class EditBicyclePartComponent implements OnInit{
  constructor(private route:ActivatedRoute, private service:PedalProServiceService, private router:Router){}

  //Promise

  //Components
  editBicycleParts:BicyclePart={
    bicyclePartId:0,
    bicyclePartName:''
  }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.service.GetBicyclePart(id).subscribe({
            next:(response)=>{
              this.editBicycleParts=response;
            }
          })
        }
      }
    })
  }

  //Modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  //Update Bicycle part
  EditBikePart(){
    if(this.editBicycleParts.bicyclePartName)
    {
      this.service.EditBicyclePart(this.editBicycleParts.bicyclePartId,this.editBicycleParts).subscribe({
        next:(response)=>{
          this.openModal();
        }
      })
    }else{
      alert('Validation error: Please fill in all fields.');
    }

    
  }

  //Cancel Button
  cancel_continue(){
    this.router.navigate(['BicyclePart'])
  }

  Logout()
  {
    this.service.logout();
  }
}
