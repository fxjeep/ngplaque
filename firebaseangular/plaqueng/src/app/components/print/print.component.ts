import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PlaqueType, PrintedEnum } from 'src/app/service/models';
import { AngularFirestore, AngularFirestoreCollection } from '@angular/fire/firestore';
import { PlaqueService } from "../../service/firebaseService";
import {Contact, PlaqueRecordType} from '../../service/models';
import { pluck } from 'rxjs/operators';
import { Utils } from 'src/app/service/Utils';
import { DomSanitizer } from '@angular/platform-browser';
import { resolve } from 'url';

const delimiter = "\t";

@Component({
  selector: 'app-print',
  templateUrl: './print.component.html',
  styleUrls: ['./print.component.css']
})
export class PrintComponent implements OnInit {

  printList: Contact[];
  downloadLink: any;
  showDownload : boolean;
  downloadFileName: string;

  constructor(public plaquesrv: PlaqueService,  private sanitizer: DomSanitizer) { 
  }

  ngOnInit() {
    this.getPrints();
  }

  getPrints(){
    let self = this;
    this.plaquesrv.getPrintContacts().subscribe(list=> self.printList = list);
  }

  async generatePrintData(){
     let txt = [];
     if (this.printList){
        let self = this;
        this.printList.forEach(async function(contact){
            let lineArray = [PlaqueRecordType.contact, contact.Name, contact.Code, contact.LastPrint, contact.SinglePrint];
            txt.push(lineArray.join(delimiter));

            if (contact.IsPrinted == PrintedEnum.All){
              //get all data and save to text
              self.plaquesrv.updateDetail(contact);

              let liveDataPromise = self.getPromise(PlaqueType.live, txt, contact.ContactId, contact.Code);
              await liveDataPromise;

              let deadDataPromise = self.getPromise(PlaqueType.dead, txt, contact.ContactId, contact.Code);
              await deadDataPromise;

              let ancesterDataPromise = self.getPromise(PlaqueType.ancestor, txt, contact.ContactId, contact.Code);
              await ancesterDataPromise;

              // Promise.all([
              //   liveDataPromise,
              //   deadDataPromise,
              //   ancesterDataPromise
              // ]).then((result)=>{
                  
              self.createDownloadFile(txt);
              //});
            }
            else if (contact.IsPrinted == PrintedEnum.Partial) {
              let liveDataPromise = self.getPartialPrintPromise(PlaqueType.live, txt, contact.ContactId, contact.Code);
              await liveDataPromise;

              let deadDataPromise = self.getPartialPrintPromise(PlaqueType.dead, txt, contact.ContactId, contact.Code);
              await deadDataPromise;

              let ancesterDataPromise = self.getPartialPrintPromise(PlaqueType.ancestor, txt, contact.ContactId, contact.Code);
              await ancesterDataPromise;
            }
        });
     }
  }

  createDownloadFile(txt:any[]){
    let txtstr = txt.join("\r\n");
    let blob = new Blob([txtstr], {type: "data:attachment/text"});
    let url = window.URL.createObjectURL(blob);
    this.downloadLink = this.sanitizer.bypassSecurityTrustUrl(url);
    this.downloadFileName = "PrintDate_"+Utils.GetDownloadFileName()+".txt";
    this.showDownload = true;
  }

  getPromise(type: PlaqueType, txt:string[], contactId:string, contactCode:string){
    let self= this;
    return new Promise((resolve, reject)=>{
      let data = self.plaquesrv.getData(type);
      data.subscribe(list=>{
        list.forEach(rec => {
          let lineArray = this.getLineArray(type, contactId, contactCode, rec);          
          let line = lineArray.join(delimiter);
          txt.push("f"+line);
        });                
        resolve(true);
      });
    });
  }

  getPartialPrintPromise(type: PlaqueType, txt:string[], contactId:string, contactCode:string) {
    let self= this;
    return new Promise((resolve, reject)=>{
      let data = self.plaquesrv.getPartialPrintData(type, contactId);
      data.subscribe(list=>{
        list.forEach(rec => {
          let lineArray = this.getLineArray(type, contactId, contactCode, rec);
          let line = lineArray.join(delimiter);
          txt.push("p"+line);
        });                
        resolve(true);
      });
    });
  }

  getLineArray(type: PlaqueType,contactId:string, contactCode:string, rec: any) : string[]{
    let lineArray = [];
    if (type == PlaqueType.live){
        lineArray = [type, contactCode, contactId, rec.LiveName];
    }
    else if (type == PlaqueType.dead){
      lineArray = [PlaqueRecordType.dead, contactCode, contactId, rec.DeadName, rec.LiveName, rec.Relation];
    }
    else if (type == PlaqueType.ancestor){
        lineArray = [PlaqueRecordType.ancester, contactCode, contactId, rec.Surname, rec.LiveName];
    }
    return lineArray;
  }

  clearPrintData() {

  }
}
