import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { TrainingModule } from 'src/app/Models/training-module';

@Component({
  selector: 'app-print-view-if',
  templateUrl: './print-view-if.component.html',
  styleUrls: ['./print-view-if.component.css']
})
export class PrintViewIFComponent implements OnInit{
  documentUrl: SafeResourceUrl | undefined;
  documentContent: Blob | null = null;
  modules:TrainingModule[]=[];

  constructor(
    private documentService: PedalProServiceService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.getDocument();
  }

  getDocument(): void {
    this.documentService.getLatestDocument().subscribe(
      (documentBlob: Blob) => {
        this.documentContent = documentBlob;
        const blobUrl = URL.createObjectURL(documentBlob);
        this.documentUrl = this.sanitizer.bypassSecurityTrustUrl(blobUrl);
      },
      (error) => {
        console.error('Error fetching the document:', error);
      }
    );
  }

  downloadDocument(): void {
    if (this.documentContent) {
      const downloadLink = document.createElement('a');
      const blobUrl = URL.createObjectURL(this.documentContent);
      downloadLink.href = blobUrl;
      downloadLink.download = 'document.docx';
      downloadLink.click();
      URL.revokeObjectURL(blobUrl);
    }
  }

  GetModules(){
    this.documentService.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }

  Logout()
  {
    this.documentService.logout();
  }

  
}

  

