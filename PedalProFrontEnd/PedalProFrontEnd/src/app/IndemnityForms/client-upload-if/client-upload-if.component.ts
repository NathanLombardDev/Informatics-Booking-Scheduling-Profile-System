import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { TrainingModule } from 'src/app/Models/training-module';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-client-upload-if',
  templateUrl: './client-upload-if.component.html',
  styleUrls: ['./client-upload-if.component.css']
})
export class ClientUploadIFComponent implements OnInit{
  selectedFile: File | null = null;
  selectedFileTwo: File | null = null;
  title: string = '';
  clientDetails: any;
  cartnumber:any;
  modules:TrainingModule[]=[];

  constructor(private dialog:MatDialog,private documentUploadService: PedalProServiceService, private router:Router) {}

  ngOnInit(): void {
    this.GetModules();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }

  fetchClientDetails() {
    this.documentUploadService.getClientDetails().subscribe(
      (response) => {
        this.clientDetails = response;
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  onUpload() {
    if (!this.selectedFile || !this.title) {
      this.openErrorDialog('Please select a file and provide a title.');
      return;
    }

    this.documentUploadService.uploadDocumentClient(this.selectedFile, this.title).subscribe(
      (response) => {
        console.log('Document uploaded successfully.', response);
        // You can perform any action here after a successful upload
        this.router.navigate(['/clientLanding'])
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }

  Logout()
  {
    this.documentUploadService.logout();
  }

  // get modules method
  GetModules(){
    this.documentUploadService.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }

  onFileSelectedtwo(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onUploadtwo() {
    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.documentUploadService.uploadImageIndemnity(formData).subscribe(
      (response) => {
        console.log(response); // Handle the response from the API
        this.router.navigate(['/clientLanding'])
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        this.openErrorDialog(errorMessage);
      }
    );
  }
}
