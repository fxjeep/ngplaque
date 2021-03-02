import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/auth';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { Contact, Live, Dead, Ancestor, PlaqueType, ColumnDefinition, getColumnDefinition, PrintedEnum } from './models';
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

  findContactByCode(code:string):Observable<Contact>{
    let contact = this.db.collection<Contact>('Contacts', ref=>ref.where("Code", "==", code)).valueChanges();
    return contact.pipe(map(c=>c.length>0?c[0]:null));
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

  

  getPartialPrintData(type:PlaqueType, contactId:string) : any{
    if (type == PlaqueType.live){
      return this.db.collection<Live>('Live', ref=>ref.where("ContactId", "==", contactId)
                                                      .where("IsPrinted", "==", true)).valueChanges();
    }
    else if (type == PlaqueType.dead){
      return this.db.collection<Dead>('Dead', ref=>ref.where("ContactId", "==", contactId)
                                                      .where("IsPrinted", "==", true)).valueChanges();
    }
    else if (type == PlaqueType.ancestor){
      return this.db.collection<Ancestor>('Ancestor', ref=>ref.where("ContactId", "==", contactId)
                                                      .where("IsPrinted", "==", true)).valueChanges();
    }
  }

  getData(type: PlaqueType):any{
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

  saveDetail(type:PlaqueType, data:any){
    if (type == PlaqueType.live){
      let itemDoc = this.db.doc<Live>('Live/'+data.LiveId);
      itemDoc.update(data);
    }
    else if (type == PlaqueType.dead){
      let itemDoc = this.db.doc<Dead>('Dead/'+data.DeadId);
      itemDoc.update(data);
    }
    else if (type == PlaqueType.ancestor){
      let itemDoc = this.db.doc<Dead>('Ancestor/'+data.AncestorId);
      itemDoc.update(data);
    }
  }

  
  updateCount(contact:Contact, detail:any, type:PlaqueType){
    if (!contact.SinglePrint) contact.SinglePrint="";
    let counts = [0,0,0];
    let split = contact.SinglePrint.split(",");
    if (split.length == 3 ){
      counts[0] = Number(split[0]);
      counts[1] = Number(split[1]);
      counts[2] = Number(split[2]);
    }
    if (type == PlaqueType.live){
       if (detail.IsPrinted) counts[0]++;
       else counts[0]--;
       if (counts[0]<0) counts[0]=0;
    }
    else if (type == PlaqueType.dead){
      if (detail.IsPrinted) counts[1]++;
      else counts[1]--;
      if (counts[1]<0) counts[1]=0;
    }
    else if (type == PlaqueType.ancestor){
      if (detail.IsPrinted) counts[2]++;
      else counts[2]--;
      if (counts[2]<0) counts[2]=0;
    }
    contact.SinglePrint = counts.join(",");
    contact.IsPrinted = PrintedEnum.Partial;
    this.updateContact(contact);
  }

  deleteDetail(type:PlaqueType, data:any){
    if (type == PlaqueType.live){
      let itemDoc = this.db.doc<Live>('Live/'+data.LiveId);
      itemDoc.delete();
    }
    else if (type == PlaqueType.dead){
      let itemDoc = this.db.doc<Dead>('Dead/'+data.DeadId);
      itemDoc.delete();
    }
    else if (type == PlaqueType.ancestor){
      let itemDoc = this.db.doc<Dead>('Ancestor/'+data.AncestorId);
      itemDoc.delete();
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

  getMainName(data: any){
    if (data.LiveName) return data.LiveName;
    else if (data.DeadName) return data.DeadName;
    else if (data.Surname) return data.Surname;
  }

  getPrintContacts(){
    let prnContacts = this.db.collection<Contact>('Contacts', ref=>ref.where('IsPrinted', '>', 0)).valueChanges();
    return prnContacts;
  }
}


