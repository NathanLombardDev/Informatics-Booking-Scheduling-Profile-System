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
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { BrandTwo } from 'src/app/Models/brand-two';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';


@Component({
  selector: 'app-view-bicycle',
  templateUrl: './view-bicycle.component.html',
  styleUrls: ['./view-bicycle.component.css']
})
export class ViewBicycleComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){}
  modules:TrainingModule[]=[];
  
  bicycles:Bicycle[]=[];
  category:BicycleCategory[]=[];
  brand:BrandTwo[]=[];
  clientDetails: any;
  cartnumber:any;

  ngOnInit(): void {
    this.GetBicycles();
    this.GetModules();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
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
    const brand = this.brand.find(m => m.bicycleBrandId === id);
  
    if (brand) {
      return brand.brandName;
    } else {
      this.service.GetBicycleBrandTwo(id).subscribe(result => {
        const brandImageId = result.brandImageId;
        this.service.GetBicycleBrandImage(brandImageId).subscribe(imageResult => {
          result.brandName = imageResult.imageUrl; // Add brandImgName to the brand
          this.brand.push(result);
          return this.brand;
        });
      });
  
      return 'Brand does not exist';
    }
  }

  fetchClientDetails() {
    this.service.getClientDetails().subscribe(
      (response) => {
        this.clientDetails = response;
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  DeleteBicycle(id:any)
  {
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete your bicylce?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });
    
    dialogRef.afterClosed().subscribe((result) =>{
      if (result) {
        this.service.DeleteBicycle(id).subscribe({
          next:(response)=>{
            
            const index=this.bicycles.findIndex((bicycle)=>bicycle.bicycleId===id);
            if(index!=-1){
              this.bicycles.slice(index,1);
            }
            this.openModal();
          },
          error:(err)=>{
            const errorMessage=err.error|| 'An error occurred';
            this.openErrorDialog(errorMessage);
          }
        })
      }
    });
    
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
