import { Component, OnInit } from '@angular/core';
import {FormsModule} from "@angular/forms";
import { LoginService } from "../../service/firebaseService";

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  model : any = {username : "", password : ""}

  constructor(public auth: LoginService) { 

  }

  ngOnInit() {
  }

  onSubmitLogin(){
      alert("ddd");
      this.auth.login(this.model.username, this.model.password);
  }
}
