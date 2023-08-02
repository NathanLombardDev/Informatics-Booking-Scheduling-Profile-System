import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-company-landing-page',
  templateUrl: './company-landing-page.component.html',
  styleUrls: ['./company-landing-page.component.css']
})
export class CompanyLandingPageComponent {
  
  constructor(private service:PedalProServiceService){}
  
  Logout()
  {
    this.service.logout();
  }

}
