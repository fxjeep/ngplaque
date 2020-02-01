import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
 
import { AngularFireModule } from '@angular/fire';
import { AngularFireDatabaseModule } from '@angular/fire/database';
import { environment } from '../environments/environment';
import { AngularFirestoreModule } from '@angular/fire/firestore';
import { AngularFireAuthModule } from '@angular/fire/auth';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SigninComponent } from './components/signin/signin.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import {FormsModule} from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { registerLocaleData } from '@angular/common';
import { AngularFireAuthGuard } from '@angular/fire/auth-guard';
import { LoadingBarModule } from '@ngx-loading-bar/core';
import { LoadingBarHttpClientModule } from '@ngx-loading-bar/http-client';
import en from '@angular/common/locales/en';


import { PlaqueService } from './service/firebaseService';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ContactsComponent } from './components/contacts/contacts.component';
import { PrintComponent } from './components/print/print.component';
import { DataComponent } from './components/data/data.component';
import { DetailTabComponent } from './components/detail-tab/detail-tab.component';
import { EditComponent } from './components/edit/edit.component';
import { ContactFilterPipe } from './service/contactfilter.pipe';
import { DetailFilterPipe } from './service/detailfilter.pipe';
import { MessageboxComponent } from './components/messagebox/messagebox.component';
import { DialogModule } from './dialogService/dialog.module';
import { DetailgridComponent } from './components/detailgrid/detailgrid.component';
import { EdtiableRowComponent } from './components/editable-row/editable-row.component';

registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    SigninComponent,
    DashboardComponent,
    HeaderComponent,
    FooterComponent,
    ContactsComponent,
    PrintComponent,
    DataComponent,
    DetailTabComponent,
    EditComponent,
    ContactFilterPipe,
    DetailFilterPipe,
    MessageboxComponent,
    DetailgridComponent,
    EdtiableRowComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebaseConfig),
    AngularFirestoreModule,
    AngularFireDatabaseModule,
    AngularFireAuthModule,
    HttpClientModule,
    BrowserAnimationsModule,
    LoadingBarModule,
    LoadingBarHttpClientModule,
    DialogModule
  ],
  providers: [PlaqueService, AngularFireAuthGuard],
  bootstrap: [AppComponent],
  entryComponents: [
    MessageboxComponent
  ]
})
export class AppModule { }
