import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { PedalProServiceService } from '../Services/pedal-pro-service.service';
import { Timeslot } from '../Models/timeslot';
import { Router } from '@angular/router';
import { TrainingModule } from '../Models/training-module';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit{
  weekDays = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  currentDate: Date = new Date();
  dayNames: string[] = [];
  timeslots: Timeslot[] = [];
  calendarData: any[] = []; // Array to store calendar data with timeslots
  tooltipVisible: boolean = false;
  tooltipText: string = '';
  hoveredTimeslot: Timeslot | null = null;
  clickedTimeslot: Timeslot | null = null;

  

  constructor(private datePipe: DatePipe, private service: PedalProServiceService, private router: Router) {}

  public user = '';

  ngOnInit() {
    this.updateCalendar();
    this.GetModules();
  }

  currentMonth: Date = new Date();

  getDaysInMonth(month: Date): any[] {
    const firstDayOfMonth = new Date(month.getFullYear(), month.getMonth(), 1);
    const lastDayOfMonth = new Date(month.getFullYear(), month.getMonth() + 1, 0);
    const daysInMonth: any[] = [];

    for (let date = firstDayOfMonth; date <= lastDayOfMonth; date.setDate(date.getDate() + 1)) {
      const formattedDate = this.datePipe.transform(date, 'yyyy-MM-dd');
      daysInMonth.push({
        date,
        isCurrentMonth: date.getMonth() === month.getMonth(),
        formattedDate // Add the formatted date
      });
    }

    return daysInMonth;
  }

  updateCalendar(): void {
    const daysInMonth = this.getDaysInMonth(this.currentMonth);
  
    // Fetch timeslots for each day in the current month
    this.calendarData = daysInMonth.map((day) => ({
      ...day,
      timeslots: []
    }));
  
    // Set day names in the correct order for the current month
    this.dayNames = this.getOrderedDayNames(this.currentMonth);
  
    this.calendarData.forEach((day, index) => {
      if (day.isCurrentMonth) {
        const currentDate = new Date(this.currentMonth);
        currentDate.setDate(index + 1); // Set the date for each day in the month
        day.date = currentDate;
        this.service.getTimeslotsTwo(day.formattedDate).subscribe(
          (timeslots: Timeslot[]) => {
            day.timeslots = timeslots.sort((a, b) => this.compareTimes(a.startTime, b.startTime));
          },
          (error) => {
            // Handle error if necessary
          }
        );
      }
    });
  }
  
  compareTimes(timeA: string, timeB: string): number {
    const dateA = new Date(`1970-01-01T${timeA}`);
    const dateB = new Date(`1970-01-01T${timeB}`);
    return dateA.getTime() - dateB.getTime();
  }

  getOrderedDayNames(month: Date): string[] {
    const orderedDayNames: string[] = this.weekDays.slice();
    const firstDayOfMonth = new Date(month.getFullYear(), month.getMonth(), 1);

    // Find the index of the first day of the month in the weekDays array
    const firstDayIndex = this.weekDays.findIndex((day) => day === firstDayOfMonth.toLocaleDateString('en-US', { weekday: 'short' }));

    // Reorder the day names array based on the first day of the month
    if (firstDayIndex !== -1) {
      orderedDayNames.push(...orderedDayNames.splice(0, firstDayIndex));
    }

    return orderedDayNames;
  }

  previousMonth(): void {
    this.currentMonth = new Date(this.currentMonth.getFullYear(), this.currentMonth.getMonth() - 1, 1);
    this.updateCalendar();
  }

  nextMonth(): void {
    this.currentMonth = new Date(this.currentMonth.getFullYear(), this.currentMonth.getMonth() + 1, 1);
    this.updateCalendar();
  }

  showTooltip(timeslot: Timeslot): void {
    this.hoveredTimeslot = timeslot;
    this.tooltipText = `${timeslot.startTime} - ${timeslot.endTime}`;
    this.tooltipVisible = true;
  }

  hideTooltip(): void {
    this.hoveredTimeslot = null;
    this.tooltipVisible = false;
  }

  onTimeslotClick(timeslot: Timeslot): void {
    this.clickedTimeslot = timeslot;
    //alert(`Clicked Timeslot ID: ${timeslot.timeslotId}`);
    localStorage.setItem('User', JSON.stringify(this.user));
  }
  
  
  
  Logout()
  {
    this.service.logout();
  }

  modules:TrainingModule[]=[];
  
  
  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }
}
