import { Component, Type, ComponentFactoryResolver, ViewChild, OnDestroy, ComponentRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { DialogConfig } from '../../dialogService/dialog-config';
import { DialogRef } from '../../dialogService/dialog-ref';


@Component({
  selector: 'app-messagebox',
  templateUrl: './messagebox.component.html',
  styleUrls: ['./messagebox.component.css']
})
export class MessageboxComponent {
  constructor(public config: DialogConfig, public dialog: DialogRef) {}
  
  onClose() {
    this.dialog.close('some value');
  }
}
