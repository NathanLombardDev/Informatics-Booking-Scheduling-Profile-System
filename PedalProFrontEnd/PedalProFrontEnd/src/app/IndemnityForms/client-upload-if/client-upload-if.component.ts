import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { TrainingModule } from 'src/app/Models/training-module';

@Component({
  selector: 'app-client-upload-if',
  templateUrl: './client-upload-if.component.html',
  styleUrls: ['./client-upload-if.component.css']
})
export class ClientUploadIFComponent implements OnInit{
selectedFile: File | null = null;
  title: string = '';

  modules:TrainingModule[]=[];

  constructor(private documentUploadService: PedalProServiceService, private router:Router) {}

  ngOnInit(): void {
    this.GetModules();
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }

  onUpload() {
    if (!this.selectedFile || !this.title) {
      alert('Please select a file and provide a title.');
      return;
    }

    this.documentUploadService.uploadDocumentClient(this.selectedFile, this.title).subscribe(
      (response) => {
        console.log('Document uploaded successfully.', response);
        // You can perform any action here after a successful upload
        this.router.navigate(['/clientLanding'])
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
}
