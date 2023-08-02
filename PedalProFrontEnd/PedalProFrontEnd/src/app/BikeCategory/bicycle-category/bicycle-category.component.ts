import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BicycleCategory } from '../../Models/bicycle-category';

@Component({
  selector: 'app-bicycle-category',
  templateUrl: './bicycle-category.component.html',
  styleUrls: ['./bicycle-category.component.css']
})
export class BicycleCategoryComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}

  bicycleCategories:BicycleCategory[]=[];

  ngOnInit(): void {
    this.GetBikeCats();
  }

  GetBikeCats()
  {
    this.service.GetBicyclecategories().subscribe(result=>{
      let bikeCatList:any[]=result
      bikeCatList.forEach((element)=>{
        this.bicycleCategories.push(element)
      });
    })
    return this.bicycleCategories;
  }

  DeleteBikeCat(id:any)
  {
    this.service.DeleteBicycleCategory(id).subscribe({
      next:(response)=>{
        
        const index=this.bicycleCategories.findIndex((bicycleCat)=>bicycleCat.bicycleCategoryId===id);
        if(index!=-1){
          this.bicycleCategories.slice(index,1);
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
    this.router.navigate(['BicycleCategory'])
  }

  Logout()
  {
    this.service.logout();
  }
}
