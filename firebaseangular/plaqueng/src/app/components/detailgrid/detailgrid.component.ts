import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ColumnDefinition, PlaqueType, Contact } from '../../service/models';
import { PlaqueService } from '../../service/firebaseService';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-detailgrid',
  templateUrl: './detailgrid.component.html',
  styleUrls: ['./detailgrid.component.css']
})
export class DetailgridComponent implements OnInit, OnChanges {

  columns: ColumnDefinition[];
  @Input() type: PlaqueType;
  @Input() contact: Contact;
  @Input() data: Observable<[]>;

  model: any = {
            searchText:'',
            newItem:{
                "Error":{}
            }}

  constructor(public plaquesrv: PlaqueService) { 

  }

  ngOnInit() {
    this.columns = this.plaquesrv.getColumnDefinition(this.type);    
  }

  ngOnChanges(changes: SimpleChanges) {
    let i=0;
    this.data = this.plaquesrv.getData(this.type);
  }

  addNewItem(newItem){
    //alert(JSON.stringify(data));
    let data : any;
    newItem.ContactId = this.contact.ContactId;
    let today = new Date();
    newItem.Added = today.getTime();
    data = Object.assign({}, newItem);
    this.plaquesrv.addDetail(this.type, data);
    this.model.newItem = { "Error":{} };
  }

  setEdit(editItem){
    this.plaquesrv.saveDetail(this.type, editItem);
    this.plaquesrv.updateCount(this.contact, editItem, this.type);
  }

  deleteData(delItem){
    this.plaquesrv.deleteDetail(this.type, delItem);
  }
}
