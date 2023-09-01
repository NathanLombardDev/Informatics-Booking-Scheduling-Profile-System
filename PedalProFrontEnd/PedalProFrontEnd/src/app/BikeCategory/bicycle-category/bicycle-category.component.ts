import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { BicycleCategory } from '../../Models/bicycle-category';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';
@Component({
  selector: 'app-bicycle-category',
  templateUrl: './bicycle-category.component.html',
  styleUrls: ['./bicycle-category.component.css']
})
export class BicycleCategoryComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient,private dialog:MatDialog){}

  bicycleCategories:BicycleCategory[]=[];

  ngOnInit(): void {
    this.GetBikeCats();
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  GetBikeCats()
  {
    this.service.GetBicyclecategories().subscribe(result=>{
      let bikeCatList:any[]=result
      bikeCatList.forEach((element)=>{
        this.bicycleCategories.push(element)
      });
    })
    return this.bicycleCategories;
  }

  DeleteBikeCat(id:any)
  {
    
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this category?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) { 
        this.service.DeleteBicycleCategory(id).subscribe({
          next:(response)=>{
            
            const index=this.bicycleCategories.findIndex((bicycleCat)=>bicycleCat.bicycleCategoryId===id);
            if(index!=-1){
              this.bicycleCategories.slice(index,1);
            }
            this.openModal();
            
          },
          error:(err)=> {
            const errorMessage = err.error || 'An error occurred';
            this.openErrorDialog(errorMessage);
          },
        })
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

  
  cancel_continue(){
    this.router.navigate(['BicycleCategory'])
  }

  Logout()
  {
    this.service.logout();
  }
}
