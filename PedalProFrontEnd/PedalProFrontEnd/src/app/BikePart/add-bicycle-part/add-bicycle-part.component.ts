import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicyclePart } from '../../Models/bicycle-part';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-bicycle-part',
  templateUrl: './add-bicycle-part.component.html',
  styleUrls: ['./add-bicycle-part.component.css']
})
export class AddBicyclePartComponent implements OnInit{
  constructor(private dataService:PedalProServiceService,private router:Router) { }

  addBicyclePart:BicyclePart={
    bicyclePartId:0,
    bicyclePartName:''
  }

  ngOnInit(): void {
    
  }

  //Modal Pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  //Add bicycle part 
  AddBicyclePart(){
    if(this.addBicyclePart.bicyclePartName)
    {
      this.dataService.AddBicyclePart(this.addBicyclePart).subscribe({
        next:(course)=>{
          this.openModal();
          //this.router.navigate(['pedalprorole'])
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
  }
  //Cancel button
  cancel_continue(){
    this.router.navigate(['BicyclePart']);
  }

  Logout()
  {
    this.dataService.logout();
  }
}
