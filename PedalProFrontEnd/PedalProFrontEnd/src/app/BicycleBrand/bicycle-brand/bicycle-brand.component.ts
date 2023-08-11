import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BicycleBrand } from '../../Models/bicycle-brand';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-bicycle-brand',
  templateUrl: './bicycle-brand.component.html',
  styleUrls: ['./bicycle-brand.component.css']
})
export class BicycleBrandComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){}

  brands:BicycleBrand[]=[];
  searchTerm:string='';

  ngOnInit(): void {
    this.GetBrands();
  }

  //Get brands from the array
  GetBrands()
  {
    this.service.GetBicycleBrands().subscribe(result=>{
      let brandList:any[]=result
      brandList.forEach((element)=>{
        this.brands.push(element)
      });
    })
    
    return this.brands;
    
  }

  //Search brands in the table
  filteredBrands(){
    return this.brands.filter(brand=>{
      const name=brand.brandName.toLowerCase();
      
      const term = this.searchTerm.toLowerCase();
      return name.includes(term);
    })
  }

  //Delete Brands from the list
  DeleteBrand(id: any) {
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this brand?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.service.DeleteBicycleBrand(id).subscribe({
          next: (response) => {
            const index = this.brands.findIndex((brand) => brand.bicycleBrandId === id);
            if (index !== -1) {
              this.brands.splice(index, 1); // Use splice instead of slice to remove the element from the array
            }
            this.openModal();
          }
        });
      }
    });
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
  Logout()
  {
    this.service.logout();
  }
}
