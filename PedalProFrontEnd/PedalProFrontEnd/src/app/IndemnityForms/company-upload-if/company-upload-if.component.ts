import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';
@Component({
  selector: 'app-company-upload-if',
  templateUrl: './company-upload-if.component.html',
  styleUrls: ['./company-upload-if.component.css']
})
export class CompanyUploadIFComponent {
  selectedFile: File | null = null;
  title: string = '';

  constructor(private dialog:MatDialog,private documentUploadService: PedalProServiceService, private router:Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
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

    this.documentUploadService.uploadDocument(this.selectedFile, this.title).subscribe(
      (response) => {
        console.log('Document uploaded successfully.', response);
        // You can perform any action here after a successful upload
        this.router.navigate(['/companyLanding'])
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
}
