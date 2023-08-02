import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BicyclePart } from '../../Models/bicycle-part';

@Component({
  selector: 'app-bicycle-part',
  templateUrl: './bicycle-part.component.html',
  styleUrls: ['./bicycle-part.component.css']
})
export class BicyclePartComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}

  bicycleParts:BicyclePart[]=[];

  ngOnInit(): void {
    this.GetBikeParts();
  }

  //Get bicycle parts
  GetBikeParts()
  {
    this.service.GetBicycleParts().subscribe(result=>{
      let bikePartList:any[]=result
      bikePartList.forEach((element)=>{
        this.bicycleParts.push(element)
      });
    })
    return this.bicycleParts;
  }

  //Delete Bicycle
  DeleteBikePart(id:any)
  {
    this.service.DeleteBicyclePart(id).subscribe({
      next:(response)=>{
        
        const index=this.bicycleParts.findIndex((bicyclePart)=>bicyclePart.bicyclePartId===id);
        if(index!=-1){
          this.bicycleParts.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  //Refresh the page
  ReloadPage()
  {
    location.reload();
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

  //  Cancel button
  cancel_continue(){
    this.router.navigate(['BicyclePart'])
  }

  Logout()
  {
    this.service.logout();
  }
}
