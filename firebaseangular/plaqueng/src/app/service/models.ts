import {Utils} from './Utils';

export class Contact {
    Name : string;
    Code : string;
    IsPrint : boolean;
    LastPrint: string;
    ContactId: string;

    constructor(name:string, code:string){
        this.Name = name;
        this.Code = code;
        this.IsPrint = true;
        let now = new Date();  let year = now.getFullYear(); let month = now.getMonth(); let date = now.getDate();
        this.LastPrint = year.toString() + "-" + (month+1).toString() + "_" + date.toString();
    }

    static createNewContact(name:string, code:string):any{
        return new Contact(name, code);
    }
}

export class Live {
    Name: string;
    LiveId: string;
    IsPrint: boolean;
    LastPrint: string;
}

export class Dead {
    DeadName: string;
    LiveName: string;
    Relation: string;
    DeadId:string;
    IsPrint: boolean;
    LastPrint: string;
}

export class Ancestor {
    Surname: string;
    LiveName:string;
    AncestorId:string;
    IsPrint: boolean;
    LastPrint: string;
}

export class ColumnDefinition {
    constructor(public Name:string, public IsEditable: boolean, public PropertyName:string, public ViewTemplate:string){}
}

export const LiveColumns = [
    new ColumnDefinition("Pnt", false, "IsPrint", "<i class=\"fas fa-print\"></i>"),
    new ColumnDefinition("Name", true, "LiveName", ""),
    new ColumnDefinition("Last Print", false, "LastPrint", "")
];

export const DeadColumns = [
    new ColumnDefinition("Pnt", false, "IsPrint", "<i class=\"fas fa-print\"></i>"),
    new ColumnDefinition("Dead Name", true, "DeadName", ""),
    new ColumnDefinition("Live Name", true, "LiveName", ""),
    new ColumnDefinition("Relation", true, "Relation", ""),
    new ColumnDefinition("Last Print", false, "LastPrint", "")
];

export const AncestorColumns = [
    new ColumnDefinition("Pnt", false, "IsPrint", "<i class=\"fas fa-print\"></i>"),
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