import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PlaqueType, PrintedEnum } from 'src/app/service/models';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { PlaqueService } from "../../service/firebaseService";
import {Contact, PlaqueRecordType} from '../../service/models';
import { pluck } from 'rxjs/operators';
import { Utils } from 'src/app/service/Utils';

const delimiter = "\t";

@Component({
  selector: 'app-print',
  templateUrl: './print.component.html',
  styleUrls: ['./print.component.css']
})
export class PrintComponent implements OnInit {

  printList: Contact[];
  downloadLink: string;
  showDownload : boolean;
  downloadFileName: string;

  constructor(public plaquesrv: PlaqueService) { }

  ngOnInit() {
    this.getPrints();
  }

  getPrints(){
    let self = this;
    this.plaquesrv.getPrintContacts().subscribe(list=> self.printList = list);
  }

  generatePrintData(){
     let txt = [];
     if (this.printList){
        let self = this;
        this.printList.forEach(function(contact){
            let lineArray = [PlaqueRecordType.contact, contact.Name, contact.Code, contact.LastPrint, contact.SinglePrint];
            txt.push(lineArray.join(delimiter));

            if (contact.IsPrinted == PrintedEnum.All){
              //get all data and save to text
              self.plaquesrv.updateDetail(contact);
              let liveData = self.plaquesrv.getData(PlaqueType.live);
              liveData.subscribe(liveList=>{
                liveList.forEach(liveRec => {
                  let lineArray = [PlaqueRecordType.live, liveRec.Name, contact.Code];
                  txt.push(lineArray.join(delimiter));
                });                
              });

              let deadData = self.plaquesrv.getData(PlaqueType.dead);
              deadData.subscribe(deadList=>{
                deadList.forEach(deadRec => {
                  let lineArray = [PlaqueRecordType.dead, deadRec.DeadName, deadRec.LiveName, deadRec.Relation, contact.Code];
                  txt.push(lineArray.join(delimiter));
                });                
              });

              let ancesterData = self.plaquesrv.getData(PlaqueType.ancestor);
              ancesterData.subscribe(ancesterList=>{
                ancesterList.forEach(ancesterRec => {
                  let lineArray = [PlaqueRecordType.ancester, ancesterRec.Surname, ancesterRec.LivveName, contact.Code];
                  txt.push(lineArray.join(delimiter));
                });                
              });
            }
        });

        let blob = new Blob(txt, {type: "text/plain;charset=utf-8"});
        let url = window.URL.createObjectURL(blob);
        this.downloadLink = url;
        this.downloadFileName = "PrintDate_"+Utils.GetDownloadFileName();
        this.showDownload = true;
     }
  }
}
