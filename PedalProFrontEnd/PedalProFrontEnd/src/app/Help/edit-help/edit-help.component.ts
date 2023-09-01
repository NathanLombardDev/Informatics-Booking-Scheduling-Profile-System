import { Component,OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Help } from 'src/app/Models/help';
import { HelpCatergory } from 'src/app/Models/help-catergory';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-edit-help',
  templateUrl: './edit-help.component.html',
  styleUrls: ['./edit-help.component.css']
})
export class EditHelpComponent implements OnInit{
  constructor(private dialog:MatDialog,private dataservice:PedalProServiceService, private router:Router, private route:ActivatedRoute){

  }

  //Help Array
  addHelps:Help={
    helpId:0,
    helpCategoryId:0,
    helpName:'',
    helpDescription:''
  }

  // Help Category
  matHelpCategories:HelpCatergory[]=[];
  

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next:(params)=>{
        const id=params.get('id');

        if(id)
        {
          this.dataservice.GetHelp(id).subscribe({
            next:(response)=>{
              this.addHelps=response;
            },
            error:(err)=>{
              const errorMessage = err.error || 'An error occurred';
              this.openErrorDialog(errorMessage);
            }
          })

        }
      }
    })


    this.GetAllHelpCategories();
  }

  //update function
  UpdateHelp(){
    if(this.addHelps.helpName && this.addHelps.helpDescription && this.addHelps.helpCategoryId)
    {
      this.dataservice.UpdateHelp(this.addHelps.helpId,this.addHelps).subscribe({
        next:(response)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      })
    }else{
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['view-managehelp'])
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  GetAllHelpCategories(){
    this.dataservice.GetAllHelpCategories().subscribe(result=>{
      let HelpCatergoryList:any[]=result
      HelpCatergoryList.forEach((element)=>{
        this. matHelpCategories.push(element)
      });
    })
    return this.matHelpCategories;
  }

  Logout()
  {
    this.dataservice.logout();
  }
}
