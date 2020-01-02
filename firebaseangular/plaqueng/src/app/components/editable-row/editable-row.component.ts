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
      for(var i=0; i<this.columns.length; i++){
        let item = this.columns[i];
        if (item.IsEditable){
          if (!self.data[item.PropertyName]){
            self.data["Error"][item.PropertyName] = "please enter " + item.Name;
            allcorrect = false;
          }
          else{
            self.data["Error"][item.PropertyName] = "";
          }
        }
      };

      if (allcorrect){
        this.saveData.emit(this.data);
      }
    }
  }
}
