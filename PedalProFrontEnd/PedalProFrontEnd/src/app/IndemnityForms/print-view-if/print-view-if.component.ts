import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { TrainingModule } from 'src/app/Models/training-module';
import { MatDialog } from '@angular/material/dialog';
import { ErrorDialogComponent } from 'src/app/Dialogs/error-dialog/error-dialog.component';

@Component({
  selector: 'app-print-view-if',
  templateUrl: './print-view-if.component.html',
  styleUrls: ['./print-view-if.component.css']
})
export class PrintViewIFComponent implements OnInit{
  documentUrl: SafeResourceUrl | undefined;
  documentContent: Blob | null = null;
  modules:TrainingModule[]=[];
  clientDetails: any;
  cartnumber:any;
  constructor(
    private documentService: PedalProServiceService,
    private sanitizer: DomSanitizer,
    private dialog:MatDialog
  ) {}

  ngOnInit(): void {
    this.getDocument();
    const storedCartQuantity = localStorage.getItem('cartQuantity');
    this.cartnumber = storedCartQuantity ? parseInt(storedCartQuantity, 10) : 0;
    this.fetchClientDetails();
  }

  fetchClientDetails() {
    this.documentService.getClientDetails().subscribe(
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

  getDocument(): void {
    this.documentService.getLatestDocument().subscribe(
      (documentBlob: Blob) => {
        this.documentContent = documentBlob;
        const blobUrl = URL.createObjectURL(documentBlob);
        this.documentUrl = this.sanitizer.bypassSecurityTrustUrl(blobUrl);
      },
      (err)=>{
        const errorMessage = err.error || 'An error occurred';
        const errmess='No form available'
        this.openErrorDialog(errmess);
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

  

