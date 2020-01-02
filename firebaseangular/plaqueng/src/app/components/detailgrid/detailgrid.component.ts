import { Component, OnInit, Input } from '@angular/core';
import { ColumnDefinition, PlaqueType } from '../../service/models';
import { PlaqueService } from '../../service/firebaseService';

@Component({
  selector: 'app-detailgrid',
  templateUrl: './detailgrid.component.html',
  styleUrls: ['./detailgrid.component.css']
})
export class DetailgridComponent implements OnInit {

  columns: ColumnDefinition[];
  data: [];
  @Input() type: PlaqueType;

  model = {
            data:[],
            newItem:{
                "Error":{}
            }}

  constructor(public plaquesrv: PlaqueService) { 

  }

  ngOnInit() {
    this.columns = this.plaquesrv.getColumnDefinition(this.type);
    this.model.data = this.plaquesrv.getData(this.type);
  }

  addNewItem(newItem){
    //alert(JSON.stringify(data));
    let data : any;
    data = Object.assign({}, newItem);
    this.plaquesrv.addDetail(this.type, data);
  }

}
