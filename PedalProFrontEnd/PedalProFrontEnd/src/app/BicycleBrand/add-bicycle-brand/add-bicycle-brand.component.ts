import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ImageType } from '../../Models/image-type';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { AddBrandTest } from 'src/app/Models/add-brand-test';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-bicycle-brand',
  templateUrl: './add-bicycle-brand.component.html',
  styleUrls: ['./add-bicycle-brand.component.css']
})
export class AddBicycleBrandComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService,private router:Router){}
  
  //Components
  addBrands:BicycleBrand={
    bicycleBrandId:0,
    brandName:'',
    imageTypeId:0,
    imageUrl:'',
    brandImgName:''
  }

  selectedFile: File | null = null;

  imageTypes:ImageType[]=[];
  
  ngOnInit(): void {
    this.GetImageTypes();
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  //Add brand to the database
  uploadAndAdd(): void {
    if (this.selectedFile) {
      this.dataservice.uploadImage(this.selectedFile).subscribe(
        (response) => {
          console.log('Image uploaded successfully:', response);
          console.log(response.url)
          this.addBrands.imageUrl=response.url;
          this.addBrand();
        },
        (error) => {
          const errorMessage = error.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      );
    } else {
      console.log('No image selected.');
    }
  }
  
  addBrand(): void {
    if (this.addBrands.brandImgName && this.addBrands.brandName && this.addBrands.imageTypeId && this.addBrands.imageUrl) {
      this.dataservice.AddBicycleBrand(this.addBrands).subscribe({
        next: (brand) => {
          this.openModal();
        },
        error: (error) => {
          console.error('Error adding brand:', error);
        }
      });
    } else {
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
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
