import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Bicycle } from '../../Models/bicycle';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { TrainingModule } from '../../Models/training-module';
import { BicycleCategory } from '../../Models/bicycle-category';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-add-bicycle',
  templateUrl: './add-bicycle.component.html',
  styleUrls: ['./add-bicycle.component.css']
})
export class AddBicycleComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  
  addBicycles:Bicycle={
    bicycleId:0,
    bicycleName:'',
    bicycleBrandId:0,
    //clientId:1,
    bicycleCategoryId:0
  }
  
  modules:TrainingModule[]=[];
  categories:BicycleCategory[]=[];
  brands:BicycleBrand[]=[];

  

  ngOnInit(): void {
    this.GetModules();
    this.GetBrands();
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
// add bicycle method
  addBicycle(){
    if(this.addBicycles.bicycleBrandId && this.addBicycles.bicycleCategoryId && this.addBicycles.bicycleName)
    {
      this.service.AddBicycle(this.addBicycles).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }else{
      alert('Validation error: Please fill in all fields.');
    }
  }
  cancel_continue(){
    this.router.navigate(['Bicycle']);
  }
// modal pop-up
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
