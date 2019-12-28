import { Component, OnInit } from '@angular/core';
import { Contact, LiveColumns, DeadColumns, AncesterColumns, PlaqueType } from '../../service/models';

@Component({
  selector: 'app-detail-tab',
  templateUrl: './detail-tab.component.html',
  styleUrls: ['./detail-tab.component.css']
})
export class DetailTabComponent implements OnInit {

  model : any = {
    lives:[],
    deads:[],
    ancesters:[],
    liveColumns:LiveColumns,
    deadColumns:DeadColumns,
    ancesterColumns:AncesterColumns,
    Contact:{}
  }

  DetailType = PlaqueType

  constructor() { }

  ngOnInit() {
  }

  showDetails(contact:Contact){
    this.model.Contact = contact;
  }
}
