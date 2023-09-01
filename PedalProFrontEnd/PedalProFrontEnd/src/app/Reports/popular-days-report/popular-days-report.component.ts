import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';
import { ChartDataset } from 'chart.js'; // Import the ChartDataSets type
import html2canvas from 'html2canvas';
import * as jsPDF from 'jspdf';

@Component({
  selector: 'app-popular-days-report',
  templateUrl: './popular-days-report.component.html',
  styleUrls: ['./popular-days-report.component.css']
})
export class PopularDaysReportComponent implements OnInit {
  reportDatatwo: any = {}; // Initialize as an empty object
  chartLabels = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  chartData: ChartDataset[] = []; // Use ChartDataSets type
  chartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    elements: {
      arc: {
        backgroundColor: ['red', 'blue', 'green', 'black', 'purple', 'yellow', 'cyan']
      }
    }
  };

  constructor(private reportService: PedalProServiceService) {}

  ngOnInit() {
    this.reportService.getReportData().subscribe(data => {
      // Convert numbers to ChartDataSets
      this.chartData = [{
        data: [
          data.sunday,
          data.monday,
          data.tuesday,
          data.wednesday,
          data.thursday,
          data.friday,
          data.saturday
        ],
        label: 'Bookings'
      }];
    });

    this.fetchWorkoutReportData();
  }

  private fetchWorkoutReportData() {
    // Fetch workout report data from your backend API
    this.reportService.getReportData().subscribe((data) => {
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
      doc.save('popular-booking-days-report.pdf');
    });
  }
}