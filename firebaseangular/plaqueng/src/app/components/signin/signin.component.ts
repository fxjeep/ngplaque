import { Component, OnInit } from '@angular/core';
import { PlaqueService } from "../../service/firebaseService";
import { LoadingBarService } from '@ngx-loading-bar/core';
import { Router } from  "@angular/router";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  model : any = {username : "", 
                  password : "",
                 error : ""}

  constructor(public plaquesrv: PlaqueService,
              public  router:  Router, 
              public loadingBar: LoadingBarService) { 

  }

  ngOnInit() {
  }

  onSubmitLogin(){
      this.model.error = "";
      this.loadingBar.start();
      this.plaquesrv.login(this.model.username, this.model.password)
          .then(() => {
            this.router.navigate(['edit']);
          })
          .catch((error) => {
            this.model.error = error;
          })
          .finally(()=>{
            this.loadingBar.complete();
          });
  }
}
