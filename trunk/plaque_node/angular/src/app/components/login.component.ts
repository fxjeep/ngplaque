import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {MatButtonModule} from '@angular/material';


@Component({
    selector: 'login',
    template: `
     <mat-card>
     <mat-card-title><h3>Login</h3></mat-card-title>
     <mat-card-content>
        <form class="example-container">
            <mat-form-field>
                    <input matInput floatPlaceholder="never" type="text"
                        placeholder="User Email" required>
            </mat-form-field>
            <mat-form-field>
                    <input matInput floatPlaceholder="never" type="password"
                        placeholder="Password" required>
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