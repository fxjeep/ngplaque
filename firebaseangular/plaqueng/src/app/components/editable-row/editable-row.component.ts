import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ColumnDefinition } from '../../service/models';

@Component({
  selector: '[editable-row]',
  templateUrl: './editable-row.component.html',
  styleUrls: ['./editable-row.component.css']
})
export class EdtiableRowComponent implements OnInit {

  @Input() columns: ColumnDefinition[];
  @Input() data: any;
  @Input() isEdit: boolean;
  @Input() isNew: boolean;

  @Output() saveData = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onKeydown(event) {
    var self =this;
    var allcorrect = true;
    if (event.key === "Enter") {
      this.columns.forEach((item,index)=>{
          if (item.IsEditable){
            if (!self.data[item.PropertyName]){
              allcorrect = false;
            }
          }
      });

      if (allcorrect){
        this.saveData.emit(this.data);
      }
    }
  }
}
