import { Component,OnInit } from '@angular/core';
import { Feedback } from 'src/app/Models/feedback';
import { FeedbackCatergory } from 'src/app/Models/feedback-catergory';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-view-feedback',
  templateUrl: './view-feedback.component.html',
  styleUrls: ['./view-feedback.component.css']
})
export class ViewFeedbackComponent implements OnInit{
  

  clientfeedback:Feedback[]=[];
  searchTerm:string='';
  category:FeedbackCatergory[]=[];

  
  matFeedbackCategories:FeedbackCatergory[]=[];
  

  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    this.GetAllFeedback();
  }

  //getmaterials function
  GetAllFeedback()
  {
    this.service.GetAllFeedback().subscribe(result=>{
      let feedbackList:any[]=result
      feedbackList.forEach((element)=>{
        this.clientfeedback.push(element)
      });
    })
    
    return this.clientfeedback;
    
  }

  //filter for search
  filteredModules(){
    return this.clientfeedback.filter(feedback=>{
      const categoryName=this.GetFeedbackCategory(feedback.feedbackCategoryId).toLowerCase();
      const description=feedback.feedbackDescription.toLowerCase();
      const term = this.searchTerm.toLowerCase();
      return description.includes(term) || categoryName.includes(term);
    })
  }

  GetFeedbackCategory(id: any) {
    const categories = this.category.find(m => m.feedbackCategoryId === id);
  
    if (categories) {
      return categories.feedbackCategoryName;
    } else {
      this.service.GetFeedbackCategory(id).subscribe(result => {
        this.category.push(result);
        return result.feedbackCategoryName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Feedback does not exist';

    
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
  
  GetAllFeedbackCategories(){
    this.service.GetAllFeedbackCategories().subscribe(result=>{
      let FeedbackCatergoryList:any[]=result
      FeedbackCatergoryList.forEach((element)=>{
        this. matFeedbackCategories.push(element)
      });
    })
    return this.matFeedbackCategories;
  }

  Logout()
  {
    this.service.logout();
  }
}
