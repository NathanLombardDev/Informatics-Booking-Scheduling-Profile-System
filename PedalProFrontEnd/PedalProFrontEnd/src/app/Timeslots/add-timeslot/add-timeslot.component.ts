import { Component ,OnInit} from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { Timeslot } from 'src/app/Models/timeslot';
import { Router } from '@angular/router';
import { Timeslotadd } from 'src/app/Models/timeslotadd';
import { Employee } from 'src/app/Models/employee';
import { CustomDate } from 'src/app/Models/custom-date';
import { DatePipe } from '@angular/common'; // Import the DatePipe
import { DateWithTimeslotDto } from 'src/app/Models/date-with-timeslot-dto';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-add-timeslot',
  templateUrl: './add-timeslot.component.html',
  styleUrls: ['./add-timeslot.component.css']
})
export class AddTimeslotComponent implements OnInit{
  dateWithTimeslotDto: DateWithTimeslotDto = {
    date: new Date(),
    startTime: '',
    endTime: '',
    employeeId: 0,
    timeslotId:0
  };

  employees:Employee[]=[];

  constructor(private dialog:MatDialog,private service: PedalProServiceService,private router:Router) { }

  createTimeslot() {
    this.service.addTimeslot(this.dateWithTimeslotDto)
      .subscribe(response => {
        console.log('Timeslot created successfully:', response);
        this.openModal();
      }, err=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      });
  }

  getCurrentDate(): Date {
    return new Date(); // Returns the current date
  }
  
  GetModules(){
    this.service.GetEmployeesTwo().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this.employees.push(element)
      });
    })
    return this.employees;
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }
  

  ngOnInit(): void {
    this.GetModules();
  }

  cancel_continue() {
    this.router.navigate(['/ViewSchedule']);
  }

  // modal pop-up
  openModal() {
    const modelDiv = document.getElementById('myModal');
    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  Logout()
  {
    this.service.logout();
  }
}
