import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VideoType } from '../../Models/video-type';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { ImageType } from '../../Models/image-type';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-edit-bicycle-brand',
  templateUrl: './edit-bicycle-brand.component.html',
  styleUrls: ['./edit-bicycle-brand.component.css']
})
export class EditBicycleBrandComponent {
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){}
  
  selectedFile: File | null = null;

  editbrands:BicycleBrand={
    bicycleBrandId:0,
    brandName:'',
    imageTypeId:0,
    imageUrl:'',
    brandImgName:''
  }

  //Image into an array
  imagetypes:ImageType[]=[];

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  //On Edit button get bicycle brands
  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');
        if(id)
        {
          this.dataservice.GetBicycleBrand(id).subscribe({
            next:(response)=>{
              this.editbrands=response;
            },
            error:(err)=>{
              const errorMessage = err.error || 'An error occurred';
              this.openErrorDialog(errorMessage);
            }
          })
        }
      }
    })

    this.GetImageTypes();
  }

  //Convert image to use in a array
  GetImageTypes(){
    this.dataservice.GetImageTypes().subscribe(result=>{
      let imageTypeList:any[]=result
      imageTypeList.forEach((element)=>{
        this.imagetypes.push(element)
      });
    })
    return this.imagetypes;
  }

  //Update Image Name and Brand Name
  UpdateMaterial(){
    if(this.editbrands.brandImgName && this.editbrands.brandName && this.editbrands.imageTypeId)
    {
      this.dataservice.EditBicycleBrand(this.editbrands.bicycleBrandId,this.editbrands).subscribe({
        next:(response)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      })
    }
    else {
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
  }

  uploadAndAdd(): void {
    if (this.selectedFile) {
      this.dataservice.uploadImage(this.selectedFile).subscribe(
        (response) => {
          console.log('Image uploaded successfully:', response);
          console.log(response.url)
          this.editbrands.imageUrl=response.url;
          this.UpdateMaterial();
        },
        (error) => {
          console.error('Error uploading image:', error);
        }
      );
    } else {
      console.log('No image selected.');
    }
  }

  //Cancel button
  cancel_continue(){
    this.router.navigate(['BicycleBrand'])
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

  Logout()
  {
    this.dataservice.logout();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }
}
