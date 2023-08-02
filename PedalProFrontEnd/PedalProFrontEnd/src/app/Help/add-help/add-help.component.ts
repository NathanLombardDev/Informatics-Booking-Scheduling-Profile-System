import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Help } from 'src/app/Models/help';
import { HelpCatergory } from 'src/app/Models/help-catergory';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-add-help',
  templateUrl: './add-help.component.html',
  styleUrls: ['./add-help.component.css']
})
export class AddHelpComponent implements OnInit{
  constructor(private dataservice:PedalProServiceService,private router:Router){}
  addHelps:Help={
    helpId:0,
    helpCategoryId:0,
    helpName:'',
    helpDescription:''
  }

  // Help Category
  matHelpCategories:HelpCatergory[]=[];
  

  ngOnInit(): void {
    this.GetAllHelpCategories();
  }

  // Add function
  addHelp(){
    if(this.addHelps.helpName && this.addHelps.helpDescription && this.addHelps.helpCategoryId)
    {
      this.dataservice.AddHelp(this.addHelps).subscribe({
        next:(course)=>{
          this.openModal();
        }
      });
    }
    else{
      alert('Validation error: Please fill in all fields.');
    }
    
    
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['view-managehelp']);
  }

  //modal-pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
  
  //getHelp Categories
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
