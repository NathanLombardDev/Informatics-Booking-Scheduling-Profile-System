import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { DateWithTimeslotDto } from 'src/app/Models/date-with-timeslot-dto';

@Component({
  selector: 'app-edittimeslots',
  templateUrl: './edittimeslots.component.html',
  styleUrls: ['./edittimeslots.component.css']
})
export class EdittimeslotsComponent {
  dateWithTimeslotDto: DateWithTimeslotDto = {
    date: new Date(),
    startTime: '',
    endTime: '',
    employeeId: 0,
    timeslotId:0
  };
  isNewTimeslot: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private service: PedalProServiceService
  ) { }

  ngOnInit() {
    this.isNewTimeslot = this.route.snapshot.paramMap.get('id') === 'new';
    if (!this.isNewTimeslot) {
      this.loadTimeslotDetails();
    }
  }

  loadTimeslotDetails() {
    const timeslotIdParam = this.route.snapshot.paramMap.get('id');
    if (timeslotIdParam !== null) {
      const timeslotId = +timeslotIdParam;
      this.service.getTimeslotById(timeslotId).subscribe(response => {
        this.dateWithTimeslotDto = response;
      });
    } else {
      // Handle the case when the timeslotId is null, e.g., show an error message or redirect to another page.
    }
  }

  saveTimeslot() {
    if (this.isNewTimeslot) {
      this.service.addTimeslot(this.dateWithTimeslotDto)
        .subscribe(response => {
          console.log('Timeslot created successfully:', response);
          this.goBack();
        }, error => {
          console.error('Error creating timeslot:', error);
        });
    } else {
      this.service.updateTimeslot(this.dateWithTimeslotDto.timeslotId, this.dateWithTimeslotDto)
        .subscribe(response => {
          console.log('Timeslot updated successfully:', response);
          this.goBack();
        }, error => {
          console.error('Error updating timeslot:', error);
        });
    }
  }

  goBack() {
    this.location.back();
  }

  Logout()
  {
    this.service.logout();
  }
}
