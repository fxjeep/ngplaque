import { Component, OnInit } from '@angular/core';
import { LoginService } from "../../service/firebaseService";
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

  constructor(public auth: LoginService, public  router:  Router) { 

  }

  ngOnInit() {
  }

  onSubmitLogin(){
      this.model.error = "";
      this.auth.login(this.model.username, this.model.password)
          .then(() => {
            this.router.navigate(['edit']);
          })
          .catch((error) => {
            this.model.error = error;
          });
  }
}
