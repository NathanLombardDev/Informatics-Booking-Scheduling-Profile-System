import { Component,OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Cart } from 'src/app/Models/cart';
import { PackagePrice } from 'src/app/Models/package-price';
import { Price } from 'src/app/Models/price';
import { TrainingModule } from 'src/app/Models/training-module';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ConfirmationDialogComponent } from 'src/app/Dialogs/confirmation-dialog/confirmation-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-view-cart',
  templateUrl: './view-cart.component.html',
  styleUrls: ['./view-cart.component.css']
})
export class ViewCartComponent implements OnInit{
  cartItems: any[] = [];
  clientDetails: any;
  cartId:number=0;

  cart:Cart|undefined;

  packagePrices:PackagePrice[]=[];

  prices:Price[]=[];

  cartnumber:any;

  modules:TrainingModule[]=[];

  cartcart:any;
  

  constructor(private service:PedalProServiceService,private router:Router,private dialog:MatDialog) {}

  ngOnInit(): void {
    //this.cartItems = this.cartService.getCartItems(); // Replace with your actual logic
    this.cartId = parseInt(localStorage.getItem('cartId') || '0', 10);
    if (this.cartId !== 0) {
      this.service.GetCart(this.cartId).subscribe(
        (cart) => {
          this.cart = cart;
          console.log(cart);
          console.log(this.cart)
        },
        (error) => {
          const errorMessage=error.error;
          this.openErrorDialog(errorMessage);
        }
      );
    }

    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;

    this.GetModules();

    this.fetchClientDetails();
    
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

  GetPackagePrice(id:any){
    const packageprice=this.packagePrices.find(m=>m.packageId===id);

    if(packageprice){
      return packageprice?.packagePriceId;
    }else{
      this.service.GetPackagePrice(id).subscribe(result=>{
        this.packagePrices.push(result);
        return result.packagePriceId;
      });
    }
    return 'Does not exist'
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

  initiatePayment() {
    this.service.initiatePayment(this.cartId).subscribe(
      response => {
        // Handle the successful response, redirect to the payment page
        window.location.href = response.paymentUrl;
      },
      error => {
        const errorMessage=error.error;
        this.openErrorDialog(errorMessage);
      }
    );
  }

  removeFromCart(itemId: number): void {
    //this.cartService.removeFromCart(itemId); // Replace with your actual logic
    //this.cartItems = this.cartService.getCartItems(); // Update the cart items
  }

  calculateTotal(): number {
    return this.cartItems.reduce((total, item) => total + item.price, 0);
  }

  checkout(): void {
    // Implement checkout logic
  }

  GetPackageName(id: any) {
    const packaged = this.cart?.packages.find(m => m.packageId === id);
  
    if (packaged) {
      return packaged.packageName;
    } else {
      this.service.GetPackage(id).subscribe(result => {
        this.cart?.packages.push(result);
        return result.packageName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Package does not exist';

    
  }

  onRemovePackage(id:any): void {
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to remove this package from your cart?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });
    
    dialogRef.afterClosed().subscribe((result) =>{
      if (result) { 
        this.service.removePackageFromCart(this.cartId, id)
        .subscribe(
          (cart: Cart) => {
            this.cart = cart;
            const currentValue = localStorage.getItem('cartQuantity');
  
          if (currentValue !== null) {
          // Convert the current value to a number and subtract 1
          const newValue = parseInt(currentValue, 10) - 1;
  
          // Update localStorage with the new value
          localStorage.setItem('cartQuantity', newValue.toString());
          }
            console.log('Package removed from cart:', this.cart);
            this.openModal();
            
          },
          error => {
            const errorMessage=error.error;
            this.openErrorDialog(errorMessage);
          }
        );
        
       }
    });

  }

  Logout()
  {
    this.service.logout();
    
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  ReloadPage()
  {
    location.reload();
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
}
