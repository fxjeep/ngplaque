import { Component, OnInit } from '@angular/core';
import { LoginService } from "../../service/firebaseService";
import { Router } from  "@angular/router";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(public auth: LoginService, public  router:  Router) { 

  }

  ngOnInit() {
  }

  isLoggedIn(){
     return this.auth.isLoggedIn();
  }

  logout(){
    this.auth.logout();
    this.router.navigate(['sign-in']);
  }
}
