import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { routing } from "./app.routing";
import { AppComponent } from './app.component';
import { LoginComponent } from "./components/login.component";
import { PanelComponent } from "./components/panel.component";
import { MatButtonModule } from '@angular/material';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
    declarations: [AppComponent, LoginComponent, PanelComponent],
    imports: [BrowserModule, routing, BrowserAnimationsModule,
        MatButtonModule, MatCardModule, MatFormFieldModule, MatInputModule],
    bootstrap: [AppComponent]
})
export class AppModule { };