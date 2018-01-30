import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { routing } from "./app.routing";
import { AppComponent } from './app.component';
import { LoginComponent } from "./components/login.component";
import { PanelComponent } from "./components/panel.component";
import { MatButtonModule } from '@angular/material';

@NgModule({
    declarations: [AppComponent, LoginComponent, PanelComponent],
    imports: [BrowserModule, routing, MatButtonModule],
    bootstrap: [AppComponent]
})
export class AppModule { };