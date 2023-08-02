import { Component, OnInit } from '@angular/core';
import { PedalProServiceService } from '../../Services/pedal-pro-service.service';
import { ClientType } from '../../Models/client-type';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingMaterial } from 'src/app/Models/training-material';
import { VideoLink } from 'src/app/Models/video-link';
import { MaterialVid } from 'src/app/Models/material-vid';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { TrainingModule } from 'src/app/Models/training-module';

@Component({
  selector: 'app-material-content',
  templateUrl: './material-content.component.html',
  styleUrls: ['./material-content.component.css']
})
export class MaterialContentComponent implements OnInit{
  materialMaterial: MaterialVid[] = [];
  videoUrls: { [key: number]: string } = {}; // Object to store video URLs by videoLinkId
  moduleTwoTwo:TrainingModule[]=[];
  numbermodule:number=0;
  modules:TrainingModule[]=[];

  constructor(
    private route: ActivatedRoute,
    private service: PedalProServiceService,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          const materialId = Number(id);
          this.numbermodule=Number(id);
          this.service.getMaterialVids(materialId).subscribe({
            next: (response) => {
              this.materialMaterial = response;
              this.fetchVideoUrls(); // Fetch video URLs after getting the materials
            },
            error: (err) => {
              console.error('Error fetching material:', err);
            }
          });
        }
      },
      error: (err) => {
        console.error('Error retrieving route parameter:', err);
      }
    });

    this.GetModules();
  }

  fetchVideoUrls(): void {
    // Fetch video URLs for all materials and store them in the videoUrls property
    for (const material of this.materialMaterial) {
      this.service.GetVideoLink(material.videoLinkId).subscribe(
        (result) => {
          this.videoUrls[material.videoLinkId] = result.videoUrl;
        },
        (error) => {
          console.error('Error fetching video URL:', error);
        }
      );
    }
  }
  GetModules(){
    this.service.GetModules().subscribe(result=>{
      let moduleList:any[]=result
      moduleList.forEach((element)=>{
        this. modules.push(element)
      });
    })
    return this.modules;
  }

  GetModule(id: number): SafeResourceUrl | undefined {
    const videoUrl = this.videoUrls[id];
    if (videoUrl) {
      // Sanitize the video URL to avoid security risks (XSS)
      return this.sanitizer.bypassSecurityTrustResourceUrl(videoUrl);
    } else {
      // Handle the case where the module is not found
      return undefined;
    }
  }

  GetModuleTwoTwo(id: any) {
    const modules = this.moduleTwoTwo.find(m => m.trainingModuleId === id);
  
    if (modules) {
      return modules.trainingModuleName;
    } else {
      this.service.GetModuleTwo(id).subscribe(result => {
        this.moduleTwoTwo.push(result);
        return result.trainingModuleName;
      });
    }
  
    // add a return statement here to handle the case where the module is not found
    return 'Module does not exist';

    
  }

  Logout()
  {
    this.service.logout();
  }
}

