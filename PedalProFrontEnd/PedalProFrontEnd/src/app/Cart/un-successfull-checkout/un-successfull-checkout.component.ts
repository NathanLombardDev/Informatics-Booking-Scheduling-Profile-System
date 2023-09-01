import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { PedalProServiceService } from 'src/app/Services/pedal-pro-service.service';

@Component({
  selector: 'app-un-successfull-checkout',
  templateUrl: './un-successfull-checkout.component.html',
  styleUrls: ['./un-successfull-checkout.component.css']
})
export class UnSuccessfullCheckoutComponent implements OnInit{
  
constructor(private service:PedalProServiceService, private router:Router){}
disableBackButton: boolean = true;
ngOnInit(): void {
  const token = this.service.getToken();

    if (token && this.service.isTokenValid(token)) {
      // Token is valid, user is authenticated
      // Display payment success content
      this.openModal();
    } else {
      // Token is invalid or missing, handle accordingly
    }


    history.pushState({}, '', window.location.href);
    window.onpopstate = (event) => {
      if (this.disableBackButton) {
        event.preventDefault();
      }
    };

    this.preventBackButton();
}

Logout()
  {
    this.service.logout();
  }

  cancel_continue(){
    this.router.navigate(['clientLanding']);
  }

  preventBackButton(): void {
    history.replaceState(null, document.title, location.href);
    window.addEventListener('popstate', () => {
      history.pushState(null, document.title, location.href);
    });
  }

  openModal()
  {
    const modelDiv=document.getElementById('myModal');
    if(modelDiv!=null)
    {
      modelDiv.style.display='block';
    }
  }
}
