
import { TrainingModule } from 'src/app/Models/training-module';
import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Help } from 'src/app/Models/help';
import { HelpCatergory } from 'src/app/Models/help-catergory';
import { Package } from 'src/app/Models/package';
import { PackagePrice } from 'src/app/Models/package-price';
import { Price } from 'src/app/Models/price';
import { MyDialogData } from 'src/app/Dialogs/add-cart-dialog/add-cart-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { AddCartDialogComponent } from 'src/app/Dialogs/add-cart-dialog/add-cart-dialog.component';

@Component({
  selector: 'app-view-avail-packages',
  templateUrl: './view-avail-packages.component.html',
  styleUrls: ['./view-avail-packages.component.css']
})
export class ViewAvailPackagesComponent implements OnInit{
  panelOpenState = false;
  modules:TrainingModule[]=[];
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){
  }

  packagePrices:PackagePrice[]=[];
  packages:Package[]=[];
  prices:Price[]=[];
  searchTerm:string='';

  cartnumber:any;


  ngOnInit(): void {
    this.GetPackagePrices();
    this.GetModules();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
  }

  GetPackagePrices(){
    this.service.GetPackagePrices().subscribe(result=>{
      let packagePriceList:any[]=result
      packagePriceList.forEach((element)=>{
        this.packagePrices.push(element)
      });
    })
    return this.packagePrices;
  }

  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }

  GetPackageName(id: any) {
    const packaged = this.packages.find(m => m.packageId === id);
  
    if (packaged) {
      return packaged.packageName;
    } else {
      this.service.GetPackage(id).subscribe(result => {
        this.packages.push(result);
        return result.packageName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Package does not exist';

    
  }

  GetPackageDescription(id: any) {
    const packaged = this.packages.find(m => m.packageId === id);
  
    if (packaged) {
      return packaged.packageDescription;
    } else {
      this.service.GetPackage(id).subscribe(result => {
        this.packages.push(result);
        return result.packageDescription;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Package does not exist';

    
  }

  GetPriceAmount(id: any) {
    const price = this.prices.find(m => m.priceId === id);
  
    if (price) {
      return price.price1;
    } else {
      this.service.GetPrice(id).subscribe(result => {
        this.prices.push(result);
        return result.price1;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Price does not exist';

    
  }

  

  DeletePackage(id:any)
  {
    this.service.DeletePackage(id).subscribe({
      next:(response)=>{
        
        const index=this.packagePrices.findIndex((packagePrice)=>packagePrice.packageId===id);
        if(index!=-1){
          this.packagePrices.slice(index,1);
        }
        this.openModal();
        
      }
    })
  }

  addtoCart(packageId: number) {
    const dialogData: MyDialogData = {
      title: 'Confirm cart item',
      message: 'Are you sure you want add this package your cart?'
    };

    const dialogRef = this.dialog.open(AddCartDialogComponent, {
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        // Call the service to add the package to the cart
        this.service.addPackageToCart(packageId).subscribe(
          (response) => {
            console.log('Package added to cart:', response);
            
            // Set the cartId in localStorage with the new value returned from the service
            localStorage.setItem('cartId', response.cartId.toString());
            localStorage.setItem('cartQuantity', response.cartQuantity.toString());

            // Handle success, update UI or navigate to the cart page
            //this.router.navigate(['/ViewAvailPackages']); // Adjust the route based on your configuration
            this.ReloadPage();
          },
          (error) => {
            console.error('Error adding package to cart:', error);
            // Handle error, show an error message, etc.
          }
        );
      } else {
        // User cancelled or closed the dialog
        // Handle accordingly if needed
      }
    });
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

  filteredPackages(){
    return this.packagePrices.filter(packageprice=>{
      
      const packageName=this.GetPackageName(packageprice.packageId).toLowerCase();
      const packageDescription=this.GetPackageDescription(packageprice.packageId).toLowerCase();
      const price=this.GetPriceAmount(packageprice.priceId);

      const term = this.searchTerm.toLowerCase();
      return packageName.includes(term)||packageDescription.includes(term);
    })
  }

  Logout()
  {
    this.service.logout();
    
  }
  
}
