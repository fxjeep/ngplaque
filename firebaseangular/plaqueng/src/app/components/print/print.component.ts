import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PlaqueService } from "../../service/firebaseService";

@Component({
  selector: 'app-print',
  templateUrl: './print.component.html',
  styleUrls: ['./print.component.css']
})
export class PrintComponent implements OnInit {

  printList: Observable<any>;

  constructor(public plaquesrv: PlaqueService) { }

  ngOnInit() {
    this.printList = this.plaquesrv.getPrintContacts();
  }
}
