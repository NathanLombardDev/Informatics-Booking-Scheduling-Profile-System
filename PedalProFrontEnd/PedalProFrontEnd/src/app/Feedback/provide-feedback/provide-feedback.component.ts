
import { Component ,OnInit} from '@angular/core';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';



interface FeedbackCategory {
  feedbackCategoryId: number;
  feedbackCategoryName: string;
}

interface Feedback {
  feedbackID: number;
  feedbackDescription: string;
  feedbackRating: number;
  feedbackCategoryID: number;
  //clientID: number;
  //trainingSessionID: number;
}

@Component({
  selector: 'app-provide-feedback',
  templateUrl: './provide-feedback.component.html',
  styleUrls: ['./provide-feedback.component.css']
})
export class ProvideFeedbackComponent implements OnInit{
  feedbackTypes: FeedbackCategory[] = [];
  selectedFeedbackTypeID: number | null = null;
  rating: number | null = null;
  feedbackDescription: string = '';
  
  
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  modules:TrainingModule[]=[];
  
  ngOnInit(): void {
    this.GetModules();
    this.getFeedbackTypes();
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

  Logout()
  {
    this.service.logout();
  }

  getFeedbackTypes() {

    this.http

      .get<FeedbackCategory[]>('https:url/api/feedbacktypes')

      .subscribe((data) => {

        this.feedbackTypes = data;

      });

  }

  submitFeedback() {

    if (this.selectedFeedbackTypeID && this.rating && this.feedbackDescription) {

      const newFeedback: Feedback = {

        feedbackID: 0, // The API will generate the ID

        feedbackDescription: this.feedbackDescription,

        feedbackRating: this.rating,

        feedbackCategoryID: this.selectedFeedbackTypeID,

        

      };



      this.http

        .post<Feedback>('https://url/api/feedback', newFeedback)

        .subscribe((response) => {

          // Handle success

          console.log('Feedback submitted successfully:', response);

          // Reset form fields

          this.selectedFeedbackTypeID = null;

          this.rating = null;

          this.feedbackDescription = '';

        });

    }

  }

}
