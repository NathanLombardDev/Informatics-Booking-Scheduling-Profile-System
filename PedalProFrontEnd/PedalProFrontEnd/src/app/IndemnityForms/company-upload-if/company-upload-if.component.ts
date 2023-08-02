import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-company-upload-if',
  templateUrl: './company-upload-if.component.html',
  styleUrls: ['./company-upload-if.component.css']
})
export class CompanyUploadIFComponent {
  selectedFile: File | null = null;
  title: string = '';

  constructor(private documentUploadService: PedalProServiceService, private router:Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }

  onUpload() {
    if (!this.selectedFile || !this.title) {
      alert('Please select a file and provide a title.');
      return;
    }

    this.documentUploadService.uploadDocument(this.selectedFile, this.title).subscribe(
      (response) => {
        console.log('Document uploaded successfully.', response);
        // You can perform any action here after a successful upload
        this.router.navigate(['/companyLanding'])
      },
      (error) => {
        console.error('Error uploading the document:', error);
        // Handle the error here
      }
    );
  }

  Logout()
  {
    this.documentUploadService.logout();
  }
}
