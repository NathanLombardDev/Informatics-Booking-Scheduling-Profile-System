import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Authentication/login/login.component';
import { RegisterComponent } from './Authentication/register/register.component';
import { ResetComponent } from './Authentication/reset/reset.component';
import { ForgotComponent } from './Authentication/forgot/forgot.component';
import { ClientLandingPageComponent } from './Landing Pages/client-landing-page/client-landing-page.component';
import { CalendarComponent } from './calendar/calendar.component';
import { CompanyLandingPageComponent } from './Landing Pages/company-landing-page/company-landing-page.component';
import { MakeBookingComponent } from './Bookings/make-booking/make-booking.component';
import { ViewBicycleComponent } from './Bicycle/view-bicycle/view-bicycle.component';
import { AddBicycleComponent } from './Bicycle/add-bicycle/add-bicycle.component';
import { EditBicycleComponent } from './Bicycle/edit-bicycle/edit-bicycle.component';
import { PedalProRoleComponent } from './PedalProRole/pedal-pro-role/pedal-pro-role.component';
import { AddRoleComponent } from './PedalProRole/add-role/add-role.component';
import { EditRoleComponent } from './PedalProRole/edit-role/edit-role.component';
import { TrainingModuleCompanyComponent } from './CompanyModule/training-module-company/training-module-company.component';
import { AddModuleComponent } from './CompanyModule/add-module/add-module.component';
import { EditModuleComponent } from './CompanyModule/edit-module/edit-module.component';
import { TrainingMaterialComponent } from './Material/training-material/training-material.component';
import { AddMaterialComponent } from './Material/add-material/add-material.component';
import { EditMaterialComponent } from './Material/edit-material/edit-material.component';
import { EmployeeComponent } from './Employ/employee/employee.component';
import { AddEmployeeComponent } from './Employ/add-employee/add-employee.component';
import { EmployeeTypeComponent } from './EmployeeType/employee-type/employee-type.component';
import { AddEmployeeTypeComponent } from './EmployeeType/add-employee-type/add-employee-type.component';
import { EditEmployeeTypeComponent } from './EmployeeType/edit-employee-type/edit-employee-type.component';
import { EditEmployeeComponent } from './Employ/edit-employee/edit-employee.component';
import { EditClientTypeComponent } from './ClientType/edit-client-type/edit-client-type.component';
import { AddClientTypeComponent } from './ClientType/add-client-type/add-client-type.component';
import { ClientTypeComponent } from './ClientType/client-type/client-type.component';
import { EditPackageComponent } from './Packages/edit-package/edit-package.component';
import { AddPackageComponent } from './Packages/add-package/add-package.component';
import { PackageComponent } from './Packages/package/package.component';
import { BicyclePartComponent } from './BikePart/bicycle-part/bicycle-part.component';
import { AddBicyclePartComponent } from './BikePart/add-bicycle-part/add-bicycle-part.component';
import { EditBicyclePartComponent } from './BikePart/edit-bicycle-part/edit-bicycle-part.component';
import { BicycleCategoryComponent } from './BikeCategory/bicycle-category/bicycle-category.component';
import { AddBicycleCategoryComponent } from './BikeCategory/add-bicycle-category/add-bicycle-category.component';
import { EditBicycleCategoryComponent } from './BikeCategory/edit-bicycle-category/edit-bicycle-category.component';
import { BicycleBrandComponent } from './BicycleBrand/bicycle-brand/bicycle-brand.component';
import { AddBicycleBrandComponent } from './BicycleBrand/add-bicycle-brand/add-bicycle-brand.component';
import { EditBicycleBrandComponent } from './BicycleBrand/edit-bicycle-brand/edit-bicycle-brand.component';
import { WorkoutComponent } from './Workouts/workout/workout.component';
import { AddWorkoutComponent } from './Workouts/add-workout/add-workout.component';
import { ViewClientComponent } from './ViewClient/view-client/view-client.component';
import { ViewHelpComponent } from './Help/view-help/view-help.component';
import { ViewHelpCompanyComponent } from './Help/view-help-company/view-help-company.component';
import { HelpComponent } from './Help/help/help.component';
import { AddHelpComponent } from './Help/add-help/add-help.component';
import { EditHelpComponent } from './Help/edit-help/edit-help.component';
import { ViewAccountDetailsComponent } from './ClientDetails/view-account-details/view-account-details.component';
import { UpdateAccountDetailsComponent } from './ClientDetails/update-account-details/update-account-details.component';
import { ViewbookingsComponent } from './Bookings/viewbookings/viewbookings.component';
import { BookingTypeComponent } from './Bookings/BookingTypes/booking-type/booking-type.component';
import { EditBookingTypeComponent } from './Bookings/BookingTypes/edit-booking-type/edit-booking-type.component';
import { AddBookingTypeComponent } from './Bookings/BookingTypes/add-booking-type/add-booking-type.component';
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
import { ProvideFeedbackComponent } from './Feedback/provide-feedback/provide-feedback.component';
import { ViewFeedbackComponent } from './Feedback/view-feedback/view-feedback.component';
import { ViewCartComponent } from './Cart/view-cart/view-cart.component';
import { SuccessCheckoutComponent } from './Cart/success-checkout/success-checkout.component';
import { UnSuccessfullCheckoutComponent } from './Cart/un-successfull-checkout/un-successfull-checkout.component';
import { WorkoutReportComponent } from './Reports/workout-report/workout-report.component';
import { PackageReportComponent } from './Reports/package-report/package-report.component';
import { PopularDaysReportComponent } from './Reports/popular-days-report/popular-days-report.component';
import { SuccessBookingPaymentComponent } from './Bookings/success-booking-payment/success-booking-payment.component';
import { UnsuccessBookingPaymentComponent } from './Bookings/unsuccess-booking-payment/unsuccess-booking-payment.component';
import { RevenueReportComponent } from './Reports/revenue-report/revenue-report.component';
import { PackageListReportComponent } from './Reports/package-list-report/package-list-report.component';
import { ClientListReportComponent } from './Reports/client-list-report/client-list-report.component';
import { StaffReportComponent } from './Reports/staff-report/staff-report.component';
import { TestingTestingUploadComponent } from './testing-testing-upload/testing-testing-upload.component';
import { ReactivateAccountComponent } from './ClientDetails/reactivate-account/reactivate-account.component';
import { RestoreDatabaseComponent } from './Database/restore-database/restore-database.component';


