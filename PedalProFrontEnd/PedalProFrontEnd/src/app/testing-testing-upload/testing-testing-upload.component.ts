import { Component } from '@angular/core';
import { PedalProServiceService } from '../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-testing-testing-upload',
  templateUrl: './testing-testing-upload.component.html', // Use a separate HTML file
  styleUrls: ['./testing-testing-upload.component.css'] // Use a separate SCSS file for styles
})
export class TestingTestingUploadComponent {
  selectedFile: File | null = null;

  constructor(private dialog:MatDialog,private testImageUploadService: PedalProServiceService,private router:Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  openErrorDialog(errorMessage: string): void {
    this.dialog.open(ErrorDialogComponent, {
      data: { message: errorMessage }
    });
  }

  uploadImage() {
    if (this.selectedFile) {
      this.testImageUploadService.uploadProfileImage(this.selectedFile).subscribe(
        (response) => {
          console.log('Image uploaded successfully:', response);
          this.openModal();
        },
        (err)=>{
          const errorMessage = err.error || 'An error occurred';
          this.openErrorDialog(errorMessage);
        }
      );
    } else {
      this.openErrorDialog('No image selected.');
      
    }
  }

  cancel_continue(){
    this.router.navigate(['ViewAccount'])
  }


// modal pop-up
  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
}
