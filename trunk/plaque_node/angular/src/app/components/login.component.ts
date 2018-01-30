import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import {MatButtonModule} from '@angular/material';


@Component({
    selector: 'login',
    template: `
     <div>
        <form class="example-container">
            <mat-form-field>
                    <input matInput floatPlaceholder="never"
                        placeholder="User Email" required>
            </mat-form-field>
            <mat-form-field>
                    <input matInput floatPlaceholder="never"
                        placeholder="Password" required>
            </mat-form-field>
        </form>
    </div>
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