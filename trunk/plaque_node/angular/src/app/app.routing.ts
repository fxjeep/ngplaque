import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./components/login.component";
import { PanelComponent } from "./components/panel.component";


const routes: Routes = [
{ path: "", component: LoginComponent },
{ path: "panel", component: PanelComponent }]
export const routing = RouterModule.forRoot(routes);