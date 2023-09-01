import { Component ,OnInit} from '@angular/core';
import html2canvas from 'html2canvas';
import * as jsPDF from 'jspdf';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-revenue-report',
  templateUrl: './revenue-report.component.html',
  styleUrls: ['./revenue-report.component.css']
})
export class RevenueReportComponent implements OnInit{
  reportDatatwo: any = {}; // Initialize as an empty object
  
  constructor(private service: PedalProServiceService){}
  
  ngOnInit(): void {
    this.fetchWorkoutReportData();
  }

  private fetchWorkoutReportData() {
    // Fetch workout report data from your backend API
    this.service.getRevenueportData().subscribe((data) => {
      this.reportDatatwo = data;
    });
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
      doc.setDrawColor(0); // Black color for border
      doc.rect(5, 5, pdfWidth + 10, pdfHeight + 150);
  
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
      doc.save('revenue-report.pdf');
    });
  }
}
