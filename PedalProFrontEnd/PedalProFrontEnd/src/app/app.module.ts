import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CalendarModule, DateAdapter, CalendarMonthModule } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { HttpClientModule } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { NgxDocViewerModule } from 'ngx-doc-viewer'; // Import the module


import { CalendarMonthViewComponent } from 'angular-calendar';
import { RouterModule } from '@angular/router';

import { MatGridListModule } from '@angular/material/grid-list';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { JwtInterceptor } from './Interceptor/JwtInterceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { PedalProServiceService } from './Services/pedal-pro-service.service';
import {MatSidenavModule} from '@angular/material/sidenav';
import { LoginComponent } from './Authentication/login/login.component';
import { RegisterComponent } from './Authentication/register/register.component';
import { ResetComponent } from './Authentication/reset/reset.component';
import { ForgotComponent } from './Authentication/forgot/forgot.component';
import { ClientLandingPageComponent } from './Landing Pages/client-landing-page/client-landing-page.component';
import { CalendarComponent } from './calendar/calendar.component';
import { CompanyLandingPageComponent } from './Landing Pages/company-landing-page/company-landing-page.component';
import { MakeBookingComponent } from './Bookings/make-booking/make-booking.component';
import { AddBicycleComponent } from './Bicycle/add-bicycle/add-bicycle.component';
import { ViewBicycleComponent } from './Bicycle/view-bicycle/view-bicycle.component';
import { EditBicycleComponent } from './Bicycle/edit-bicycle/edit-bicycle.component';
import { AddBicycleCategoryComponent } from './BikeCategory/add-bicycle-category/add-bicycle-category.component';
import { EditBicycleCategoryComponent } from './BikeCategory/edit-bicycle-category/edit-bicycle-category.component';
import { BicycleCategoryComponent } from './BikeCategory/bicycle-category/bicycle-category.component';
import { BicycleBrandComponent } from './BicycleBrand/bicycle-brand/bicycle-brand.component';
import { AddBicycleBrandComponent } from './BicycleBrand/add-bicycle-brand/add-bicycle-brand.component';
import { EditBicycleBrandComponent } from './BicycleBrand/edit-bicycle-brand/edit-bicycle-brand.component';
import { BicyclePartComponent } from './BikePart/bicycle-part/bicycle-part.component';
import { AddBicyclePartComponent } from './BikePart/add-bicycle-part/add-bicycle-part.component';
import { EditBicyclePartComponent } from './BikePart/edit-bicycle-part/edit-bicycle-part.component';
import { ClientTypeComponent } from './ClientType/client-type/client-type.component';
import { AddClientTypeComponent } from './ClientType/add-client-type/add-client-type.component';
import { EditClientTypeComponent } from './ClientType/edit-client-type/edit-client-type.component';
import { AddModuleComponent } from './CompanyModule/add-module/add-module.component';
import { EditModuleComponent } from './CompanyModule/edit-module/edit-module.component';
import { TrainingModuleCompanyComponent } from './CompanyModule/training-module-company/training-module-company.component';
import { EmployeeComponent } from './Employ/employee/employee.component';
import { AddEmployeeComponent } from './Employ/add-employee/add-employee.component';
import { EditEmployeeComponent } from './Employ/edit-employee/edit-employee.component';
import { EditEmployeeTypeComponent } from './EmployeeType/edit-employee-type/edit-employee-type.component';
import { AddEmployeeTypeComponent } from './EmployeeType/add-employee-type/add-employee-type.component';
import { EmployeeTypeComponent } from './EmployeeType/employee-type/employee-type.component';
import { TrainingMaterialComponent } from './Material/training-material/training-material.component';
import { AddMaterialComponent } from './Material/add-material/add-material.component';
import { EditMaterialComponent } from './Material/edit-material/edit-material.component';
import { PackageComponent } from './Packages/package/package.component';
import { AddPackageComponent } from './Packages/add-package/add-package.component';
import { EditPackageComponent } from './Packages/edit-package/edit-package.component';
import { EditRoleComponent } from './PedalProRole/edit-role/edit-role.component';
import { AddRoleComponent } from './PedalProRole/add-role/add-role.component';
import { PedalProRoleComponent } from './PedalProRole/pedal-pro-role/pedal-pro-role.component';
import { WorkoutComponent } from './Workouts/workout/workout.component';
import { AddWorkoutComponent } from './Workouts/add-workout/add-workout.component';
import { ViewClientComponent } from './ViewClient/view-client/view-client.component';
import { ViewHelpComponent } from './Help/view-help/view-help.component';
import { MaterialModule } from './Shared/material.module';
import { ViewHelpCompanyComponent } from './Help/view-help-company/view-help-company.component';
import { HelpComponent } from './Help/help/help.component';
import { AddHelpComponent } from './Help/add-help/add-help.component';
import { EditHelpComponent } from './Help/edit-help/edit-help.component';
import { ViewAccountDetailsComponent } from './ClientDetails/view-account-details/view-account-details.component';
import { UpdateAccountDetailsComponent } from './ClientDetails/update-account-details/update-account-details.component';
import { ViewbookingsComponent } from './Bookings/viewbookings/viewbookings.component';
import { BookingTypeComponent } from './Bookings/BookingTypes/booking-type/booking-type.component';
import { AddBookingTypeComponent } from './Bookings/BookingTypes/add-booking-type/add-booking-type.component';
import { EditBookingTypeComponent } from './Bookings/BookingTypes/edit-booking-type/edit-booking-type.component';
import { WorkoutTypeComponent } from './Workouts/WorkoutTypes/workout-type/workout-type.component';
import { AddWorkoutTypeComponent } from './Workouts/WorkoutTypes/add-workout-type/add-workout-type.component';
import { EditWorkoutTypeComponent } from './Workouts/WorkoutTypes/edit-workout-type/edit-workout-type.component';
import { CompanyUploadIFComponent } from './IndemnityForms/company-upload-if/company-upload-if.component';
import { ClientUploadIFComponent } from './IndemnityForms/client-upload-if/client-upload-if.component';
import { PrintViewIFComponent } from './IndemnityForms/print-view-if/print-view-if.component';
import { AddTimeslotComponent } from './Timeslots/add-timeslot/add-timeslot.component';
import { ViewtimeslotsComponent } from './Timeslots/add-timeslot/viewtimeslots/viewtimeslots.component';
import { EdittimeslotsComponent } from './Timeslots/edittimeslots/edittimeslots.component';
import { ViewScheduleComponent } from './Schedule/view-schedule/view-schedule.component';
import { MaterialContentComponent } from './ClientMaterial/material-content/material-content.component';
import { ViewAvailPackagesComponent } from './ClientPackages/view-avail-packages/view-avail-packages.component';
import { ViewFeedbackComponent } from './Feedback/view-feedback/view-feedback.component';
import { ProvideFeedbackComponent } from './Feedback/provide-feedback/provide-feedback.component';
import { DeleteDialogComponent } from './Dialogs/delete-dialog/delete-dialog.component';


