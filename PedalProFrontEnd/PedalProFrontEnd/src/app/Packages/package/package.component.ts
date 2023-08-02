import { Component ,OnInit} from '@angular/core';
import { Package } from '../../Models/package';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { PackagePrice } from '../../Models/package-price';
import { Price } from '../../Models/price';

@Component({
  selector: 'app-package',
  templateUrl: './package.component.html',
  styleUrls: ['./package.component.css']
})
export class PackageComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
  }

  packagePrices:PackagePrice[]=[];
  packages:Package[]=[];
  prices:Price[]=[];
  searchTerm:string='';


  ngOnInit(): void {
    this.GetPackagePrices();
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
