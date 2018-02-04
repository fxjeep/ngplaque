import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {MatButtonModule} from '@angular/material';
import { CommonService } from "./common.service";

@Component({
    selector: 'login',
    template: `
     <mat-card>
     <mat-card-title><h3>Login</h3></mat-card-title>
     <mat-card-content>
        <form class="example-container">
            <mat-form-field>
                    <input matInput floatPlaceholder="never" type="text" placeholder="User Email" required
                    name="email" 
                    [(ngModel)]="email"/>
            </mat-form-field>
            <mat-form-field>
                    <input matInput floatPlaceholder="never" type="password" placeholder="Password" required
                    name="password" 
                        [(ngModel)]="password"/>
            </mat-form-field>
        </form>
    </mat-card-content>
    <mat-card-actions>
        <button mat-raised-button (click)="login()">Login</button>
    </mat-card-actions>
    </mat-card>
  `
})
export class LoginComponent implements OnInit {

    email: string = "111";
    password: string = "22";

    constructor(private activeRoute: ActivatedRoute,
        private router: Router,
        private commonSvc: CommonService) {
    }

    ngOnInit(): void {
    }

    login():void{
        alert(this.email);
        alert(this.password);
        this.router.navigateByUrl("/panel");
    }
};