const routes: Routes = [
  {path:'login',component:LoginComponent},
  {path:'',redirectTo:'/login',pathMatch:'full'},
  {path:'register',component:RegisterComponent},
  {path:'reset',component:ResetComponent},
  {path:'forgot',component:ForgotComponent},
  {path:'clientLanding',component:ClientLandingPageComponent},
  {path:'calendar', component:CalendarComponent},
  {path:'companyLanding',component:CompanyLandingPageComponent},
  {path:'booking/make/:id',component:MakeBookingComponent},{path:'',
  component:LoginComponent
},
{path:'pedalprorole',
  component:PedalProRoleComponent
},
{path:'pedalprorole/add',
  component:AddRoleComponent
},
{path:'pedalprorole/edit/:id',
  component:EditRoleComponent
},

{
  path:'traingModuleCompany',component:TrainingModuleCompanyComponent
},
{
  path:'addModule',component:AddModuleComponent
},
{
  path:'trainingmoduleCompany/edit/:id', component:EditModuleComponent
},
{
  path:'trainingmaterial',component:TrainingMaterialComponent
},
{
  path:'trainingmaterial/add',component: AddMaterialComponent
},
{
  path:'trainingmaterial/edit/:id',component:EditMaterialComponent
},
{
  path:'viewEmployees',component:EmployeeComponent
},
{
  path:'addEmployee',component:AddEmployeeComponent
},
{
  path:'viewEmployeeTypes',component:EmployeeTypeComponent
},
{
  path:'addEmployeeTypes',component:AddEmployeeTypeComponent
},
{
  path:'EmployeeType/edit/:id',component:EditEmployeeTypeComponent
},
{
  path:'Employee/edit/:id',component:EditEmployeeComponent
},
{
  path:'ViewPackages',component:PackageComponent
},
{
  path:'AddPackage',component:AddPackageComponent
},
{
  path:'Package/edit/:id',component:EditPackageComponent
},
{
  path:'ClientType',component:ClientTypeComponent
},
{
  path:'AddClientType',component:AddClientTypeComponent
},
{
  path:'ClientType/edit/:id',component:EditClientTypeComponent
},
{
  path:'Bicycle',component:ViewBicycleComponent
},
{
  path:'AddBicycle',component:AddBicycleComponent
},
{
  path:'Bicycle/edit/:id',component:EditBicycleComponent
},
{
  path:'BicyclePart',component:BicyclePartComponent
},
{
  path:'AddBicyclePart',component:AddBicyclePartComponent
},
{
  path:'BicyclePart/edit/:id',component:EditBicyclePartComponent
},
{
  path:'BicycleCategory',component:BicycleCategoryComponent
},
{
  path:'AddBicycleCategory',component:AddBicycleCategoryComponent
},
{
  path:'BicycleCategory/edit/:id',component:EditBicycleCategoryComponent
},
{
  path:'BicycleBrand',component:BicycleBrandComponent
},
{
  path:'AddBicycleBrand',component:AddBicycleBrandComponent
},
{
  path:'BicycleBrand/edit/:id',component:EditBicycleBrandComponent
},

  {
    path:'Bicycle',component:ViewBicycleComponent
  },
  {
    path:'AddBicycle',component:AddBicycleComponent
  },
  {
    path:'Bicycle/edit/:id',component:EditBicycleComponent
  },
  {path:'Workouts',component:WorkoutComponent},
  {path:'AddWorkout',component:AddWorkoutComponent},
  {path:'viewClientsClients',component:ViewClientComponent},
  {path:'view-help',component:ViewHelpComponent},
  {path:'view-help-company',component:ViewHelpCompanyComponent},
  {
    path:'view-managehelp',component:HelpComponent
  },
  {
    path:'addHelp',component:AddHelpComponent
   },
   {
    path:'viewManageHelp/edit/:id',component:EditHelpComponent
   },
   {path:'ViewAccount',component:ViewAccountDetailsComponent},
   {path:'UpdateAccount',component:UpdateAccountDetailsComponent},
   {path:'ViewClientBookings',component:ViewbookingsComponent},
   {
    path:'viewBookingType',component:BookingTypeComponent
  },
   { 
    path:'addBookingType',component:AddBookingTypeComponent
   },
   {
    path:'updatebooking/edit/:id',component:EditBookingTypeComponent
   },
   {
    path:'WorkoutType',component:WorkoutTypeComponent
  },
  {
    path:'AddWorkoutType',component:AddWorkoutTypeComponent
  },
  {
    path:'WorkoutType/edit/:id',component:EditWorkoutTypeComponent
  },
  {path:'CompanyUploadIF',component:CompanyUploadIFComponent},
  {path:'ClientUploadIF',component:ClientUploadIFComponent},
  {path:'PrintViewIF',component:PrintViewIFComponent},
  {path:'AddTimeSlot',component:AddTimeslotComponent},
  {path:'Viewtimeslot',component:ViewtimeslotsComponent},
  {
    path:'EditTimeSlot/edit/:id',component:EdittimeslotsComponent
   },
   {
    path:'ViewSchedule',component:ViewScheduleComponent
   },
   {path:'MaterialContent/edit/:id',component:MaterialContentComponent},
   {path:'ViewAvailPackages',component:ViewAvailPackagesComponent},
   {path:'ProvideFeedback',component:ProvideFeedbackComponent},
   {path:'ViewFeedback',component:ViewFeedbackComponent},
   {path:'ViewCart',component:ViewCartComponent},
   {path:'SuccessfulPayment', component:SuccessCheckoutComponent},
   {path:'UnSuccessfulPayment',component:UnSuccessfullCheckoutComponent},
   {path:'WorkoutReport',component:WorkoutReportComponent},
   {path:'PackageReport',component:PackageReportComponent},
   {path:'PopularDaysReport',component:PopularDaysReportComponent},
   {path:'SuccessfulBookingPayment', component:SuccessBookingPaymentComponent},
   {path:'UnSuccessfulBookingPayment',component:UnsuccessBookingPaymentComponent},
   {path:'RevenueReport',component:RevenueReportComponent},
   {path:'PackageListReport',component:PackageListReportComponent},
   {path:'ClientListReport',component:ClientListReportComponent},
   {path:'StaffReport',component:StaffReportComponent},
   {path:'testingpage',component:TestingTestingUploadComponent},
   {path:'Reactivate',component:ReactivateAccountComponent},
   {path:'RestoreDatabase',component:RestoreDatabaseComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
