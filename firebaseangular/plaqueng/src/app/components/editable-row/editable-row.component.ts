import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ColumnDefinition, PrintedEnum } from '../../service/models';
import {DialogService} from '../../dialogService/dialog.service';
import {MessageboxComponent} from '../messagebox/messagebox.component';
import { PlaqueService } from "../../service/firebaseService";

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
  @Input() isContactInPrint: PrintedEnum;

  @Output() saveData = new EventEmitter();
  @Output() editData = new EventEmitter();
  @Output() deleteData = new EventEmitter();

  printedEnumCopy = PrintedEnum;

  constructor(public plaquesrv: PlaqueService, public dialogSvr: DialogService) { }

  ngOnInit() {
  }

  onKeydown(event) {
    var self =this;
    var allcorrect = true;
    if (event.key === "Enter") {
      this.save();
    }
  }

  showEdit(){
    this.isEdit = true;
  }

  saveEdit(){
    this.editData.emit(this.data);
    this.isEdit = false;
  }

  showDeleteConfirm(){
    let self = this;
    const ref = this.dialogSvr.open(MessageboxComponent, 
      { data: { message: 'Delete '+ this.plaquesrv.getMainName(this.data) +' ?',
                title: 'Confirm Delete',
                okCallback: function(){
                      ref.close();
                      self.deleteData.emit(self.data);
                },
                cancelCallback:function(){
                  ref.close();
                }
      } });
  }

  save(){
    var self =this;
    var allcorrect = true;
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

  togglePrint(){
    this.data.IsPrinted = !this.data.IsPrinted;
    this.editData.emit(this.data);
  }
}
