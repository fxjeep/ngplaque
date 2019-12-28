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
    if (event.key === "Enter") {
      this.saveData.emit(this.data);
    }
  }
}
