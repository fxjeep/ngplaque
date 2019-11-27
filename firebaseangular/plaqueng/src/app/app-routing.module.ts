import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from './components/signin/signin.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { PrintComponent } from './components/print/print.component';
import { DataComponent } from './components/data/data.component';

const routes: Routes =  [
  { path: '', redirectTo: '/sign-in', pathMatch: 'full' },
  { path: 'sign-in', component: SigninComponent },
  { path: 'contacts', component: ContactsComponent },
  { path: 'print', component: PrintComponent},
  { path: 'data', component: DataComponent }
];;

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
