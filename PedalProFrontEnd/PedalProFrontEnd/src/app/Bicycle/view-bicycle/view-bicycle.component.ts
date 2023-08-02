import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { Bicycle } from '../../Models/bicycle';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { BicycleCategory } from '../../Models/bicycle-category';

@Component({
  selector: 'app-view-bicycle',
  templateUrl: './view-bicycle.component.html',
  styleUrls: ['./view-bicycle.component.css']
})
export class ViewBicycleComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  modules:TrainingModule[]=[];
  
  bicycles:Bicycle[]=[];
  category:BicycleCategory[]=[];
  brand:BicycleBrand[]=[];

  ngOnInit(): void {
    this.GetBicycles();
    this.GetModules();
  }
  // get modules method
  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }
// get bicycles method
  GetBicycles()
  {
    this.service.GetBicycles().subscribe(result=>{
      let bicycleList:any[]=result
      bicycleList.forEach((element)=>{
        this.bicycles.push(element)
      });
    })
    
    return this.bicycles;
    
  }
  // get categories method
  GetCategory(id: any) {
    const categories = this.category.find(m => m.bicycleCategoryId === id);
  
    if (categories) {
      return categories.bicycleCategoryName;
    } else {
      this.service.GetBicycleCategory(id).subscribe(result => {
        this.category.push(result);
        return result.bicycleCategoryName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Category does not exist';
  }

  GetBrand(id: any) {
    const brands = this.brand.find(m => m.bicycleBrandId === id);
  
    if (brands) {
      return brands.brandName;
    } else {
      this.service.GetBicycleBrand(id).subscribe(result => {
        this.brand.push(result);
        return result.brandName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Brand does not exist';
  }

  DeleteBicycle(id:any)
  {
    this.service.DeleteBicycle(id).subscribe({
      next:(response)=>{
        
        const index=this.bicycles.findIndex((bicycle)=>bicycle.bicycleId===id);
        if(index!=-1){
          this.bicycles.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  ReloadPage()
  {
    location.reload();
  }
// pop-up modal
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
    this.service.logout();
  }
}
