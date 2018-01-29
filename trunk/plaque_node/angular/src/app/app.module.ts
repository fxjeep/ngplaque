import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { routing } from "./app.routing";
import { AppComponent } from './app.component';
import { LoginComponent } from "./components/login.component";
import { PanelComponent } from "./components/panel.component";


@NgModule({
    declarations: [AppComponent, LoginComponent, PanelComponent],
    imports: [BrowserModule, routing],
    bootstrap: [AppComponent]
})
export class AppModule { };