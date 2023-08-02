import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-view-feedback',
  templateUrl: './view-feedback.component.html',
  styleUrls: ['./view-feedback.component.css']
})
export class ViewFeedbackComponent {
  constructor(private service:PedalProServiceService){};

  Logout()
  {
    this.service.logout();
  }
}
