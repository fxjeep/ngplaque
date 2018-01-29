import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";

@Component({
    selector: 'login',
    template: '<h1>{{text}}</h1><input type="button" (click)="login()" value="login"/>'
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