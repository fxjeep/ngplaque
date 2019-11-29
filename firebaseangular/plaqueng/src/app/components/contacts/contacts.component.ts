import { Component, OnInit, ViewChild  } from '@angular/core';
import { NgForm } from '@angular/forms';


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

  constructor() { }

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
  }
}
