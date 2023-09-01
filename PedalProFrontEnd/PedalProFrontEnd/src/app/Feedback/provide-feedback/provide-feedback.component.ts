
import { Component ,OnInit} from '@angular/core';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';

import { Feedback } from 'src/app/Models/feedback';
import { FeedbackCatergory } from 'src/app/Models/feedback-catergory';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';




@Component({
  selector: 'app-provide-feedback',
  templateUrl: './provide-feedback.component.html',
  styleUrls: ['./provide-feedback.component.css']
})
export class ProvideFeedbackComponent implements OnInit{
  feedbackTypes: FeedbackCatergory[] = [];
  selectedFeedbackTypeID: number | null = null;
  rating: number | null = null;
  feedbackDescription: string = '';
  clientDetails: any;
  cartnumber:any;
  
  
  constructor(private dialog:MatDialog,private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  modules:TrainingModule[]=[];

  provideFeedbacks:Feedback={
    feedbackId:0,
    feedbackCategoryId:0,
    feedbackRating:0,
    feedbackDescription:''
  }

   // Help Category
   matFeedbackCategories:FeedbackCatergory[]=[];


   Logout()
   {
     this.service.logout();
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
  
  ngOnInit(): void {
    this.GetModules();
    this.GetAllFeedbackCategories();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
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

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  provideFeedback(){
    if(this.provideFeedbacks.feedbackCategoryId && this.provideFeedbacks.feedbackRating && this.provideFeedbacks.feedbackDescription)
    {
      this.service.ProvideFeedback(this.provideFeedbacks).subscribe({
        next:(course)=>{
          this.openModal();
        },
        error:(err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      });
    }
    else{
      this.openErrorDialog('Validation error: Please fill in all fields.');
    }
    
    
  }
  //redirect
  cancel_continue(){
    this.router.navigate(['clientLanding']);
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
  GetAllFeedbackCategories(){
    this.service.GetAllFeedbackCategories().subscribe(result=>{
      let FeedbackCatergoryList:any[]=result
      FeedbackCatergoryList.forEach((element)=>{
        this. matFeedbackCategories.push(element)
      });
    })
    return this.matFeedbackCategories;
  }

}
