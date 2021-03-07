import { Component, OnInit } from '@angular/core';
import { Live, PlaqueType, Dead, Ancestor, Contact, PlaqueRecordType } from 'src/app/service/models';
import { PlaqueService } from "../../service/firebaseService";

@Component({
  selector: 'app-data',
  templateUrl: './data.component.html',
  styleUrls: ['./data.component.css']
})
export class DataComponent implements OnInit {

  importLines : string;
  importLog: string;
  constructor(public plaquesrv: PlaqueService) { }

  ngOnInit() {
  }

  import(){
      if (!this.importLines) return;
      let self= this;
      this.importLog = "";
      let lines = this.importLines.split("\n");
      let lineCount = 0;
      lines.forEach(function(line){
          lineCount++;
          if (!line) return;
          var fields = line.split(",");
          if (fields.length>0){
            if (fields[0].trim().toLowerCase() == PlaqueRecordType.live){
              self.importLive(fields, lineCount);
            }
            else if (fields[0].trim().toLowerCase() == PlaqueRecordType.dead){
              self.importDead(fields, lineCount);
            }
            else if (fields[0].trim().toLowerCase() == PlaqueRecordType.ancester){
              self.importAncestor(fields, lineCount);
            }
            else if (fields[0].trim().toLowerCase() == PlaqueRecordType.contact){
              self.importContact(fields, lineCount);
            }
          }
      });
  }

  importLive(fields:string[], lineCount:number){
    if (fields.length <3 || !fields[1] || !fields[2]){
      this.addLog("line:"+lineCount+": live needs a name and contact code");
      return;
   }
   let self = this;
   let observContact = this.plaquesrv.findContactByCode(fields[1]);
   
   observContact.subscribe(contact=>{
     if (!contact) return;
      self.plaquesrv.updateDetail(contact);
      let live = new Live();
      live.LiveName = fields[2];
      live.ContactId = contact.ContactId;
      live.IsPrinted = false;
      live.LastPrint = "";
      self.plaquesrv.addDetail(PlaqueType.live, {...live});
      self.addLog("live record "+fields[2]+" added");
   });
  }

  importDead(fields:string[], lineCount:number){
    if (fields.length <5 || !fields[1] || !fields[2] || !fields[3] || !fields[4]){
      this.addLog("line:"+lineCount+": dead needs livename, deadname, relation and contact code");
      return;
   }
   let self = this;
   let observContact = this.plaquesrv.findContactByCode(fields[1]);
   observContact.subscribe(contact=>{
    if (!contact) return;
    self.plaquesrv.updateDetail(contact);
      let dead = new Dead();
      dead.DeadName = fields[2];
      dead.LiveName = fields[3];
      dead.Relation = fields[4];
      dead.ContactId = contact.ContactId;
      dead.IsPrinted = false;
      dead.LastPrint = "";
      self.plaquesrv.addDetail(PlaqueType.dead, {...dead});
      self.addLog("dead record "+fields[2]+" added");
   });
  }

  importAncestor(fields:string[], lineCount:number){
    if (fields.length <4 || !fields[1] || !fields[2] || !fields[3] ){
      this.addLog("line:"+lineCount+": ancester needs surname, livename, and contact code");
      return;
   }
   let self = this;
   let observContact = this.plaquesrv.findContactByCode(fields[1]);
   observContact.subscribe(contact=>{
    if (!contact) return;
    self.plaquesrv.updateDetail(contact);
      let anc = new Ancestor();
      anc.Surname = fields[2];
      anc.LiveName = fields[3];
      anc.ContactId = contact.ContactId;
      anc.IsPrinted = false;
      anc.LastPrint = "";
      self.plaquesrv.addDetail(PlaqueType.ancestor, {...anc});
      self.addLog("ancestor record "+fields[2]+" added");
   });
  }

  importContact(fields:string[], lineCount:number){
     if (fields.length <3 || !fields[1] || !fields[2]){
        this.addLog("line:"+lineCount+": contact needs name and code");
        return;
     }
     this.plaquesrv.createContact(fields[2], fields[1].toUpperCase());
     this.addLog("contact "+fields[1]+" added");
  }

  addLog(txt:string){
    this.importLog += txt+"\r\n";
  }
}
