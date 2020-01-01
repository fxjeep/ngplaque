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
  data: any[];
  @Input() type: PlaqueType;

  model = {
              newItem:{  }
          };

  constructor(public plaquesrv: PlaqueService) { 

  }

  ngOnInit() {
    this.columns = this.plaquesrv.getColumnDefinition(this.type);
    this.data = this.plaquesrv.getData(this.type);
  }

  addNewItem(data){
    alert(JSON.stringify(data));
  }

}
