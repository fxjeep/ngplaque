import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase, AngularFireList } from '@angular/fire/database';
import { database } from 'firebase/app';
import { Contact } from './models';
import { Observable } from 'rxjs';

const ContactCollection:string = "Contacts";
 
@Injectable({
  providedIn: 'root'
})
export class PlaqueService {
 
  authState: any = null;
  contactList: AngularFireList<Contact>;

  constructor(public afAuth: AngularFireAuth,
              public db: AngularFireDatabase) {
    this.afAuth.authState.subscribe((auth) => { this.authState = auth; });
    this.contactList = db.list(ContactCollection);
  }

  isLoggedIn(){
      return this.authState !== null;
  }
  
  login(email: string, password: string) {
    return this.afAuth.auth.signInWithEmailAndPassword(email, password);
  }
  
  async logout() {
    await this.afAuth.auth.signOut()
              .catch(function(error) { 
                alert(error); 
              });
  }

  createContact(name:string, code: string) : database.ThenableReference{
      let newContact = Contact.createNewContact(name, code);
      let thenable = this.contactList.push(newContact)
      return thenable;
  }
}