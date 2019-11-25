import { Component, OnInit } from '@angular/core';
import {FormsModule} from "@angular/forms";


@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})
export class SigninComponent implements OnInit {
  model : any = {username : "", password : ""}

  constructor() { }

  ngOnInit() {
  }

  onSubmitLogin(){
      alert("ddd");
  }
}
