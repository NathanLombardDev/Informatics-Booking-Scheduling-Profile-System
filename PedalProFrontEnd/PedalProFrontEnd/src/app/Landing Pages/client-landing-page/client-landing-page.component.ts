
import { Component ,OnInit} from '@angular/core';
import { PedalProRole } from 'src/app/Models/pedal-pro-role';
import { TrainingModule } from '../../Models/training-module';
import { ModuleStatus } from '../../Models/module-status';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-client-landing-page',
  templateUrl: './client-landing-page.component.html',
  styleUrls: ['./client-landing-page.component.css']
})
export class ClientLandingPageComponent implements OnInit{
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){}
  modules:TrainingModule[]=[];
  
  ngOnInit(): void {
    this.GetModules();
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
  viewAccountDetails() {
    // Implement the functionality to show the account details or navigate to the account details page.
    alert('View Account Details clicked');
  }

}
