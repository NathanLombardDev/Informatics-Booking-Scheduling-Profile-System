import { Component ,OnInit} from '@angular/core';
import { TrainingModule } from '../../Models/training-module';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router,ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { Bicycle } from '../../Models/bicycle';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { BicycleCategory } from '../../Models/bicycle-category';

@Component({
  selector: 'app-edit-bicycle',
  templateUrl: './edit-bicycle.component.html',
  styleUrls: ['./edit-bicycle.component.css']
})
export class EditBicycleComponent implements OnInit{

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient, private route:ActivatedRoute){}
  
  modules:TrainingModule[]=[];
  categories:BicycleCategory[]=[];
  brands:BicycleBrand[]=[];


  editBicycles:Bicycle={
    bicycleId:0,
    bicycleName:'',
    bicycleBrandId:0,
    //clientId:1,
    bicycleCategoryId:0
  }


  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');
        if(id)
        {
          this.service.GetBicycle(id).subscribe({
            next:(response)=>{
              this.editBicycles=response;
            }
          })
        }
      }
    })
    this.GetBrands();
    this.GetModules();
    this.GetCategories();
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
// get brands method
  GetBrands(){
    this.service.GetBicycleBrands().subscribe(result=>{
      let brandList:any[]=result
      brandList.forEach((element)=>{
        this.brands.push(element)
      });
    })
    return this.brands;
  }
// get categories method
  GetCategories(){
    this.service.GetBicyclecategories().subscribe(result=>{
      let catList:any[]=result
      catList.forEach((element)=>{
        this.categories.push(element)
      });
    })
    return this.categories;
  }
// update bicycles method
  UpdateBicycle(){
    if(this.editBicycles.bicycleBrandId && this.editBicycles.bicycleCategoryId && this.editBicycles.bicycleName)
    {
    this.service.EditBicycle(this.editBicycles.bicycleId,this.editBicycles).subscribe({
      next:(response)=>{
        this.openModal();
      }
    })
    }else {
      alert('Validation error: Please fill in all fields.');
    }
  }
  cancel_continue(){
    this.router.navigate(['Bicycle'])
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
