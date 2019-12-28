import { Component, OnInit, Input  } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PlaqueService } from "../../service/firebaseService";
import {DetailTabComponent} from "../detail-tab/detail-tab.component";
import { Observable } from 'rxjs';
import {Contact} from '../../service/models';
import {DialogService} from '../../dialogService/dialog.service';
import {MessageboxComponent} from '../messagebox/messagebox.component';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {
  
  @Input() editDetail: DetailTabComponent;

  model:any={
    showAdd:false,
    newName:'',
    newCode:'',
    searchText:'',
    contacts: [],
    selectedContact: null,
    error: ""
  }

  constructor(public plaquesrv: PlaqueService, public dialogSvr: DialogService) { 
    
  }

  ngOnInit() {
    this.model.contacts = this.plaquesrv.contactCollection.valueChanges({idField:'ContactId'});
  }

  closeAdd(){
    this.model.showAdd = false;
  }

  showAdd(form: NgForm){
    this.model.showAdd = true;
    form.resetForm({});
  }

  clearError(){
    this.model.error = "";
  }

  addContact(isValid){
    if (!isValid) return;
    
    let self = this;
    this.clearError();
    this.plaquesrv.createContact(this.model.newName, this.model.newCode) 
                  .then(function(result){
                    self.model.showAdd = false;
                  })
                  .catch(function(error){
                    self.model.showAdd = false;
                    self.model.error = error.toString();
                  });
    return true;
  }

   setClickedRow(contact){
    this.model.selectedContact = contact;
    this.editDetail.showDetails(contact);
  }

  delete(contact:Contact){
    let self = this;
    const ref = this.dialogSvr.open(MessageboxComponent, 
      { data: { message: 'Delete contact '+contact.Name+' : '+contact.Code+' ?',
                title: 'Confirm Delete Contact',
                okCallback: function(){
                      ref.close();
                      self.plaquesrv.deleteContact(contact)
                          .then(function(result){
                            alert("Contact " + contact.Name + " is deleted");
                          })
                          .catch(function(error){
                            self.model.error = "Failed to delete contact " + contact.Name;
                          });
                },
                cancelCallback:function(){
                  ref.close();
                }
      } });
  }

  print(contact:Contact){
    contact.IsPrint = true;
    this.plaquesrv.updateContact(contact)
      .then(result=>{
        alert("Contact is set to print");
      })
      .catch(error=>{
        this.model.error = "Failed to set contact " + contact.Name + " to print";
      });
  }

  saveList(contact:Contact){
    alert("save list contact");
  }
}
