import { Component, OnInit, ViewChild  } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PlaqueService } from "../../service/firebaseService";


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {
  
  model:any={
    showAdd:false,
    newName:'',
    newCode:''
  }

  constructor(public plaquesrv: PlaqueService) { 

  }

  ngOnInit() {
  }

  closeAdd(){
    this.model.showAdd = false;
  }

  showAdd(form: NgForm){
    this.model.showAdd = true;
    form.resetForm({});
  }

  addContact(){
    this.plaquesrv.createContact(this.model.newName, this.model.newCode);
    this.model.showAdd = false;
  }
}
