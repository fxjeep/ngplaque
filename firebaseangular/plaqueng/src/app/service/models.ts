import {Utils} from './Utils';

export enum PrintedEnum {
    None=0,
    All=1,
    Partial=2
  }

export const PlaqueRecordType = {
    live:"l",
    dead:"d",
    ancester:"a",
    contact:"c"
}

export class Contact {
    Name : string;
    Code : string;
    IsPrinted : PrintedEnum;
    LastPrint: string;
    ContactId: string;
    SinglePrint: string;

    constructor(name:string, code:string){
        this.Name = name;
        this.Code = code;
        this.IsPrinted = PrintedEnum.None;
        let now = new Date();  
        let year = now.getFullYear(); let month = now.getMonth(); let date = now.getDate();
        this.LastPrint = year.toString() + "-" + (month+1).toString().padStart(2, '0') + "-" + date.toString().padStart(2,'0');
        this.SinglePrint = "0,0,0";
    }

    static createNewContact(name:string, code:string):any{
        return new Contact(name, code);
    }
}

export class Live {
    LiveName: string;
    LiveId: string;
    IsPrinted: boolean;
    LastPrint: string;
    ContactId: string;
    Added: string;

    reset() {
        this.LiveName = "";
    }

    mainName(){
        return this.LiveName;
    }
}

export class Dead {
    DeadName: string;
    LiveName: string;
    Relation: string;
    DeadId:string;
    IsPrinted: boolean;
    LastPrint: string;
    ContactId: string;
    Added: string;

    reset() {
        this.DeadName = "";
        this.LiveName = "";
        this.Relation = "";
    }

    mainName(){
        return this.DeadName;
    }
}

export class Ancestor {
    Surname: string;
    LiveName:string;
    AncestorId:string;
    IsPrinted: boolean;
    LastPrint: string;
    ContactId: string;
    Added: string;

    reset() {
        this.Surname = "";
        this.LiveName = "";
    }

    mainName(){
        return this.Surname+"-" + this.LiveName;
    }
}

export class ColumnDefinition {
    constructor(public Name:string, public IsEditable: boolean, public PropertyName:string, public ViewTemplate:string){}
}

export const LiveColumns = [
    new ColumnDefinition("Pnt", false, "IsPrinted", "<i class=\"fas fa-print\"></i>"),
    new ColumnDefinition("Name", true, "LiveName", ""),
    new ColumnDefinition("Last Print", false, "LastPrint", "")
];

export const DeadColumns = [
    new ColumnDefinition("Pnt", false, "IsPrinted", "<i class=\"fas fa-print\"></i>"),
    new ColumnDefinition("Dead Name", true, "DeadName", ""),
    new ColumnDefinition("Live Name", true, "LiveName", ""),
    new ColumnDefinition("Relation", true, "Relation", ""),
    new ColumnDefinition("Last Print", false, "LastPrint", "")
];

export const AncestorColumns = [
    new ColumnDefinition("Pnt", false, "IsPrinted", "<i class=\"fas fa-print\"></i>"),
    new ColumnDefinition("Surname", true, "Surname", ""),
    new ColumnDefinition("Live Name", true, "LiveName", ""),
    new ColumnDefinition("Last Print", false, "LastPrint", "")    
];

export function getColumnDefinition(ptype: PlaqueType) : ColumnDefinition[]{
    switch(ptype){
        case PlaqueType.live:
            return LiveColumns;
        case PlaqueType.dead:
            return DeadColumns;
        case PlaqueType.ancestor:
            return AncestorColumns
    }
}


export enum PlaqueType {
    live=1,
    dead=2,
    ancestor=3
}