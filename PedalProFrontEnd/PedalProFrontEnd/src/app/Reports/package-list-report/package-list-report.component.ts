import { Component ,OnInit} from '@angular/core';
import { Package } from '../../Models/package';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable, Subject } from 'rxjs';
import { NgModule } from '@angular/core';
import { PackagePrice } from '../../Models/package-price';
import { Price } from '../../Models/price';
import html2canvas from 'html2canvas';
import * as jsPDF from 'jspdf';

@Component({
  selector: 'app-package-list-report',
  templateUrl: './package-list-report.component.html',
  styleUrls: ['./package-list-report.component.css']
})
export class PackageListReportComponent {
  constructor(private service:PedalProServiceService,private router:Router, private http:HttpClient){
  }

  packagePrices:PackagePrice[]=[];
  packages:Package[]=[];
  prices:Price[]=[];
  searchTerm:string='';
  reportDatatwo: any = {}; // Initialize as an empty object


  ngOnInit(): void {
    this.GetPackagePrices();
    this.fetchWorkoutReportData();
  }

  GetPackagePrices(){
    this.service.GetPackagePrices().subscribe(result=>{
      let packagePriceList:any[]=result
      packagePriceList.forEach((element)=>{
        this.packagePrices.push(element)
      });
    })
    return this.packagePrices;
  }

  GetPackageName(id: any) {
    const packaged = this.packages.find(m => m.packageId === id);
  
    if (packaged) {
      return packaged.packageName;
    } else {
      this.service.GetPackage(id).subscribe(result => {
        this.packages.push(result);
        return result.packageName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Package does not exist';

    
  }

  GetPackageDescription(id: any) {
    const packaged = this.packages.find(m => m.packageId === id);
  
    if (packaged) {
      return packaged.packageDescription;
    } else {
      this.service.GetPackage(id).subscribe(result => {
        this.packages.push(result);
        return result.packageDescription;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Package does not exist';

    
  }

  GetPriceAmount(id: any) {
    const price = this.prices.find(m => m.priceId === id);
  
    if (price) {
      return price.price1;
    } else {
      this.service.GetPrice(id).subscribe(result => {
        this.prices.push(result);
        return result.price1;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Price does not exist';

    
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
      doc.save('package-list-report.pdf');
    });
  }

  private fetchWorkoutReportData() {
    // Fetch workout report data from your backend API
    this.service.getPackageReportData().subscribe((data) => {
      this.reportDatatwo = data;
    });
  }
}
