import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { Contact, Live, Dead, Ancestor, PlaqueType, ColumnDefinition, getColumnDefinition } from './models';
import { Observable } from 'rxjs';
import {  map} from "rxjs/operators";

const ContactCollection:string = "Contacts";
 
@Injectable({
  providedIn: 'root'
})
export class PlaqueService {
 
  authState: any = null;
  contactCollection: AngularFirestoreCollection<Contact>;
  liveCollection : AngularFirestoreCollection<Live>;
  deadCollection : AngularFirestoreCollection<Dead>;
  ancestorCollection : AngularFirestoreCollection<Ancestor>;

  liveData: Observable<Live[]>;
  deadData: Observable<Dead[]>;
  ancestorData: Observable<Ancestor[]>;

  constructor(public afAuth: AngularFireAuth,
              public db: AngularFirestore) {
    this.afAuth.authState.subscribe((auth) => { this.authState = auth; });
    this.contactCollection = db.collection<Contact>('Contacts');
    
  }

  isLoggedIn(){
      return this.authState !== null;
  }
  
  login(email: string, password: string) {
    this.afAuth.auth.setPersistence('none');
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
      try {
        let data : any;
        data = Object.assign({}, newContact);
        return this.contactCollection.add(data);
      }
      catch(ex) {
        return Promise.reject(ex);
      }
  }

  deleteContact(contact:Contact){
     return this.contactCollection.doc(contact.ContactId).delete();
  }

  updateContact(contact:Contact){
    return this.contactCollection.doc(contact.ContactId).set(contact);
  }

  getColumnDefinition(type:PlaqueType) : ColumnDefinition[]{
      return getColumnDefinition(type);
  }

  updateDetail(contact:Contact){
      this.liveCollection = this.db.collection<Live>('Live', ref=>ref.where("ContactId", "==", contact.ContactId));
      this.liveData = this.liveCollection.valueChanges({idField:'LiveId'});

      this.deadCollection = this.db.collection<Dead>('Dead', ref=>ref.where("ContactId", "==", contact.ContactId));    
      this.deadData = this.deadCollection.valueChanges({idField:'DeadId'});

      this.ancestorCollection = this.db.collection<Ancestor>('Ancestor', ref=>ref.where("ContactId", "==", contact.ContactId));
      this.ancestorData = this.ancestorCollection.valueChanges({idField:'AncestorId'});
  }

  getData(type:PlaqueType, contactId:string) : any{
    if (type == PlaqueType.live){
      return this.liveData;
    }
    else if (type == PlaqueType.dead){
      return this.deadData;
    }
    else if (type == PlaqueType.ancestor){
      return this.ancestorData;
    }
  }

  addDetail(type:PlaqueType, data:any){
    if (type == PlaqueType.live){
      this.liveCollection.add(data);
    }
    else if (type == PlaqueType.dead){
      this.deadCollection.add(data);
    }
    else if (type == PlaqueType.ancestor){
      this.ancestorCollection.add(data);
    }
  }
}
