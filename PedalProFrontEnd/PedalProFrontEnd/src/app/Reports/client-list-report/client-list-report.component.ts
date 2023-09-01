import { Component ,OnInit} from '@angular/core';
import { ClientClient } from 'src/app/Models/client-client';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { ClientType } from 'src/app/Models/client-type';
import html2canvas from 'html2canvas';
import * as jsPDF from 'jspdf';

@Component({
  selector: 'app-client-list-report',
  templateUrl: './client-list-report.component.html',
  styleUrls: ['./client-list-report.component.css']
})
export class ClientListReportComponent {
  clients:ClientClient[]=[];
  reportDatatwo: any = {}; // Initialize as an empty object
  clientTypes:ClientType[]=[];
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
    
  }

  ngOnInit(): void {
    
    this.service.GetRoles().subscribe(data => console.log(data));
    
    this.GetClients();
    
    this.fetchWorkoutReportData();
    
    

    
  }

  GetClients()
  {
    this.service.GetClientsClients().subscribe(result=>{
      let roleList:any[]=result
      roleList.forEach((element)=>{
        this.clients.push(element)
      });
    })
    return this.clients;
  }

  

  ReloadPage()
  {
    location.reload();
  }



  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }

  
  cancel_continue(){
    this.router.navigate(['/viewClientsClients'])
  }

  GetClientType(id: any) {
    const categories = this.clientTypes.find(m => m.clientTypeId === id);
  
    if (categories) {
      return categories.clientTypeName;
    } else {
      this.service.GetClientType(id).subscribe(result => {
        this.clientTypes.push(result);
        return result.clientTypeName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Type does not exist';
  }

  
  Logout()
  {
    this.service.logout();
  }

  generatePDF() {
    const data = document.getElementById('report-content') as HTMLElement;
  
    html2canvas(data).then((canvas: HTMLCanvasElement) => {
      const imgData = canvas.toDataURL('image/png');
      const doc = new jsPDF.default();
  
      const imgProps = doc.getImageProperties(imgData);
      const pdfWidth = doc.internal.pageSize.getWidth() - 20; // Adjust as needed
      const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
  
      // Add a border to the PDF
      /*
      doc.setDrawColor(0); // Black color for border
      doc.rect(5, 5, pdfWidth + 10, pdfHeight + 150);
      */
      doc.addImage(imgData, 'PNG', 10, 10, pdfWidth, pdfHeight);
  
      // Add footer
      const generatedDate = new Date(this.reportDatatwo.generateddate);
      const formattedGeneratedDate = generatedDate.toLocaleDateString();
      const footerText = 'Generated on ' + formattedGeneratedDate+' by '+this.reportDatatwo.generateby;
      const footerX = doc.internal.pageSize.getWidth() / 2;
      const footerY = doc.internal.pageSize.getHeight() -3;
  
      // Set background color for the footer
      doc.setFillColor(0, 0, 0); // Set background color to black
      doc.rect(0, footerY - 6, doc.internal.pageSize.getWidth(), 10, 'F');
  
      // Set font color for the footer text
      doc.setFontSize(10);
      doc.setTextColor(255); // Set font color to white
      doc.text(footerText, footerX, footerY, { align: 'center' });
  
      // Save the PDF
      doc.save('client-list-report.pdf');
    });
  }

  private fetchWorkoutReportData() {
    // Fetch workout report data from your backend API
    this.service.getPackageReportData().subscribe((data) => {
      this.reportDatatwo = data;
    });
  }
}