import { MatDialogModule } from '@angular/material/dialog';
import { AddCartDialogComponent } from './Dialogs/add-cart-dialog/add-cart-dialog.component';
import { ViewCartComponent } from './Cart/view-cart/view-cart.component';























@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ResetComponent,
    ForgotComponent,
    ClientLandingPageComponent,
    CalendarComponent,
    CompanyLandingPageComponent,
    MakeBookingComponent,
    AddBicycleComponent,
    ViewBicycleComponent,
    EditBicycleComponent,
    AddBicycleCategoryComponent,
    EditBicycleCategoryComponent,
    BicycleCategoryComponent,
    BicycleBrandComponent,
    AddBicycleBrandComponent,
    EditBicycleBrandComponent,
    BicyclePartComponent,
    AddBicyclePartComponent,
    EditBicyclePartComponent,
    ClientTypeComponent,
    AddClientTypeComponent,
    EditClientTypeComponent,
    AddModuleComponent,
    EditModuleComponent,
    TrainingModuleCompanyComponent,
    EmployeeComponent,
    AddEmployeeComponent,
    EditEmployeeComponent,
    EditEmployeeTypeComponent,
    AddEmployeeTypeComponent,
    EmployeeTypeComponent,
    TrainingMaterialComponent,
    AddMaterialComponent,
    EditMaterialComponent,
    PackageComponent,
    AddPackageComponent,
    EditPackageComponent,
    EditRoleComponent,
    AddRoleComponent,
    PedalProRoleComponent,
    WorkoutComponent,
    AddWorkoutComponent,
    ViewClientComponent,
    ViewHelpComponent,
    ViewHelpCompanyComponent,
    HelpComponent,
    AddHelpComponent,
    EditHelpComponent,
    ViewAccountDetailsComponent,
    UpdateAccountDetailsComponent,
    ViewbookingsComponent,
    BookingTypeComponent,
    AddBookingTypeComponent,
    EditBookingTypeComponent,
    WorkoutTypeComponent,
    AddWorkoutTypeComponent,
    EditWorkoutTypeComponent,
    CompanyUploadIFComponent,
    ClientUploadIFComponent,
    PrintViewIFComponent,
    AddTimeslotComponent,
    ViewtimeslotsComponent,
    EdittimeslotsComponent,
    ViewScheduleComponent,
    MaterialContentComponent,
    ViewAvailPackagesComponent,
    ViewFeedbackComponent,
    ProvideFeedbackComponent,
    DeleteDialogComponent,
    AddCartDialogComponent,
    ViewCartComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CalendarModule.forRoot({ provide: DateAdapter, useFactory: adapterFactory }),
    CalendarMonthModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule,
    MatGridListModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatSidenavModule,
    MaterialModule,
    NgxDocViewerModule,
    MatDialogModule
    
  ],
  providers: [DatePipe,PedalProServiceService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
