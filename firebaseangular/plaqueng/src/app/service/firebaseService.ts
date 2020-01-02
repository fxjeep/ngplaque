import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { Contact, Live, Dead, Ancestor, PlaqueType, ColumnDefinition, getColumnDefinition } from './models';
import { Observable } from 'rxjs';

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

  constructor(public afAuth: AngularFireAuth,
              public db: AngularFirestore) {
    this.afAuth.authState.subscribe((auth) => { this.authState = auth; });
    this.contactCollection = db.collection<Contact>('Contacts');
    this.liveCollection = db.collection<Live>('Live');
    this.deadCollection = db.collection<Dead>('Dead');
    this.ancestorCollection = db.collection<Ancestor>('Ancestor');
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

  getData(type:PlaqueType) : any{
    if (type == PlaqueType.live){
      return this.liveCollection.valueChanges({idField:'LiveId'});
    }
    else if (type == PlaqueType.dead){
      return this.deadCollection.valueChanges({idField:'DeadId'});
    }
    else if (type == PlaqueType.ancestor){
      return this.ancestorCollection.valueChanges({idField:'AncestorId'});
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
