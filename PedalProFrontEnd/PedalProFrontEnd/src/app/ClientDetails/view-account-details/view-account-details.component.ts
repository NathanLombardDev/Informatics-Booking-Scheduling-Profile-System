import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ClientClient } from 'src/app/Models/client-client';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-account-details',
  templateUrl: './view-account-details.component.html',
  styleUrls: ['./view-account-details.component.css']
})
export class ViewAccountDetailsComponent implements OnInit{
  clientDetails: any;


  
  constructor(private http: HttpClient,private service:PedalProServiceService,private router:Router) {}

  ngOnInit() {
    this.fetchClientDetails();
  }

  fetchClientDetails() {
    this.service.getClientDetails().subscribe(
      (response) => {
        this.clientDetails = response;
      },
      (error) => {
        console.error(error);
      }
    );
  }

  openEditDialog(){
    
    this.router.navigate(['/UpdateAccount']); // Redirect to company side code
  }
}
