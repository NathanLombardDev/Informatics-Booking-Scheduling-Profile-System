import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ImageType } from '../../Models/image-type';
import { BicycleBrand } from '../../Models/bicycle-brand';

@Component({
  selector: 'app-add-bicycle-brand',
  templateUrl: './add-bicycle-brand.component.html',
  styleUrls: ['./add-bicycle-brand.component.css']
})
export class AddBicycleBrandComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService,private router:Router){}
  
  //Components
  addBrands:BicycleBrand={
    bicycleBrandId:0,
    brandName:'',
    imageTypeId:0,
    imageUrl:'',
    brandImgName:''
  }

  imageTypes:ImageType[]=[];
  
  ngOnInit(): void {
    this.GetImageTypes();
  }

  //Add brand to the database
  addBrand(){
    if(this.addBrands.brandImgName && this.addBrands.brandName && this.addBrands.imageTypeId && this.addBrands.imageUrl)
    {
      this.dataservice.AddBicycleBrand(this.addBrands).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }
    else {
      alert('Validation error: Please fill in all fields.');
    }
  }

  //Cancel button
  cancel_continue(){
    this.router.navigate(['BicycleBrand']);
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

  //Convert image type and use in the array
  GetImageTypes(){
    this.dataservice.GetImageTypes().subscribe(result=>{
      let imageTypeList:any[]=result
      imageTypeList.forEach((element)=>{
        this.imageTypes.push(element)
      });
    })
    return this.imageTypes;
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
