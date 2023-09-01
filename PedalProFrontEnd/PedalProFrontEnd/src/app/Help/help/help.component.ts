import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Help } from 'src/app/Models/help';
import { HelpCatergory } from 'src/app/Models/help-catergory';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';
import { DeleteDialogComponent,MyDialogData } from 'src/app/Dialogs/delete-dialog/delete-dialog.component';
@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.css']
})
export class HelpComponent implements OnInit{
  addHelps:Help={
    helpId:0,
    helpCategoryId:0,
    helpName:'',
    helpDescription:''
  }
  
  helps:Help[]=[];
  searchTerm:string='';
  category:HelpCatergory[]=[];

  
  matHelpCategories:HelpCatergory[]=[];
  

  constructor(private dialog:MatDialog,private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    this.GetAllHelp();
  }

  //getmaterials function
  GetAllHelp()
  {
    this.service.GetAllHelp().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.helps.push(element)
      });
    })
    
    return this.helps;
    
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  //filter for search
  filteredModules(){
    return this.helps.filter(help=>{
      const name=help.helpName.toLowerCase();
      const description = help.helpDescription.toLowerCase();
      const categoryName=this.GetCategory(help.helpCategoryId).toLowerCase();
      const term = this.searchTerm.toLowerCase();
      return name.includes(term) || description.includes(term) || categoryName.includes(term);
    })
  }

  GetCategory(id: any) {
    const categories = this.category.find(m => m.helpCategoryId === id);
  
    if (categories) {
      return categories.helpCategoryName;
    } else {
      this.service.GetHelpCategory(id).subscribe(result => {
        this.category.push(result);
        return result.helpCategoryName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Module does not exist';

    
  }

  //delete function
  DeleteHelp(id:any)
  {
    const dialogData: MyDialogData = {
      title: 'Confirm Delete',
      message: 'Are you sure you want to delete this help information?'
    };

    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: dialogData
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {  
        this.service.DeleteHelp(id).subscribe({
          next:(response)=>{
            
            const index=this.helps.findIndex((help)=>help.helpId===id);
            if(index!=-1){
              this.helps.slice(index,1);
            }
            this.openModal();
            
          },
          error:(err)=>{
            const errorMessage = err.error || 'An error occurred';
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

  //modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
  
  GetAllHelpCategories(){
    this.service.GetAllHelpCategories().subscribe(result=>{
      let HelpCatergoryList:any[]=result
      HelpCatergoryList.forEach((element)=>{
        this. matHelpCategories.push(element)
      });
    })
    return this.matHelpCategories;
  }

  Logout()
  {
    this.service.logout();
  }
}
