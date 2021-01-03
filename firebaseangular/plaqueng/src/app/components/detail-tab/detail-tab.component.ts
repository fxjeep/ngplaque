import { Component, OnInit, Input } from '@angular/core';
import { Contact, Live, Dead, Ancestor, PlaqueType } from '../../service/models';
import { PlaqueService } from '../../service/firebaseService';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-detail-tab',
  templateUrl: './detail-tab.component.html',
  styleUrls: ['./detail-tab.component.css']
})
export class DetailTabComponent implements OnInit {

  @Input() Contact:any = {}
  // Live : Observable<Live[]>;
  // Dead :　Observable<Dead[]>;
  // Ancestor :　Observable<Ancestor[]>;
  tab:string;
  
  DetailType = PlaqueType

  constructor(public plaquesrv: PlaqueService) { 
  }

  ngOnInit() {
  }

  showDetails(contact:Contact){
    this.Contact = contact;
    this.plaquesrv.updateContact(contact);
    // this.Live = this.plaquesrv.liveData;
    // this.Dead = this.plaquesrv.deadData;
    // this.Ancestor = this.plaquesrv.ancestorData;
  }
}
