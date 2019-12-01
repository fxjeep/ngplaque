import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFireDatabase } from '@angular/fire/database';
import { Contact } from './models';

const ContactCollection:string = "Contacts";
 
@Injectable({
  providedIn: 'root'
})
export class PlaqueService {
 
  authState: any = null;
  contactList: any;

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

  createContact(name:string, code: string){
      let newContact = Contact.createNewContact(name, code);
      this.contactList.push(newContact);
  }
}