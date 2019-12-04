import { Component, OnInit, ViewChild  } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PlaqueService } from "../../service/firebaseService";
import { Observable } from 'rxjs';


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {
  
  model:any={
    showAdd:false,
    newName:'',
    newCode:'',
    searchText:'',
    contacts: [],
    selectedRow:-1
  }

  constructor(public plaquesrv: PlaqueService) { 
    
  }

  ngOnInit() {
    this.model.contacts = this.plaquesrv.contactList.valueChanges();
  }

  closeAdd(){
    this.model.showAdd = false;
  }

  showAdd(form: NgForm){
    this.model.showAdd = true;
    form.resetForm({});
  }

  addContact(){
    var thenable = this.plaquesrv.createContact(this.model.newName, this.model.newCode);
    let self = this;
    thenable.then(function(result){
      self.model.showAdd = false;
    }, function(error){
      self.model.showAdd = false;
    });
  }

   search(){
    this.model.contacts = this.plaquesrv.contactList.valueChanges();
   }

   setClickedRow(index){
    this.model.selectedRow = index;
  }
}
