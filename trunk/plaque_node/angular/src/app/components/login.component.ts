import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {MatButtonModule} from '@angular/material';

@Component({
    selector: 'login',
    template: '<h1>{{text}}</h1><button mat-raised-button (click)="login()" value="login">Login</button>'
})
export class LoginComponent implements OnInit {

    text: string;

    constructor(activeRoute: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit(): void {
        this.text  = "Login";
    }

    login():void{
        this.router.navigateByUrl("/panel");
    }
};