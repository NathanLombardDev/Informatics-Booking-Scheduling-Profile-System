import { Component } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-restore-database',
  templateUrl: './restore-database.component.html',
  styleUrls: ['./restore-database.component.css']
})
export class RestoreDatabaseComponent {
  constructor(private dataservice:PedalProServiceService){

  }

  Logout()
  {
    this.dataservice.logout();
  }
}
